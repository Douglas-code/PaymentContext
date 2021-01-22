using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            this._studentRepository = studentRepository;
            this._emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            //Verificar se Documento já está Cadastrado
            if (this._studentRepository.DocumentExits(command.Document))
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            //Verificar se Email já existe
            if (this._studentRepository.EmailExists(command.Email))
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");
            }

            //Gerar VOs
            Name name = new Name(command.FirstName, command.LastName);
            Document document = new Document(command.Document, EDocumentType.CPF);
            Email email = new Email(command.Email);
            Address address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State,
                command.Country, command.ZipCode);

            //GerarEntidades
            Student student = new Student(name, document, email);
            Subscription subscription = new Subscription(DateTime.Now.AddMonths(1));
            Payment payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, 
                command.Total, command.TotalPaid, command.Payer, new Document(command.PayerDocument, command.PayerDocumentType),
                address, email);

            //Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            //Agrupar Validacao
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar Notificacoes
            if(Invalid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");

            //SalvarInformações
            this._studentRepository.CreateSubscription(student);

            //Enviar Email de boas vindas
            this._emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo!", "Sua assinatura foi criada");

            return new CommandResult(true, "Cadastrado com sucesso");
        }
    }
}
