using Database;
using Handlers;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
builder.Services.AddDbContext<Context>();
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "AISmartHomeDotNet", Version = "v1" }); });
builder.Services.AddLogging( b=> b.AddConsole());
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<AisParser.Parser>();
//builder.Services.AddHostedService<AISservice>();
builder.Services.AddScoped<Context>();

builder.Services.AddScoped<IVesselRepository, VesselRepository>();
builder.Services.AddScoped<AISService>();
builder.Services.AddHostedService<HighPerformanceServer>();
builder.Services.AddHealthChecks();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var repo = scope.ServiceProvider.GetRequiredService<IVesselRepository>();
    await repo.EnsureCreated(new CancellationToken());
}

app.MapGroup("/vessels").MapVesselEndpoints();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "AISmartHomeDotNet");
});


TheConfiguration.Setup(Environment.GetEnvironmentVariables());

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapHealthChecks("/health");

app.Run();