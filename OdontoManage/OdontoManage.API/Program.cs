using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql;
using OdontoManage.Application.Interfaces;
using OdontoManage.Application.Services;
using OdontoManage.Application.Util;
using OdontoManage.Core.Interfaces;
using OdontoManage.Core.Models;
using OdontoManage.Infrastructure.Data;
using OdontoManage.Infrastructure.Repositories;
using Formatting = Newtonsoft.Json.Formatting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/*
 * Swagger configuration
 */
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Docs", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

/*
 * Database configuration
 */
builder.Configuration.AddJsonFile("appsettings.json");
builder.Services.AddDbContext<OdontoManageDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

/*
 * Mapping configuration
 */
builder.Services.AddAutoMapper(typeof(MappingConfiguration));

/*
 * Authentication configuration
 */
var secret = builder.Configuration.GetSection("Security:Key").ToString();
var key = Encoding.ASCII.GetBytes(secret);
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

/*
 * Authorization configuration
 */
builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        JwtBearerDefaults.AuthenticationScheme);
    defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

/*
 * CORS configuration
 */
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllCorsPolicy",
        corsPolicyBuilder =>
            corsPolicyBuilder.SetIsOriginAllowed((host) => true)
                .AllowCredentials()
                .AllowAnyMethod()
                // .AllowAnyOrigin()
                .WithMethods("GET", "PUT", "POST", "DELETE")
                .AllowAnyHeader());

});

builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.DateFormatString = "yyyy-MM-dd:HH:mm:ss";
        options.SerializerSettings.Formatting = Formatting.Indented;
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.AllowInputFormatterExceptionMessages = true;
    });


/*
 * Dependency injection configuration
 */
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();

builder.Services.AddScoped<IDentistRepository, DentistRepository>();
builder.Services.AddScoped<IDentistService, DentistService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseCors("AllowAllCorsPolicy");

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

using var scope = app.Services.CreateScope();
try
{
    var context = scope.ServiceProvider.GetRequiredService<OdontoManageDbContext>();
    context.Database.Migrate();
    if (!context.Users!.Any())
    {
        var user = new User
        {
            Name = "Admnistrador",
            Username = "admin"
        };
        user.SetPassword("admin@123");

        context.Users?.Add(user);
        context.SaveChanges();
    }
}
catch (NpgsqlException e)
{
    Console.WriteLine(e);
}

app.Run();