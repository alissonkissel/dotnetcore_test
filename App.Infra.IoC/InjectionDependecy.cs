using Microsoft.Extensions.DependencyInjection;
using System;
using App.Infra.Data;
using App.Domain.Interfaces;
using App.Application;

namespace App.Infra.IoC
{
    public class InjectionDependecy
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IApplication, Application.Application>();
            services.AddScoped<IRepository, Repository>();
        }
    }
}

