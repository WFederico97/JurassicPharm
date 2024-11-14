using System.IdentityModel.Tokens.Jwt;
using JurassicPharm.DTO.Cities;
using JurassicPharm.DTO.Personnel;
using JurassicPharm.DTO.Stores;
using JurassicPharm.Models;
using JurassicPharm.Repositories.Personnel.Interfaces;
using JurassicPharm.Services.EmailSenderService;
using JurassicPharm.Services.JWT;
using JurassicPharm.Services.Personnel.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace JurassicPharm.Services.Personnel.Implementations
{
    public class PersonnelService : IPersonnelService
    {
        public const int FORGOT_TOKEN_EXPIRATION_TIME = 15;
        private readonly IPersonnelRepository _repository;
        private readonly JwtService _jwtService;
        private readonly IEmailSender _emailSenderService;

        public PersonnelService(IPersonnelRepository repository, JwtService jwtService, IEmailSender emailSender)
        {
            _repository = repository;
            _jwtService = jwtService;
            _emailSenderService = emailSender;
        }
        public async Task<List<GetCitySummaryDTO>> GetCities()
        {
            return await _repository.GetCities();
        }

        public async Task<List<GetStoreDTO>> GetStores()
        {
            return await _repository.GetStores();
        }
        public async Task<List<GetPersonnelDTO>> GetAllPersonnel()
        {
            return await _repository.GetAllPersonnel();
        }

        public async Task<Empleado> GetPersonnel(int codigo)
        {
            return await _repository.GetPersonnel(codigo);
        }
        public async Task<bool> CreateEmployee(CreatePersonnelDTO employee)
        {
            return await _repository.CreateEmployee(employee);
        }
        public async Task<bool> UpdatePersonnel(UpdatePersonnelDTO personnel, int legajo)
        {
            return await _repository.UpdatePersonnel(personnel, legajo);
        }

        public async Task<bool> DeletePersonnel(int legajo)
        {
            return await _repository.DeletePersonnel(legajo);
        }
        public async Task<bool> ValidatePersonnelLogin(string email, string password)
        {
            return await _repository.ValidatePersonnelLogin(email, password);
        }

        public async Task<Empleado> GetByEmail(string email)
        {
            return await _repository.GetByEmail(email);
        }
        public async Task<string> CheckProlongedPrescriptionDate(int clientId)
        {
            return await _repository.CheckProlongedPrescriptionDate(clientId);
        }

        public async Task ForgotPassword(string email)
        {
            Empleado employee = await GetByEmail(email);

            if (employee == null) return;

            string token = _jwtService.GenerateJwtToken(email, employee, FORGOT_TOKEN_EXPIRATION_TIME);

            _emailSenderService.SendEmail(email, employee, token, "Recuperación de acceso a tu cuenta");
        }

        public async Task ResetPassword(string token, string newPassword)
        {
            //Validate and verify token 
            if (!_jwtService.VerifyToken(token))
            {
                throw new InvalidOperationException("Invalid or expired token");
            }

            var data = _jwtService.DecodeToken(token);

            int userId = Convert.ToInt32(data["UserId"]);

            Empleado employee = await _repository.GetPersonnel(userId);

            //Hasheo de password 
            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword, 13);

            employee.PasswordEmpleado = hashedPassword;

            UpdatePersonnelDTO employeeToUpdate = new ResetEmployeePassword()
            {
                NewPassword = hashedPassword
            };

            await _repository.UpdatePersonnel(employeeToUpdate, userId);

        }
    }
}
