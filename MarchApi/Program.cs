using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

using MarchApi.Repositories.Implements;
using MarchApi.Repositories.Interfaces;
using MarchApi.Services.Implements;
using MarchApi.Services.Interfaces;
using MarchApi.Settings;
using MarchApi.Utilities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// * Serilog
// đọc serilog config từ file appsettings.json hoặc file appsettings.*.json, tùy vào môi trường api đang chạy
string environment = builder.Environment.EnvironmentName;
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

// config lại logger của api
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.WithComputed("SourceContext", "Substring(SourceContext, LastIndexOf(SourceContext, '.') + 1)") // chỗ placeholder SourceContext sẽ log tên class mà ko log namespace của log 
    .CreateLogger();

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    // serialize enums as strings in api responses
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// * register swagger with summary
builder.Services.AddSwaggerGen(options =>
    {
        string xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

        // hiển thị ổ khoá trên swagger
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    }
);

// * map appsettings.json trường TokenSettings với class TokenSettings
IConfigurationSection tokenSettingsConfig = builder.Configuration.GetSection(nameof(TokenSettings));
builder.Services.Configure<TokenSettings>(tokenSettingsConfig);
TokenSettings? tokenSettings = tokenSettingsConfig.Get<TokenSettings>();

// * map appsettings.json trường AppSettings với class AppSettings
IConfigurationSection appSettingsConfig = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(appSettingsConfig);
AppSettings? appSettings = appSettingsConfig.Get<AppSettings>();

// * đăng ký service cho Authentication jwt bearer
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

builder.Services.AddHttpContextAccessor();

// * đăng ký repository
builder.Services.AddDbContext<MarchContext>(o => o.UseSqlite(appSettings?.ConnectionString));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IToDoItemRepository, ToDoItemRepository>();
builder.Services.AddScoped<IToDoCheckListRepository, ToDoCheckListRepository>();

// * đăng ký service
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IToDoService, ToDoService>();

var app = builder.Build();

const string rootPath = "/";
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // tự động chuyển qua trang swagger
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

// thêm middleware để log nếu api trả về http status code 401 hoặc 403
app.UseMiddleware<AuthorizationLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

// thêm middleware để lấy userId từ token và thêm log userId vào serilog
app.UseMiddleware<ExecutorLoggingMiddleware>();

app.MapControllers();

Log.Information($"{Ct.Common.AppName} start successfully");
Log.Information($"Run at environment: {environment}");

app.Run();
