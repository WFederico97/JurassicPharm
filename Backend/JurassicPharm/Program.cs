using JurassicPharm.Models;
using JurassicPharm.Repositories.Invoices;
using JurassicPharm.Services.Invoices;
using JurassicPharm.Repositories.Personnel.Implementations;
using JurassicPharm.Repositories.Personnel.Interfaces;
using JurassicPharm.Services.Personnel.Implementations;
using JurassicPharm.Services.Personnel.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<JurassicPharmContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();


builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
