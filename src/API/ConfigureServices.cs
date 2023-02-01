using API.Mapper;
using Infrastructure.Repositry.Common;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json.Serialization;

namespace API;

public static class ConfigureServices
{
    public static string LogFolder = "Logs6";

    public static IServiceCollection AddAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddDbContext<ApplicationDbContext>();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);


        services.AddDistributedMemoryCache();

        services.AddAuthentication();

        services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

        services.AddControllers().AddJsonOptions(x =>
        {
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        return services;
    }


}