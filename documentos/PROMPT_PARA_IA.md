# Instrucciones para IA - Arquitectura Clean Code .NET

## Descripción General
Estas instrucciones guían a una IA para crear una arquitectura Clean Code en .NET (versión 6, 8, 9 o superior) siguiendo el patrón implementado en este proyecto. La arquitectura separa las responsabilidades en 4 capas principales con inversión de dependencias.

> **✅ Compatibilidad:** Este template está optimizado para **.NET 8/9** pero incluye instrucciones para **.NET 6** cuando sea necesario.

## Estructura de Proyecto Requerida

### 1. Crear Solución Base
```bash
dotnet new sln -n [NombreProyecto]
```

### 2. Crear Proyectos por Capa

> **Nota sobre versiones .NET:** 
> - Para .NET 6: usar `net6.0`
> - Para .NET 8: usar `net8.0` 
> - Para .NET 9: usar `net9.0`
> - Ajustar todas las versiones de paquetes NuGet según la versión de .NET utilizada

#### Capa Core (Dominio)
```bash
dotnet new classlib -n [NombreProyecto].Core -f net8.0
dotnet sln add [NombreProyecto].Core
```

#### Capa Infrastructure (Infraestructura)
```bash
dotnet new classlib -n [NombreProyecto].Infraestructura -f net8.0
dotnet sln add [NombreProyecto].Infraestructura
```

#### Capa Application (Aplicación)
```bash
dotnet new classlib -n [NombreProyecto].Application -f net8.0
dotnet sln add [NombreProyecto].Application
```

#### Capa Web API
```bash
dotnet new webapi -n [NombreProyecto].API -f net8.0
dotnet sln add [NombreProyecto].API
```

## Configuración de Dependencias

### Referencias de Proyecto
1. **Core**: Sin dependencias externas
2. **Infrastructure**: Referencia a Core
3. **Application**: Referencia a Core e Infrastructure
4. **API**: Referencia a todas las capas

```bash
# Infrastructure → Core
dotnet add [NombreProyecto].Infraestructura reference [NombreProyecto].Core

# Application → Core + Infrastructure
dotnet add [NombreProyecto].Application reference [NombreProyecto].Core
dotnet add [NombreProyecto].Application reference [NombreProyecto].Infraestructura

# API → Todas
dotnet add [NombreProyecto].API reference [NombreProyecto].Core
dotnet add [NombreProyecto].API reference [NombreProyecto].Application
dotnet add [NombreProyecto].API reference [NombreProyecto].Infraestructura
```

## Paquetes NuGet Requeridos

### API Project (.NET 8/9)
```xml
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="13.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="8.0.10" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.8.1" />
<PackageReference Include="Scalar.AspNetCore" Version="1.2.44" />
<PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.22.0" />
```

### Application Project (.NET 8/9)
```xml
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="MediatR" Version="12.4.1" />
```

### Infrastructure Project (.NET 8/9)
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.10" />
```

### Versiones para .NET 6 (Legacy)
```xml
<!-- API Project .NET 6 -->
<PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="7.0.20" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
<PackageReference Include="Scalar.AspNetCore" Version="1.2.44" />

<!-- Application Project .NET 6 -->
<PackageReference Include="AutoMapper" Version="12.0.1" />
<PackageReference Include="MediatR" Version="12.0.1" />

<!-- Infrastructure Project .NET 6 -->
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="7.0.20" />
```

## Templates de Código

### 1. Entidad Core (Domain)
```csharp
namespace [NombreProyecto].Core.Entities
{
    public class [NombreEntidad]
    {
        public int Id { get; set; }
        // Propiedades de la entidad
        
        public [NombreEntidad]()
        {
        }
        
        public [NombreEntidad](/* parámetros */)
        {
            // Asignación de propiedades
        }
        
        public void Actualizar(/* parámetros */)
        {
            // Lógica de actualización
        }
    }
}
```

### 2. DbContext (Infrastructure)
```csharp
using Microsoft.EntityFrameworkCore;
using [NombreProyecto].Core.Entities;

namespace [NombreProyecto].Infraestructura.Data
{
    public class [NombreProyecto]DbContext : DbContext
    {
        public DbSet<[NombreEntidad]> [NombreEntidad] { get; set; }
        
        public [NombreProyecto]DbContext(DbContextOptions<[NombreProyecto]DbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<[NombreEntidad]>().HasKey(e => e.Id);
        }
    }
}
```

### 3. Servicio Application
```csharp
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace [NombreProyecto].Application.Services
{
    public class [NombreEntidad]Servicio : I[NombreEntidad]Servicio
    {
        private readonly [NombreProyecto]DbContext dbContext;
        private readonly ILogger<[NombreEntidad]Servicio> logger;
        private readonly IMapper mapper;
        
        public [NombreEntidad]Servicio([NombreProyecto]DbContext dbContext, ILogger<[NombreEntidad]Servicio> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
        }
        
        // Implementar métodos CRUD
    }
}
```

### 4. Controller (Web API)
```csharp
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace [NombreProyecto].API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class [NombreEntidad]Controller : ControllerBase
    {
        private readonly I[NombreEntidad]Servicio servicio;
        private readonly ILogger<[NombreEntidad]Controller> logger;
        private readonly IMapper mapper;
        
        public [NombreEntidad]Controller(I[NombreEntidad]Servicio servicio, ILogger<[NombreEntidad]Controller> logger, IMapper mapper)
        {
            this.servicio = servicio;
            this.logger = logger;
            this.mapper = mapper;
        }
        
        // Implementar endpoints HTTP
    }
}
```

### 5. Extension Methods para DI
```csharp
// ApplicationExtensions.cs
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<I[NombreEntidad]Servicio, [NombreEntidad]Servicio>();
            return services;
        }
    }
}

// InfraestructureExtensions.cs
namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfraestructureExtensions
    {
        public static IServiceCollection AddInfraestructuraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<[NombreProyecto]DbContext>(options => 
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddAutoMapper(typeof([NombreEntidad]Profile).Assembly)
                .AddHealthChecks();
            
            return services;
        }
    }
}
```

## Configuración Program.cs
```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfraestructuraServices(builder.Configuration);

var app = builder.Build();

// Configurar documentación API - Swagger para desarrolladores y Scalar para clientes
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
        c.RoutePrefix = "swagger"; // Swagger UI disponible en /swagger
        c.DocumentTitle = "[NombreProyecto] API - Documentación para Desarrolladores";
    });
}

// Scalar UI para clientes - disponible en todas las configuraciones
app.UseSwagger();
app.MapScalarApiReference(options =>
{
    options.Title = "[NombreProyecto] API";
    options.RoutePrefix = "docs"; // Scalar disponible en /docs
    options.DefaultHttpClient = Scalar.AspNetCore.ScalarTarget.JavaScript;
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseHealthChecks("/healtz");

app.Run();
```

## Patrones a Seguir

### 1. DTOs (Data Transfer Objects)
- Crear DTOs para entrada (Create/Update)
- Crear DTOs para salida (Get/List)
- Usar AutoMapper para conversiones

### 2. Manejo de Errores
- Usar try-catch en servicios
- Log de errores apropiado
- Retornar excepciones específicas

### 3. Paginación
- Implementar Request/Response objects para listados
- Usar Skip/Take para paginación
- Agregar headers de paginación en respuesta

### 4. Logging
- Inyectar ILogger en todos los servicios
- Log de operaciones importantes
- Log de errores con detalles

### 5. Async/Await
- Usar async/await en todas las operaciones I/O
- Pasar CancellationToken en métodos async

## Documentación API - Swagger + Scalar

### Configuración de Endpoints
- **Swagger UI** (`/swagger`): Interfaz técnica para desarrolladores
  - Incluye herramientas de testing
  - Documentación detallada de schemas
  - Disponible solo en Development

- **Scalar** (`/docs`): Interfaz moderna para clientes
  - Diseño limpio y profesional
  - Ideal para documentación pública
  - Disponible en todos los entornos

### Personalización adicional (Opcional)
```csharp
// En InfraestructureExtensions.cs
public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
{
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "[NombreProyecto] API", 
            Version = "v1",
            Description = "API para gestión de [descripción]",
            Contact = new OpenApiContact
            {
                Name = "Equipo de Desarrollo",
                Email = "dev@empresa.com"
            }
        });
        
        // Configuración JWT en Swagger
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
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
                new string[] {}
            }
        });
    });
    
    return services;
}
```

## Configuraciones Adicionales

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=[NombreBaseDatos];Trusted_Connection=true;TrustServerCertificate=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information"
    }
  }
}
```

### Localization (Opcional)
- Configurar cultura por defecto
- Configurar RequestLocalization

## Testing

### 1. Crear Proyectos de Testing
```bash
# Unit Tests
dotnet new xunit -n [NombreProyecto].Tests.Unit
dotnet sln add [NombreProyecto].Tests.Unit

# Integration Tests
dotnet new xunit -n [NombreProyecto].Tests.Integration
dotnet sln add [NombreProyecto].Tests.Integration
```

### 2. Paquetes NuGet para Tests
#### Unit Tests (.NET 8/9)
```xml
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
<PackageReference Include="xunit" Version="2.9.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
<PackageReference Include="Moq" Version="4.20.72" />
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.10" />
```

#### Integration Tests (.NET 8/9)
```xml
<PackageReference Include="Microsoft.AspNetCore.Mvc.Testing" Version="8.0.10" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.10" />
<PackageReference Include="Testcontainers.SqlServer" Version="3.11.0" />
```

### 3. Template Unit Test para Servicio
```csharp
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using [NombreProyecto].Application.Services;
using [NombreProyecto].Core.Entities;
using [NombreProyecto].Infraestructura.Data;

namespace [NombreProyecto].Tests.Unit.Services
{
    public class [NombreEntidad]ServicioTests : IDisposable
    {
        private readonly [NombreProyecto]DbContext _context;
        private readonly Mock<ILogger<[NombreEntidad]Servicio>> _loggerMock;
        private readonly IMapper _mapper;
        private readonly [NombreEntidad]Servicio _service;

        public [NombreEntidad]ServicioTests()
        {
            var options = new DbContextOptionsBuilder<[NombreProyecto]DbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            
            _context = new [NombreProyecto]DbContext(options);
            _loggerMock = new Mock<ILogger<[NombreEntidad]Servicio>>();
            
            var config = new MapperConfiguration(cfg => cfg.AddProfile<[NombreEntidad]Profile>());
            _mapper = config.CreateMapper();
            
            _service = new [NombreEntidad]Servicio(_context, _loggerMock.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_DeberiaCrear[NombreEntidad]_CuandoDatosValidos()
        {
            // Arrange
            var dto = new [NombreEntidad]CreateDto { /* propiedades */ };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await _service.CreateAsync(dto, cancellationToken);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            
            var entityInDb = await _context.[NombreEntidad].FindAsync(result.Id);
            Assert.NotNull(entityInDb);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
```

### 4. Template Integration Test
```csharp
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using [NombreProyecto].API;
using [NombreProyecto].Application.DTO.[NombreEntidad]s;

namespace [NombreProyecto].Tests.Integration.Controllers
{
    public class [NombreEntidad]ControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public [NombreEntidad]ControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<[NombreProyecto]DbContext>));
                    if (descriptor != null) services.Remove(descriptor);

                    services.AddDbContext<[NombreProyecto]DbContext>(options =>
                        options.UseInMemoryDatabase("InMemoryDbForTesting"));
                });
            });
            
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Post_DeberiaCrear[NombreEntidad]_CuandoDatosValidos()
        {
            // Arrange
            var dto = new [NombreEntidad]CreateDto { /* propiedades */ };

            // Act
            var response = await _client.PostAsJsonAsync("/api/[nombreentidad]", dto);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<[NombreEntidad]Dto>();
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }
    }
}
```

### 5. Comandos para Ejecutar Tests
```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar solo unit tests
dotnet test [NombreProyecto].Tests.Unit

# Ejecutar con coverage
dotnet test --collect:"XPlat Code Coverage"
```

## Autenticación JWT

### 1. Paquetes NuGet Adicionales para API
```xml
<!-- Para .NET 8/9 -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.10" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="8.1.2" />

<!-- Para .NET 6 -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="7.0.20" />
<PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.1.2" />
```

### 2. Configuración JWT en appsettings.json
```json
{
  "JwtSettings": {
    "SecretKey": "tu-clave-secreta-muy-larga-y-segura-aqui",
    "Issuer": "https://tu-dominio.com",
    "Audience": "https://tu-dominio.com",
    "ExpirationMinutes": 60
  }
}
```

### 3. Modelo de Configuración JWT
```csharp
namespace [NombreProyecto].API.Configuration
{
    public class JwtSettings
    {
        public string SecretKey { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; }
    }
}
```

### 4. Servicio de Autenticación
```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace [NombreProyecto].Application.Services
{
    public interface IAuthService
    {
        Task<string> GenerateJwtTokenAsync(string userId, string email, List<string> roles);
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }

    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        
        public AuthService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> GenerateJwtTokenAsync(string userId, string email, List<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, userId),
                new(ClaimTypes.Email, email)
            };
            
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> ValidateCredentialsAsync(string email, string password)
        {
            // Implementar validación de credenciales
            return await Task.FromResult(true);
        }
    }
}
```

### 5. Configuración JWT en Program.cs
```csharp
// Configurar JWT
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(jwtSettings);

var key = Encoding.ASCII.GetBytes(jwtSettings.Get<JwtSettings>()!.SecretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Get<JwtSettings>()!.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Get<JwtSettings>()!.Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();
builder.Services.AddScoped<IAuthService, AuthService>();
```

### 6. Controller de Autenticación
```csharp
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        if (await _authService.ValidateCredentialsAsync(request.Email, request.Password))
        {
            var token = await _authService.GenerateJwtTokenAsync("1", request.Email, new List<string> { "User" });
            return Ok(new LoginResponse { Token = token });
        }
        
        return Unauthorized();
    }
}

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
}
```

### 7. Uso en Controllers
```csharp
[Authorize] // Requiere autenticación
[Authorize(Roles = "Admin")] // Requiere rol específico
[Route("api/[controller]")]
[ApiController]
public class [NombreEntidad]Controller : ControllerBase
{
    // Endpoints protegidos
}
```

## Dockerización

### 1. Dockerfile para API
```dockerfile
# Dockerfile para .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Para .NET 6 usar:
# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["[NombreProyecto].API/[NombreProyecto].API.csproj", "[NombreProyecto].API/"]
COPY ["[NombreProyecto].Application/[NombreProyecto].Application.csproj", "[NombreProyecto].Application/"]
COPY ["[NombreProyecto].Core/[NombreProyecto].Core.csproj", "[NombreProyecto].Core/"]
COPY ["[NombreProyecto].Infraestructura/[NombreProyecto].Infraestructura.csproj", "[NombreProyecto].Infraestructura/"]

RUN dotnet restore "[NombreProyecto].API/[NombreProyecto].API.csproj"
COPY . .
WORKDIR "/src/[NombreProyecto].API"
RUN dotnet build "[NombreProyecto].API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "[NombreProyecto].API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "[NombreProyecto].API.dll"]
```

### 2. docker-compose.yml
```yaml
version: '3.8'

services:
  api:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:80"
      - "8443:443"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=[NombreBaseDatos];User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=true;
    depends_on:
      - sqlserver
    networks:
      - app-network

  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
      - MSSQL_PID=Express
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql
    networks:
      - app-network

volumes:
  sqlserver_data:

networks:
  app-network:
    driver: bridge
```

### 3. .dockerignore
```
**/.dockerignore
**/.env
**/.git
**/.gitignore
**/.project
**/.settings
**/.toolstarget
**/.vs
**/.vscode
**/.idea
**/*.*proj.user
**/*.dbmdl
**/*.jfm
**/azds.yaml
**/bin
**/charts
**/docker-compose*
**/Dockerfile*
**/node_modules
**/npm-debug.log
**/obj
**/secrets.dev.yaml
**/values.dev.yaml
LICENSE
README.md
```

### 4. Comandos Docker
```bash
# Construir imagen
docker build -t [nombreproyecto]-api .

# Ejecutar con docker-compose
docker-compose up -d

# Ver logs
docker-compose logs -f api

# Parar servicios
docker-compose down
```

## Entity Framework Migrations

### 1. Instalación de Tools
```bash
# Global
dotnet tool install --global dotnet-ef

# Local (recomendado)
dotnet new tool-manifest
dotnet tool install dotnet-ef
```

### 2. Comandos de Migrations
```bash
# Crear primera migración
dotnet ef migrations add InitialCreate --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API

# Crear migración adicional
dotnet ef migrations add [NombreMigracion] --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API

# Actualizar base de datos
dotnet ef database update --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API

# Ver historial de migraciones
dotnet ef migrations list --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API

# Revertir migración
dotnet ef database update [NombreMigracionAnterior] --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API

# Remover última migración (solo si no se ha aplicado)
dotnet ef migrations remove --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API

# Generar script SQL
dotnet ef migrations script --project [NombreProyecto].Infraestructura --startup-project [NombreProyecto].API
```

### 3. Configuración de Migrations en Program.cs
```csharp
// Aplicar migraciones automáticamente en desarrollo
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<[NombreProyecto]DbContext>();
    context.Database.Migrate();
}
```

### 4. Seeding de Datos
```csharp
// En DbContext
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
    
    // Seed data
    modelBuilder.Entity<[NombreEntidad]>().HasData(
        new [NombreEntidad] { Id = 1, /* propiedades */ },
        new [NombreEntidad] { Id = 2, /* propiedades */ }
    );
}
```

## Validaciones y Mejores Prácticas

1. **Nullable Reference Types**: Habilitar en todos los proyectos
2. **ImplicitUsings**: Habilitar para menos verbosidad
3. **Health Checks**: Configurar endpoint de salud
4. **CORS**: Configurar políticas según necesidades
5. **API Documentation**: Configurar Swagger UI (/swagger) para desarrolladores y Scalar (/docs) para clientes
6. **AutoMapper Profiles**: Crear perfiles específicos por entidad
7. **Testing**: Mantener cobertura > 80%
8. **Security**: Nunca hardcodear secrets en código
9. **Logging**: Log de operaciones críticas y errores
10. **Migrations**: Siempre revisar scripts antes de aplicar en producción
