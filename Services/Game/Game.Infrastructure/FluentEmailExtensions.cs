using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Infrastructure
{
    public static class FluentEmailExtensions
    {
        public static void AddFluentEmail(this IServiceCollection services, ConfigurationManager configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var defaultFromEmail = emailSettings["DefaultFromEmail"];
            var host = emailSettings["SMTPSetting:Host"];
            var port = emailSettings.GetValue<int>("SMTPSetting:Port");

            services.AddFluentEmail(defaultFromEmail).AddSmtpSender(host, port);
        }
    }
}
