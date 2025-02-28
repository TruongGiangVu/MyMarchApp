using System.Reflection;
using System.Text;

using MarchApi.Repositories;
using MarchApi.Services;
using MarchApi.Settings;
using MarchApi.Utils;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// * Serilog
string environment = builder.Environment.EnvironmentName;
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.WithComputed("SourceContext", "Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)")
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// * register swagger with summary
builder.Services.AddSwaggerGen(options =>
    {
        string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
);

// * mapp appsetting TokenSettings property with class TokenSettings
IConfigurationSection tokenSettingsConfig = builder.Configuration.GetSection(nameof(TokenSettings));
builder.Services.Configure<TokenSettings>(tokenSettingsConfig);
TokenSettings? tokenSettings = tokenSettingsConfig.Get<TokenSettings>();

// * register service for Authentication jwt bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        // ValidIssuer = builder.Configuration["TokenSettings:Issuer"],
        ValidIssuer = tokenSettings?.Issuer ?? string.Empty,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings?.SecretKey ?? string.Empty)),
        ClockSkew = TimeSpan.Zero
    };
});

// * register repository in source
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IToDoItemRepository, IToDotemRepository>();

// * register service in source
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();

const string rootPath = "/";
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGet(rootPath, context =>
        {
            context.Response.Redirect("swagger");
            return Task.CompletedTask;
        });
}
else
{
    app.MapGet(rootPath, () => $"${Ct.Common.AppName} is running");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExecutorLoggingMiddleware>();

app.MapControllers();

Log.Information(Ct.Common.AppName + " start successfully");
Log.Information("Run at environment: " + environment);

app.Run();
