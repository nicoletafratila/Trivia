using Common.ViewModels;
using EventBus.Contracts.Events;

namespace Game.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task Send(EmailMetadata emailMetadata);
        Task SendUsingTemplate(EmailMetadata emailMetadata, string template, ApplicationUserModel user);
        Task SendUsingTemplateFromFile(EmailMetadata emailMetadata, string templateFile, ApplicationUserModel user);
        Task SendWithAttachment(EmailMetadata emailMetadata);
    }
}
