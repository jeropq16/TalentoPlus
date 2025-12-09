using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

namespace TalentoPlus.Application.Services;

public class EmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendWelcomeEmail(string toEmail, string password)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("TalentoPlus", _config["SMTP:Username"]));
        emailMessage.To.Add(new MailboxAddress("", toEmail));
        emailMessage.Subject = "Bienvenido a TalentoPlus";

        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"¡Hola! Tu cuenta ha sido creada exitosamente. Aquí tienes tu contraseña: {password}. Puedes cambiarla en cualquier momento."
        };

        emailMessage.Body = bodyBuilder.ToMessageBody();

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]), false);
            await smtp.AuthenticateAsync(_config["SMTP:Username"], _config["SMTP:Password"]);
            await smtp.SendAsync(emailMessage);
            await smtp.DisconnectAsync(true);
        }
    }
}