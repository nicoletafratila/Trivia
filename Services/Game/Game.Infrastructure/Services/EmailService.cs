using Common.ViewModels;
using EventBus.Contracts.Events;
using FluentEmail.Core;
using Game.Application.Interfaces.Services;

namespace Game.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;

        public EmailService(IFluentEmailFactory fluentEmailFactory)
        {
            _fluentEmailFactory = fluentEmailFactory ?? throw new ArgumentNullException(nameof(fluentEmailFactory));
        }

        public async Task Send(EmailMetadata emailMetadata)
        {
            if (emailMetadata.ToAddresses != null && emailMetadata.ToAddresses.Any())
            {
                foreach (var address in emailMetadata.ToAddresses)
                {
                    await _fluentEmailFactory
                        .Create()
                        .To(address)
                        .Subject(emailMetadata.Subject)
                        .Body(emailMetadata.Body)
                        .SendAsync();
                }
            }
        }

        public async Task SendUsingTemplate(EmailMetadata emailMetadata, string template, ApplicationUserModel user)
        {
            if (emailMetadata.ToAddresses != null && emailMetadata.ToAddresses.Any())
            {
                foreach (var address in emailMetadata.ToAddresses)
                {
                    await _fluentEmailFactory
                        .Create()
                        .To(address)
                        .Subject(emailMetadata.Subject)
                        .UsingTemplate(template, user)
                        .SendAsync();
                }
            }
        }

        public async Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, string templateFile, ApplicationUserModel user)
        {
            if (emailMetadata.ToAddresses != null && emailMetadata.ToAddresses.Any())
            {
                foreach (var address in emailMetadata.ToAddresses)
                {
                    await _fluentEmailFactory
                        .Create()
                        .To(address)
                        .Subject(emailMetadata.Subject)
                        .UsingTemplateFromFile(templateFile, user)
                        .SendAsync();
                }
            }
        }

        public async Task SendWithAttachment(EmailMetadata emailMetadata)
        {
            if (emailMetadata.ToAddresses != null && emailMetadata.ToAddresses.Any())
            {
                foreach (var address in emailMetadata.ToAddresses)
                {
                    await _fluentEmailFactory
                        .Create()
                        .To(address)
                        .Subject(emailMetadata.Subject)
                        .AttachFromFilename(emailMetadata.AttachmentPath, attachmentName: Path.GetFileName(emailMetadata.AttachmentPath))
                        .Body(emailMetadata.Body)
                        .SendAsync();
                }
            }
        }
    }
}
