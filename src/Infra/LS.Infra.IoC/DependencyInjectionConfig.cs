using FluentValidation.Results;
using Infra.Data.Write.Context;
using Infra.Data.Write.Repository;
using LS.Application.Clientes.Commands;
using LS.Application.Clientes.Events;
using LS.Application.Clientes.Queries;
using LS.Domain.Clientes.Interfaces;
using LS.Domain.Core.Data;
using LS.Domain.Core.Mediator;
using LS.Domain.Interfaces;
using LS.Infra.CrossCutting.Identity;
using LS.Infra.Data.Read.Context;
using LS.Infra.Data.Read.Interfaces;
using LS.Infra.Data.Read.Repository;
using LS.Infra.Data.Read.UoW;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace LS.Infra.IoC
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain - Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Application - Events
            services.AddScoped<INotificationHandler<ClienteCadastradoEvent>, ClienteCadastroEventHandler>();
            services.AddScoped<INotificationHandler<CadastroClienteCompletoEvent>, ClienteCadastroEventHandler>();
            services.AddScoped<INotificationHandler<CadastroClienteCompletoEvent>, ClienteCadastroEventHandler>();

            // Application - Commands
            services.AddScoped<IRequestHandler<CadastroClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CompletaCadastroClienteCommand, ValidationResult>, ClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastroClienteEnderecoCommand, ValidationResult>, ClienteCommandHandler>();

            // Application - Queries
            services.AddScoped<IClienteQuerie, ClienteQuerie>();

            // Infra - Data - Write
            services.AddScoped<IClienteWriteRepository, ClienteWriteRepository>();
            services.AddScoped<WriteContext>();

            // Infra - Data - Read
            services.AddScoped<IReadContext, ReadContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IClienteReadRepository, ClienteReadRepository>();

            // Infra - Identity
            services.AddScoped<IUser, AspNetUser>();
        }
    }
}
