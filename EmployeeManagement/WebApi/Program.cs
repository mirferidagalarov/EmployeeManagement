using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependcyResolvers;
using Business.Mappers;
using Core.Security;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.DatabaseContext;
using Entities.Concrete.Membership;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoFac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
                       .ConfigureContainer<ContainerBuilder>(builder =>
                       {
                           builder.RegisterModule(new AutofacBusinessModule());
                       });
#endregion

#region  ConnectionDatabase
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("localDb"));
});
#endregion

#region SwaggerConfiguration
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter the Bearer Authorization: `Bearer Genreated-JWT-Token`",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            },new string[] {}
        }
    });
});

#endregion

#region Injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region  CorsConfiguration
builder.Services.AddCors(option =>
{
    option.AddPolicy("cors", policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    });
});
#endregion

#region IdentityConfiguration
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 1;
    //
    options.User.RequireUniqueEmail = false;
    //
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);

}).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
#endregion

#region TokenConfiguration
var tokenConfig = builder.Configuration.GetSection("TokenOptions").Get<Business.Security.JWT.TokenOptions>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenConfig.Issuer,
        ValidAudience = tokenConfig.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenConfig.SecurityKey),
    };
});
#endregion

#region AutoMapper
builder.Services.AddAutoMapper(typeof(Automapper));
#endregion

#region  UrlLowercase
builder.Services.AddRouting(x => x.LowercaseUrls = true);
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("cors");

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();
