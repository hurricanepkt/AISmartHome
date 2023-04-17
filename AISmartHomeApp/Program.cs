using Database;
using Handlers;
using Infrastructure;
using Microsoft.Extensions.Caching.Memory;
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
builder.Services.AddSingleton<Context>();
builder.Services.AddScoped<VesselsHandler>();
builder.Services.AddScoped<IVesselRepository, VesselRepository>();
builder.Services.AddScoped<AISService>();
builder.Services.AddScoped<PacketServer>();
// builder.Services.AddHostedService<RepeatingService>();
builder.Services.AddHealthChecks();


var app = builder.Build();
StaticLoggerFactory.Initialize(app.Services.GetRequiredService<ILoggerFactory>());
using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<Context>();
    DbInitializer.Initialize(dbcontext);
    var handlers = scope.ServiceProvider.GetRequiredService<VesselsHandler>();
    handlers.Setup(app.MapGroup("/vessels"));
    var blah = scope.ServiceProvider.GetRequiredService<PacketServer>();
    blah.Start();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "AISmartHomeDotNet");
});


TheConfiguration.Setup(Environment.GetEnvironmentVariables());

app.MapGet("/", () => Results.Redirect("/swagger"));
app.MapHealthChecks("/health");

app.Run();