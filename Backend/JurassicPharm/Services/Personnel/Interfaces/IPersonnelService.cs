﻿using JurassicPharm.DTO.Cities;
using JurassicPharm.DTO.Personnel;
using JurassicPharm.DTO.Stores;
using JurassicPharm.Models;

namespace JurassicPharm.Services.Personnel.Interfaces
{
    public interface IPersonnelService
    {
        Task<bool> CreateEmployee(CreatePersonnelDTO employee);
        Task<List<GetPersonnelDTO>> GetAllPersonnel();
        Task<Empleado> GetPersonnel(int codigo);
        Task<bool> UpdatePersonnel(UpdatePersonnelDTO personnel, int legajo);
        Task<bool> DeletePersonnel(int legajo);
        Task<bool> ValidatePersonnelLogin(string email, string password);
        Task<Empleado> GetByEmail(string email);
        Task<List<GetCitySummaryDTO>> GetCities();
        Task<List<GetStoreDTO>> GetStores();
        Task<string> CheckProlongedPrescriptionDate(int clientId);

        Task ForgotPassword(string email);
        Task ResetPassword(string token, string newPassword);
    }
}
