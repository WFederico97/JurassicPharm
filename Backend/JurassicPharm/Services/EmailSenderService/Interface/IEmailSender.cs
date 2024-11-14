using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JurassicPharm.Models;

namespace JurassicPharm.Services.EmailSenderService
{
    public interface IEmailSender
    {
        void SendEmail(string toEmail, Empleado employee, string token, string subject);
    }
}