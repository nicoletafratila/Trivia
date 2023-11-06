using AutoMapper;
using FluentValidation;
using Game.Application;
using Game.Application.Features.Games.Queries.GetCurrentQuestion;
using Game.Application.Mappers;
using Game.Infrastructure;
using Game.Infrastructure.Data;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Reflection;
using TriviaCsv.Services.Game.API;
using TriviaCsv.Services.Game.API.BackgroundJobs;
using TriviaCsv.Services.Game.API.Configuration;
using TriviaCsv.Services.Game.API.EventBusConsumers;
using TriviaCsv.Services.Game.API.Hubs;
using TriviaCsv.Services.Game.API.Pipelines;
using TriviaCsv.Services.Game.API.Services;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var identityServerOptions = new IdentityServerOptions();
        builder.Configuration.GetSection(nameof(IdentityServerOptions)).Bind(identityServerOptions);

        builder.Services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication("Bearer", options =>
            {
                options.Authority = identityServerOptions.Authority;
                options.ApiName = identityServerOptions.ApiName;
            });

        builder.Services.AddCors(o => o.AddPolicy("client", policy => policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .WithExposedHeaders("*")));

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        var config = new MapperConfiguration(c =>
        {
            c.AddProfile<CategoryProfile>();
            c.AddProfile<QuestionProfile>();
            c.AddProfile<TriviaGameProfile>();
            c.AddProfile<PlayerProfile>();
            c.AddProfile<PlayerHistoryProfile>();
        });
        builder.Services.AddSingleton(s => config.CreateMapper());

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            builder.Services.AddInfrastructure(connectionString);
        }

        builder.Services.AddFluentEmail(builder.Configuration);

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationHandler<,>));
        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceHandler<,>));
        var assembly = typeof(GetCurrentQuestionQuery).GetTypeInfo().Assembly;
        builder.Services.AddValidatorsFromAssemblies(new[] { assembly });

        builder.Services.Configure<QuartzOptions>(options =>
        {
            options.Scheduling.IgnoreDuplicates = true;
            options.Scheduling.OverWriteExistingData = true;
        });

        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
        builder.Services.AddLogging();
        builder.Services.AddDbContext<TriviaDbContext>(
            options => options.UseSqlServer(connectionString, opt => opt.MigrationsAssembly(migrationsAssembly))
        );

        builder.Services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();

            q.UseSimpleTypeLoader();
            q.UseInMemoryStore();
            q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });

            q.ScheduleJob<MonthlyJob>(trigger => trigger
                .WithIdentity("Trivia Game Trigger")
                .WithSchedule(CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(1, 1, 0)));
        });

        builder.Services.AddCore();
        builder.Services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        //MassTransit.RabbitMq
        builder.Services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.AddConsumer<EmailConsumer>();
            busConfigurator.AddConsumer<UserRegisteredConsumer>();
            
            busConfigurator.UsingRabbitMq((context, busFactoryConfigurator) =>
            {
                busFactoryConfigurator.Host("localhost", hostConfigurator => { });
                busFactoryConfigurator.ReceiveEndpoint("email-created-listener", e =>
                {
                    e.ConfigureConsumer<EmailConsumer>(context);
                });
                busFactoryConfigurator.ReceiveEndpoint("user-registered-listener", e =>
                {
                    e.ConfigureConsumer<UserRegisteredConsumer>(context);
                });
            });
        });

        //Timer
        builder.Services.AddHostedService<QuestionTimerService>();
        builder.Services.AddSignalR();
        builder.Services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });

        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        var app = builder.Build();

        app.UseResponseCompression();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("client");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.MapHub<NewCurrentQuestionHub>("/newcurrentquestionhub");

        await MigrateDatabase(app);

        app.Run();
    }

    static async Task MigrateDatabase(WebApplication webApplication)
    {
        using var scope = webApplication.Services.GetService<IServiceScopeFactory>()?.CreateScope();
        if (scope != null)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<TriviaDbContext>();
            await dbContext.Database.MigrateAsync();
            await SeedData.EnsureSeedData(dbContext);
        }
    }
}