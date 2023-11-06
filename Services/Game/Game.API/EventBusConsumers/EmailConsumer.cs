using EventBus.Contracts.Events;
using Game.Application.Interfaces.Services;
using MassTransit;

namespace TriviaCsv.Services.Game.API.EventBusConsumers
{
    public class EmailConsumer : IConsumer<EmailMetadata>
    {
        private readonly IEmailService _emailService;

        public EmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task Consume(ConsumeContext<EmailMetadata> context)
        {
            if (_emailService != null && context != null && context.Message != null)
            {
                EmailMetadata emailMetadata = new(context.Message.ToAddresses, context.Message.Subject, context.Message.Body);
                await _emailService.Send(emailMetadata);
            }
        }
    }
}