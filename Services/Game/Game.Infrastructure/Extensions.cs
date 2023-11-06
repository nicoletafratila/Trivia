using Game.Application.Interfaces.Persistence;
using Game.Application.Interfaces.Services;
using Game.Infrastructure.Data;
using Game.Infrastructure.Data.Repositories;
using Game.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IGameParticipantsRepository, GameParticipantsRepository>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IGameQuestionsRepository, GameQuestionsRepository>();
            services.AddScoped<ITriviaGamesRepository, TriviaGamesRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IPlayersRepository,PlayersRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IPlayersHistoryRepository,PlayersHistoryRepository>();

            services.AddDbContext<TriviaDbContext>(options =>
            {
                options.UseSqlServer(connectionString,b=>b.MigrationsAssembly("Game.Infrastructure"));
                options.EnableSensitiveDataLogging();
            });
        }
    }
}
