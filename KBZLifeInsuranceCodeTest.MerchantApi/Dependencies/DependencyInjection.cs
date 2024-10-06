using KBZLifeInsuranceCodeTest.DbService.AppDbContextModels;
using KBZLifeInsuranceCodeTest.MerchantApi.Features.GiftCard;
using KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice;
using KBZLifeInsuranceCodeTest.MerchantApi.Features.PurchaseInvoice.Purchase;
using KBZLifeInsuranceCodeTest.Shared.Services;
using KBZLifeInsuranceCodeTest.Shared.Services.AuthServices;
using KBZLifeInsuranceCodeTest.Shared.Services.CacheServices;
using KBZLifeInsuranceCodeTest.Shared.Services.SecurityServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace KBZLifeInsuranceCodeTest.MerchantApi.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return services.AddDbContextService(builder)
            .AddMediatRService()
            .AddAuthenticationService(builder)
            .AddRepositoryServices()
            .AddCustomServices()
            .AddValidatorServices();
    }

    private static IServiceCollection AddDbContextService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySQL(builder.Configuration.GetConnectionString("DbConnection")!);
            opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }, ServiceLifetime.Transient, ServiceLifetime.Transient);

        return services;
    }

    private static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        return services.AddScoped<IGiftCardRepository, GiftCardRepository>()
            .AddScoped<IPurchaseInvoiceRepository, PurchaseInvoiceRepository>();
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
        return services.AddScoped<AesService>().AddScoped<JwtService>()
            .AddTransient<TokenValidationService>().AddScoped<DapperService>()
            .AddScoped<RedisService>();
    }

    private static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        return services.AddScoped<PurchaseValidator>();
    }
}
