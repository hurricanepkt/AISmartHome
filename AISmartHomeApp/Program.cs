using Database;
using Handlers;
using Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<FileContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "AISmartHomeDotNet", Version = "v1" }); });
builder.Services.AddLogging( b=> b.AddConsole());
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<AisParser.Parser>();
builder.Services.AddHostedService<AISservice>();
builder.Services.AddSingleton<FileContext>();
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();
builder.Services.AddTransient<ReadOnlyDBRepo>();

// builder.Services.AddHostedService<RepeatingService>();

MessageStrategySetup.Setup(builder.Services);

builder.Services.AddHealthChecks();

var app = builder.Build();

CallbackHandlers.Setup(app.MapGroup("/vessels"), app.Services.GetService<ReadOnlyDBRepo>());
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "AISmartHomeDotNet");
});

StaticLoggerFactory.Initialize(app.Services.GetRequiredService<ILoggerFactory>());
TheConfiguration.Setup(Environment.GetEnvironmentVariables());

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapHealthChecks("/health");
app.Run();