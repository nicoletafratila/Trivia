using Game.Blazor;
using Game.Blazor.Configuration;
using Game.Blazor.Interfaces;
using Game.Blazor.Services;
using Game.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
});

var identityServerOptions = new IdentityServerOptions();
builder.Configuration.GetSection(nameof(IdentityServerOptions)).Bind(identityServerOptions);
builder.Services.AddSingleton(typeof(IdentityServerOptions), identityServerOptions);

//leave this here since we will call the trivia API as well

var triviaApiOptions = new TriviaApiOptions();
triviaApiOptions.BlazorBaseUrl = builder.HostEnvironment.BaseAddress;
builder.Configuration.GetSection(nameof(TriviaApiOptions)).Bind(triviaApiOptions);
builder.Services.AddSingleton(typeof(TriviaApiOptions), triviaApiOptions);
builder.Services.AddScoped<AuthHandler>();

//attach the authHandle to the httpclient in order to automatically add the Authorization header to the requests that the client makes
//without this you'll have to add the token manually otherwise you'll get a 401
builder.Services.AddHttpClient<IUsersService, UsersService>(client => 
    client.BaseAddress = new Uri(identityServerOptions.Authority)).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddHttpClient<ITriviaGameService, TriviaGameService>(client =>
    client.BaseAddress = new Uri(triviaApiOptions.BaseUrl)).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddHttpClient<IQuestionService, QuestionService>(client =>
    client.BaseAddress = new Uri(triviaApiOptions.BaseUrl)).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddHttpClient<ICategoryService, CategoryService>(client => client.BaseAddress = new Uri(triviaApiOptions.BaseUrl));

builder.Services.AddHttpClient<IHistoryService, HistoryService>(client =>
    client.BaseAddress = new Uri(triviaApiOptions.BaseUrl)).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddScoped<IWriterService, CsvWriterService>();

builder.Services.AddHttpClient<IPlayerService, PlayerService>(client =>
    client.BaseAddress = new Uri(triviaApiOptions.BaseUrl)).AddHttpMessageHandler<AuthHandler>();

builder.Services.AddHttpClient<IPlayerHistoryService, PlayerHistoryService>(client =>
    client.BaseAddress = new Uri(triviaApiOptions.BaseUrl)).AddHttpMessageHandler<AuthHandler>();

await builder.Build().RunAsync();
