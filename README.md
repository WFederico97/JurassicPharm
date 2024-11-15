JURASSIC PHARM
Proyecto de TPI para Programacion II de Tecnicatura Universitaria en Programacion de la UTN - Regional Cordoba
## Alumnos
* Alonso, Iker
* Rinaudo, Julian
* Pastran Verde, Joaquin
* Wuthrich, Federico

El proyecto JurassicPharm está organizado en dos componentes principales:

Backend: Implementado en C#, maneja la lógica del negocio y la interacción con la base de datos.
Frontend: Desarrollado con tecnologías web estándar como HTML, CSS y JavaScript, proporciona la interfaz de usuario.

## Estructura del Proyecto
```bash
TPI_V2/
├── Backend/
│   ├── JurassicPharm/
│   │   ├── appsettings.Development.json
│   │   ├── appsettings.json
│   │   ├── Controllers/
│   │   │   ├── Authentication/
│   │   │   │   ├── AuthController.cs
│   │   │   ├── Branches/
│   │   │   │   ├── BranchesController.cs
│   │   │   ├── Clients/
│   │   │   │   ├── ClientController.cs
│   │   │   ├── Invoices/
│   │   │   │   ├── InvoicesController.cs
│   │   │   ├── Personnel/
│   │   │   │   ├── PersonnelController.cs
│   │   │   ├── Supplies/
│   │   │   │   ├── SuppliesController.cs
│   │   ├── DTO/
│   │   │   ├── Branch/
│   │   │   │   ├── BranchDTO.cs
│   │   │   ├── Cities/
│   │   │   │   ├── GetCityDTO.cs
│   │   │   │   ├── GetCitySummaryDTO.cs
│   │   │   ├── Clients/
│   │   │   │   ├── ClientResponseDTO.cs
│   │   │   │   ├── CreateClientDTO.cs
│   │   │   │   ├── DiscountClientDTO.cs
│   │   │   │   ├── UpdateClientDTO.cs
│   │   │   ├── InvoIce/
│   │   │   │   ├── BillingReportDTO.cs
│   │   │   │   ├── InvoiceCreateDTO.cs
│   │   │   │   ├── InvoiceResponseDTO.cs
│   │   │   │   ├── InvoiceUpdateDTO.cs
│   │   │   │   ├── TopSuppliersDTO.cs
│   │   │   ├── InvoiceDetail/
│   │   │   │   ├── InvoiceDetailDTO.cs
│   │   │   │   ├── InvoiceDetailResponseDTO.cs
│   │   │   ├── Personnel/
│   │   │   │   ├── CreatePersonnelDTO.cs
│   │   │   │   ├── DeletePersonnelDTO.cs
│   │   │   │   ├── GetPersonnelDTO.cs
│   │   │   │   ├── GetPersonnelSummaryDTO.cs
│   │   │   │   ├── LoginPersonnelDTO.cs
│   │   │   │   ├── ResetEmployeePassword.cs
│   │   │   │   ├── UpdatePersonnelDTO.cs
│   │   │   ├── Stores/
│   │   │   │   ├── GetStoreDTO.cs
│   │   │   ├── Supplies/
│   │   │   │   ├── CreateSupplyDTO.cs
│   │   │   │   ├── GetMedBrandDTO.cs
│   │   │   │   ├── GetSupplyDistributionDTO.cs
│   │   │   │   ├── GetSupplyDTO.cs
│   │   │   │   ├── SelectOptionDTO.cs
│   │   │   │   ├── UpdateSupplyDTO.cs
│   │   ├── e.IdCiudadNavigation)
│   │   ├── efpt.config.json
│   │   ├── efpt.config.json.user
│   │   ├── JurassicPharm.csproj
│   │   ├── JurassicPharm.csproj.user
│   │   ├── JurassicPharm.http
│   │   ├── JurassicPharm.sln
│   │   ├── Migrations/
│   │   │   ├── 20241112021736_InitialMigration.cs
│   │   │   ├── 20241112021736_InitialMigration.Designer.cs
│   │   │   ├── 20241114033209_UpdateViewFacturacionSuministroAnual.cs
│   │   │   ├── 20241114033209_UpdateViewFacturacionSuministroAnual.Designer.cs
│   │   │   ├── JurassicPharmContextModelSnapshot.cs
│   │   ├── Models/
│   │   │   ├── Ciudad.cs
│   │   │   ├── Cliente.cs
│   │   │   ├── DbContextExtensions.cs
│   │   │   ├── DetalleFactura.cs
│   │   │   ├── DetalleReceta.cs
│   │   │   ├── Empleado.cs
│   │   │   ├── Factura.cs
│   │   │   ├── IJurassicPharmContextProcedures.cs
│   │   │   ├── JurassicPharmContext.cs
│   │   │   ├── JurassicPharmContext.Functions.cs
│   │   │   ├── JurassicPharmContextProcedures.cs
│   │   │   ├── Localidad.cs
│   │   │   ├── Marca.cs
│   │   │   ├── Medico.cs
│   │   │   ├── ObraSocial.cs
│   │   │   ├── Proveedor.cs
│   │   │   ├── Provincia.cs
│   │   │   ├── Receta.cs
│   │   │   ├── SP_TOP_PROVEEDORES_ENTREGASResult.cs
│   │   │   ├── Stock.cs
│   │   │   ├── Sucursal.cs
│   │   │   ├── Suministro.cs
│   │   │   ├── TipoDistribucion.cs
│   │   │   ├── TipoSuministro.cs
│   │   │   ├── ViewFacturacionPorAnio.cs
│   │   ├── obj/
│   │   │   ├── Debug/
│   │   │   │   ├── net8.0/
│   │   │   │   │   ├── .NETCoreApp,Version=v8.0.AssemblyAttributes.cs
│   │   │   │   │   ├── ApiEndpoints.json
│   │   │   │   │   ├── apphost.exe
│   │   │   │   │   ├── EndpointInfo/
│   │   │   │   │   │   ├── JurassicPharm.json
│   │   │   │   │   │   ├── JurassicPharm.OpenApiFiles.cache
│   │   │   │   │   ├── Jurassic.EA689D71.Up2Date
│   │   │   │   │   ├── JurassicPharm.AssemblyInfo.cs
│   │   │   │   │   ├── JurassicPharm.AssemblyInfoInputs.cache
│   │   │   │   │   ├── JurassicPharm.assets.cache
│   │   │   │   │   ├── JurassicPharm.csproj.AssemblyReference.cache
│   │   │   │   │   ├── JurassicPharm.csproj.BuildWithSkipAnalyzers
│   │   │   │   │   ├── JurassicPharm.csproj.CoreCompileInputs.cache
│   │   │   │   │   ├── JurassicPharm.csproj.FileListAbsolute.txt
│   │   │   │   │   ├── JurassicPharm.dll
│   │   │   │   │   ├── JurassicPharm.GeneratedMSBuildEditorConfig.editorconfig
│   │   │   │   │   ├── JurassicPharm.genruntimeconfig.cache
│   │   │   │   │   ├── JurassicPharm.GlobalUsings.g.cs
│   │   │   │   │   ├── JurassicPharm.MvcApplicationPartsAssemblyInfo.cache
│   │   │   │   │   ├── JurassicPharm.MvcApplicationPartsAssemblyInfo.cs
│   │   │   │   │   ├── JurassicPharm.pdb
│   │   │   │   │   ├── JurassicPharm.sourcelink.json
│   │   │   │   │   ├── ref/
│   │   │   │   │   │   ├── JurassicPharm.dll
│   │   │   │   │   ├── refint/
│   │   │   │   │   │   ├── JurassicPharm.dll
│   │   │   │   │   ├── staticwebassets/
│   │   │   │   │   │   ├── msbuild.build.JurassicPharm.props
│   │   │   │   │   │   ├── msbuild.buildMultiTargeting.JurassicPharm.props
│   │   │   │   │   │   ├── msbuild.buildTransitive.JurassicPharm.props
│   │   │   │   │   ├── staticwebassets.build.endpoints.json
│   │   │   │   │   ├── staticwebassets.build.json
│   │   │   │   │   ├── staticwebassets.references.upToDateCheck.txt
│   │   │   │   │   ├── staticwebassets.removed.txt
│   │   │   ├── JurassicPharm.csproj.EntityFrameworkCore.targets
│   │   │   ├── JurassicPharm.csproj.nuget.dgspec.json
│   │   │   ├── JurassicPharm.csproj.nuget.g.props
│   │   │   ├── JurassicPharm.csproj.nuget.g.targets
│   │   │   ├── project.assets.json
│   │   │   ├── project.nuget.cache
│   │   ├── Program.cs
│   │   ├── Properties/
│   │   │   ├── launchSettings.json
│   │   ├── Repositories/
│   │   │   ├── Branches/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── BranchesRepository.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IBranchesRepository.cs
│   │   │   ├── Clients/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── ClientRepository.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IClientRepository.cs
│   │   │   ├── Exceptions/
│   │   │   │   ├── InvalidEmailException.cs
│   │   │   │   ├── InvalidPropertyException.cs
│   │   │   │   ├── NotFoundException.cs
│   │   │   ├── Invoices/
│   │   │   │   ├── IInvoiceRepository.cs
│   │   │   │   ├── InvoiceRepository.cs
│   │   │   ├── Personnel/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── PersonnelRepository.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IPersonnelRepository.cs
│   │   │   ├── Supplies/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── SuppliesRepository.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── ISuppliesRepository.cs
│   │   ├── Services/
│   │   │   ├── Branches/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── BranchesService.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IBranchesService.cs
│   │   │   ├── Clients/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── ClientService.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IClientService.cs
│   │   │   ├── EmailSenderService/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── EmailSenderService.cs
│   │   │   │   ├── Interface/
│   │   │   │   │   ├── IEmailSender.cs
│   │   │   ├── Invoices/
│   │   │   │   ├── IInvoiceService.cs
│   │   │   │   ├── InvoiceService.cs
│   │   │   ├── JWT/
│   │   │   │   ├── JwtService.cs
│   │   │   ├── Personnel/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── PersonnelService.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── IPersonnelService.cs
│   │   │   ├── Supplies/
│   │   │   │   ├── Implementations/
│   │   │   │   │   ├── SuppliesService.cs
│   │   │   │   ├── Interfaces/
│   │   │   │   │   ├── ISuppliesService.cs
├── Frontend/
│   ├── Assets/
│   │   ├── Images/
│   │   │   ├── bob_400.webp
│   │   │   ├── bob_500.webp
│   │   │   ├── bob_error.webp
│   │   │   ├── bob_ok.webp
│   │   │   ├── bob_success.webp
│   │   │   ├── bob_ups.webp
│   │   │   ├── chart-icon.png
│   │   │   ├── database-icon.png
│   │   │   ├── fw-profile.jpg
│   │   │   ├── ia-profile.jpg
│   │   │   ├── jp-profile.jpg
│   │   │   ├── jph-icon.jpg
│   │   │   ├── jph-logo.jpg
│   │   │   ├── jr-profile.jpg
│   │   │   ├── under-construction-icon.png
│   │   │   ├── user-icon.png
│   ├── index.html
│   ├── Javascript/
│   │   ├── Auth/
│   │   │   ├── auth.js
│   │   ├── Clients/
│   │   │   ├── clients.js
│   │   ├── Dashboard/
│   │   │   ├── dashboard.js
│   │   ├── Employees/
│   │   │   ├── employees.js
│   │   ├── helpers/
│   │   │   ├── setSelectedOption.js
│   │   │   ├── showAlert.js
│   │   ├── index.js
│   │   ├── Invoices/
│   │   │   ├── invoices.js
│   │   ├── login.js
│   │   ├── modules/
│   │   │   ├── Auth/
│   │   │   │   ├── api.js
│   │   │   ├── Branches/
│   │   │   │   ├── api.js
│   │   │   ├── Clients/
│   │   │   │   ├── api.js
│   │   │   ├── Invoices/
│   │   │   │   ├── api.js
│   │   │   │   ├── generateDetails.js
│   │   │   │   ├── generateTable.js
│   │   │   ├── Supplies/
│   │   │   │   ├── api.js
│   │   ├── Supplies/
│   │   │   ├── supplies.js
│   ├── package-lock.json
│   ├── package.json
│   ├── Pages/
│   │   ├── about.html
│   │   ├── clientes.html
│   │   ├── empleados.html
│   │   ├── facturas.html
│   │   ├── login.html
│   │   ├── reset-password.html
│   │   ├── suministros.html
│   ├── styles/
│   │   ├── index.css
│   │   ├── login.css
│   │   ├── reset-password.css
```

## Lenguajes Usados
* Backend: C# , .NET , Entity Framework
* Frontend: Js, HTML5, CSS3

## Patrones de Diseño Aplicados
* Singleton
* Repository
* MVC
## Dependencias y Herramientas
* .NET
* Entity Framework
* Bootstrap
* Chart Js
## Configuracion y Despliegue
1. Clonar el repositorio
   ```bash
     git clone https://github.com/WFederico97/JurassicPharm.git
   ```
2. Backend:
  * Navegar al directorio ``Backend/JurassicPharm/``
  * Restaurar Dependencias:
      ```bash
        dotnet restore
      ```
  * Ejecutar la aplicacion:
      ```bash
        dotnet run
      ```
3. Frontend
  * Se recomienda utilizar la extension live server de visual studio [live server url](https://marketplace.visualstudio.com/items?itemName=ritwickdey.LiveServer)
