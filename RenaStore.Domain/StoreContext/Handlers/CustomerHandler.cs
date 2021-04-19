using System;
using FluentValidator;
using RenaStore.Domain.StoreContext.Commands.CustomerCommands.Inputs;
using RenaStore.Domain.StoreContext.Commands.CustomerCommands.Outputs;
using RenaStore.Domain.StoreContext.Entities;
using RenaStore.Domain.StoreContext.Repositories;
using RenaStore.Domain.StoreContext.Services;
using RenaStore.Domain.StoreContext.ValueObjects;
using RenaStore.Shared.Commands;

namespace RenaStore.Domain.StoreContext.Handlers
{
    public class CustomerHandler : Notifiable, ICommandHandler<CreateCustomerCommand>, ICommandHandler<AddAddressCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEmailService _emailService;
        public CustomerHandler(ICustomerRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Headle(CreateCustomerCommand command)
        {
            // Verificar se o CPF já existe na base
            if (_repository.CheckDocument(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se o Email já existe na base
            if (_repository.CheckEmail(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Criar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document);
            var email = new Email(command.Email);

            // Criar a entidade
            var customer = new Customer(name, document, email, command.Phone);

            // Validar entidades e Vos
            AddNotifications(name.Notifications);
            AddNotifications(document.Notifications);
            AddNotifications(email.Notifications);
            AddNotifications(customer.Notifications);

            if (Invalid)
                return new CommandResult(false, "Por favor, corrija os campos, null", Notifications);

            // Persistir o cliente
            _repository.Save(customer);

            // Enviar um email de boas vindas
            _emailService.Send(email.Address, "ree.arua@gmail.com", "Bem vindo", "Bem vindo ao Rena Store!!!!");

            // Retornar o resultado para a tela
            return new CommandResult(true, "Bem vindo", new
            {
                Id = customer.Id,
                Name = name.ToString(),
                Email = email.Address
            });
        }

        public ICommandResult Headle(AddAddressCommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}