using JurassicPharm.Models;
using JurassicPharm.Repositories.Branches.Implementations;
using JurassicPharm.Repositories.Branches.Interfaces;
using JurassicPharm.Repositories.Clients.Implementations;
using JurassicPharm.Repositories.Clients.Interfaces;
using JurassicPharm.Repositories.Invoices;
using JurassicPharm.Repositories.Personnel.Implementations;
using JurassicPharm.Repositories.Personnel.Interfaces;
using JurassicPharm.Repositories.Supplies.implementations;
using JurassicPharm.Repositories.Supplies.Interfaces;
using JurassicPharm.Services.Branches.Implementations;
using JurassicPharm.Services.Branches.Interfaces;
using JurassicPharm.Services.Clients.Implementations;
using JurassicPharm.Services.Clients.Interfaces;
using JurassicPharm.Services.EmailSenderService;
using JurassicPharm.Services.EmailSenderService.Implementations;
using JurassicPharm.Services.HealthPlan.Interfaces;
using JurassicPharm.Services.HealthPlan.Implementations;
using JurassicPharm.Services.Invoices;
using JurassicPharm.Services.JWT;
using JurassicPharm.Services.Personnel.Implementations;
using JurassicPharm.Services.Personnel.Interfaces;
using JurassicPharm.Services.Supplies.Implementations;
using JurassicPharm.Services.Supplies.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using JurassicPharm.Repositories.HealthPlan.Interfaces;
using JurassicPharm.Repositories.HealthPlan.Implementations;
using JurassicPharm.Services.City.Interfaces;
using JurassicPharm.Services.City.Implementations;
using JurassicPharm.Repositories.City.Interfaces;
using JurassicPharm.Repositories.City.Implementations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

/**
 * CORS Policy to allow the frontend to access the API
 * */
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});


//JWT Tokenization
builder.Services.AddScoped<JwtService>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
//Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JurassicPharm", Version = "v1" });

    // Configuraci�n para JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Introduce el token JWT aqu�. Ejemplo: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

//DB Contex
builder.Services.AddDbContext<JurassicPharmContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//DI

/*--Personnel--*/
builder.Services.AddScoped<IPersonnelService, PersonnelService>();
builder.Services.AddScoped<IPersonnelRepository, PersonnelRepository>();
/*--Invoices--*/
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
/*--Supplies--*/
builder.Services.AddScoped<ISuppliesService, SuppliesService>();
builder.Services.AddScoped<IsuppliesRepository, SuppliesRepository>();
/*--Clients--*/
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
/*--Branches--*/
builder.Services.AddScoped<IBranchesService, BranchesService>();
builder.Services.AddScoped<IBranchesRepository, BranchesRepository>();
/*--EmailSender--*/
builder.Services.AddScoped<IEmailSender, EmailSenderService>();
/*--HealthPlan--*/
builder.Services.AddScoped<IHealthPlanService, HealthPlanService>();
builder.Services.AddScoped<IHealthPlanRepository, HealthPlanRepository>();
/*--City--*/
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICityRepository, CityRepository>();


//Authorization
builder.Services.AddAuthorizationBuilder()
                   //Authorization
                   .AddPolicy("RequireAdminRole", policy => policy.RequireRole("ADMIN"))
                   //Authorization
                   .AddPolicy("RequireStockManagerRole", policy => policy.RequireRole("REPOSITOR"))
                   //Authorization
                   .AddPolicy("RequireCashierRole", policy => policy.RequireRole("CAJERO"))
                   //Authorization
                   .AddPolicy("AdminOrCashier", policy => policy.RequireRole("ADMIN", "CAJERO"));

//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("AllowFrontend");
app.UseHttpsRedirection();

app.UseAuthentication();


app.UseAuthorization();

app.MapControllers();

app.Run();
