﻿namespace KBZLifeInsuranceCodeTest.GiftCardManagementSystem.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        return services
            .AddDbContextService(builder)
            .AddValidatorServices()
            .AddMediatRService()
            .AddAuthenticationService(builder)
            .AddRepositoryServices()
            .AddCustomServices();
    }

    private static IServiceCollection AddDbContextService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder.Services.AddDbContext<AppDbContext>(
            opt =>
            {
                opt.UseMySQL(builder.Configuration.GetConnectionString("DbConnection")!);
                opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            },
            ServiceLifetime.Transient,
            ServiceLifetime.Transient
        );

        return services;
    }

    private static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IGiftCardRepository, GiftCardRepository>();
    }

    private static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        return services
            .AddScoped<CreateAccountValidator>()
            .AddScoped<LoginValidator>()
            .AddScoped<UpdateGiftCardValidator>();
    }

    private static IServiceCollection AddMediatRService(this IServiceCollection services)
    {
        return services.AddMediatR(cf =>
            cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly)
        );
    }

    private static IServiceCollection AddAuthenticationService(
        this IServiceCollection services,
        WebApplicationBuilder builder
    )
    {
        builder
            .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)
                    )
                };
            });

        return services;
    }

    private static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        return services
            .AddScoped<AesService>()
            .AddScoped<JwtService>()
            .AddScoped<QRService>()
            .AddTransient<TokenValidationService>()
            .AddScoped<DapperService>()
            .AddScoped<RedisService>();
    }

    public static IApplicationBuilder UseAuthMiddleware(this WebApplication app)
    {
        return app.UseMiddleware<AuthenticationMiddleware>();
    }
}
