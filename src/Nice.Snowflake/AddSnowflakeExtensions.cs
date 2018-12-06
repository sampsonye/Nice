using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace Nice.Snowflake
{
    public static class AddSnowflakeExtensions
    {
        public static IServiceCollection AddSnowflake(this IServiceCollection services)
        {
            services.AddSingleton<>()
            return services;
        }
    }
}
