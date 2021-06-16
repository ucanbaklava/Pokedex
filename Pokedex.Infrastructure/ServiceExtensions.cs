using Microsoft.Extensions.DependencyInjection;
using Pokedex.Application.Interfaces;
using Pokedex.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IPokemonRepository, PokemonRepository>();
            services.AddTransient<IMoveRepository, MoveRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
