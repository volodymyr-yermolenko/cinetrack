using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CineTrack.App.Interfaces;
using CineTrack.Infrastructure.Common.Helpers;
using CineTrack.Infrastructure.Settings;
using CineTrack.Infrastructure.Persistence;
using CineTrack.Infrastructure.Repositories;
using CineTrack.Infrastructure.Services;
using CineTrack.App.Common.Settings;

namespace CineTrack.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddJwtAuthentication(configuration);
        
        services.AddOptions<EmailSettings>()
            .Bind(configuration.GetSection("EmailSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<WebSiteSettings>()
            .Bind(configuration.GetSection("WebSiteSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                sqlServerOptionsAction: sqlOptions =>
                {
                    // Set the global Command Timeout (in seconds)
                    sqlOptions.CommandTimeout(120);
                    // Important: Also enable retries for the Azure "wake-up" period
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);                    
                }
            ));
        
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<IWatchEntryRepository, WatchEntryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IMailSender, MailSender>();
        services.AddScoped<ITokenService, JwtTokenService>();
        
        return services;
    }    
    
    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<JwtSettings>()
            .Bind(configuration.GetSection("JwtSettings"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtSettings>>((options, jwtSettings) =>
            {
                var settings = jwtSettings.Value;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = SecurityKeyHelper.GetSymmetricSecurityKey(settings.SecretKey),
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        // Register JwtTokenService for generating JWT tokens during authentication
        services.AddScoped<JwtTokenService>();
        
        return services;
    }    
}