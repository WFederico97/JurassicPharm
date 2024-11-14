using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using JurassicPharm.Models;

namespace JurassicPharm.Services.EmailSenderService.Implementations
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string toEmail, Empleado employee, string token, string subject)
        {
            // Obtener configuraciones de appsettings.json
            string smtpHost = _configuration["EmailSettings:SmtpHost"];
            int smtpPort = Convert.ToInt32(_configuration["EmailSettings:SmtpPort"]);
            string username = _configuration["EmailSettings:Username"];
            string password = _configuration["EmailSettings:Password"];


            string htmlTemplate = @$"
                                       <!DOCTYPE html>
                                    <html lang='es'>
                                    <head>
                                        <meta charset='UTF-8' />
                                        <meta name='viewport' content='width=device-width, initial-scale=1.0' />
                                        <title>Recuperación de Contraseña</title>
                                    </head>
                                    <body style='margin: 0; padding: 0; background-color: #1e1b2e; font-family: Arial, sans-serif;'>
                                        <table width='100%' border='0' cellspacing='0' cellpadding='0' style='background-color: #1e1b2e; height: 100vh; text-align: center;'>
                                            <tr>
                                                <td align='center' valign='middle'>
                                                    <!-- Contenedor principal -->
                                                    <table width='600' border='0' cellspacing='0' cellpadding='0' style='background-color: #ffffff; border-radius: 16px; overflow: hidden; box-shadow: 0 4px 24px rgba(0, 0, 0, 0.2);'>
                                                        <tr>
                                                            <td style='background-color: #1e1b2e; padding: 32px; text-align: center;'>
                                                                <!-- Logo -->
                                                                <div style='width: 100px; height: 100px; margin: 0 auto 20px; background-color: #7fb9aa; border-radius: 20px; display: inline-block;'>
                                                                    <img src='{_configuration["Logo_url"]}' alt='Jurassic Pharm Logo' style='width: 100%; height: 100%; border-radius: 20px;' />
                                                                </div>
                                                                <h1 style='color: white; font-size: 24px; font-weight: bold;'>Recuperación de Contraseña</h1>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='padding: 32px; color: #444; text-align: left;'>
                                                                <p>Estimado {employee.Nombre},</p>
                                                                <p>Hemos recibido una solicitud para restablecer la contraseña de tu cuenta. Si no has realizado esta solicitud, puedes ignorar este mensaje.</p>
                                                                <p>Para continuar con el proceso de recuperación de contraseña, por favor haz clic en el siguiente botón:</p>
                                                                <div style='text-align: center; margin: 32px 0;'>
                                                                    <a href='{_configuration["Front_Host"]}/Pages/reset-password.html?token={token}' style='background-color: #7fb9aa; color: #1e1b2e; padding: 16px 32px; text-decoration: none; border-radius: 8px; font-weight: bold; display: inline-block;'>Restablecer Contraseña</a>
                                                                </div>
                                                                <p>Por razones de seguridad, este enlace expirará en 15 minutos. Si necesitas un nuevo enlace, deberás realizar una nueva solicitud.</p>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='background-color: #f5f5f5; padding: 24px; text-align: center; border-top: 1px solid #eee;'>
                                                                <p style='color: #666; font-size: 14px; margin-bottom: 8px;'>Si tienes problemas haciendo clic en el botón, copia y pega el siguiente enlace en tu navegador:</p>
                                                                <a href='{_configuration["Front_Host"]}/Pages/reset-password.html?token={token}' style='color: #7fb9aa; text-decoration: none;'>{_configuration["Front_Host"]}/Pages/reset-password.html?token={token}</a>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style='background-color: #1e1b2e; padding: 24px; text-align: center;'>
                                                                <p style='color: #666; font-size: 14px; margin-bottom: 8px;'>Este es un mensaje automático, por favor no responda a este correo.</p>
                                                                <p style='color: #666; font-size: 14px;'>© 2024 Jurassic Pharm SA. Todos los derechos reservados.</p>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </body>
                                    </html>
                                    ";



            SmtpClient client = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(username, password)
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(username),
                Subject = subject,
                IsBodyHtml = true,
                Body = htmlTemplate
            };

            mailMessage.To.Add(toEmail);

            try
            {
                client.Send(mailMessage);
                Console.WriteLine("Correo enviado exitosamente");
            }
            catch (SmtpException ex)
            {
                Console.WriteLine($"Error al enviar el correo: {ex.Message}");
            }
        }
    }
}
