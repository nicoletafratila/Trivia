using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Application;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }   
}