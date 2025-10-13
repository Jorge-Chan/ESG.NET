using System.Text;
using Fiap.Web.ESG2.Data.Contexts;
using Fiap.Web.ESG2.Services; // onde estão IUsuarioService/UsuarioService e outros
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// =======================================
// Controllers
// =======================================
builder.Services.AddControllers();

// =======================================
// Swagger + JWT Bearer
// =======================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    // Suporte ao Bearer no Swagger
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Fiap.Web.ESG2 API",
        Version = "v1"
    });

    var securityScheme = new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Informe o token JWT no formato: Bearer {seu_token}",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Reference = new Microsoft.OpenApi.Models.OpenApiReference
        {
            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };

    opt.AddSecurityDefinition("Bearer", securityScheme);
    opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        { securityScheme, Array.Empty<string>() }
    });
});

#region Banco de Dados (Oracle + EF Core)
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseOracle(connectionString)
       .EnableSensitiveDataLogging(builder.Environment.IsDevelopment())
);
#endregion

#region Autenticação/Autorização (JWT)
var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = Encoding.UTF8.GetBytes(jwtSection["Key"]
    ?? throw new InvalidOperationException("Jwt:Key não configurado."));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = true;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
            ClockSkew = TimeSpan.FromMinutes(2)
        };
    });

builder.Services.AddAuthorization();
#endregion

#region DI de Serviços/Repos
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
// Adicione aqui os outros services conforme forem criados:
// builder.Services.AddScoped<IRelatorioEmissaoService, RelatorioEmissaoService>();
// builder.Services.AddScoped<IEmpresaService, EmpresaService>();
// builder.Services.AddScoped<ICompensacaoCarbonoService, CompensacaoCarbonoService>();
#endregion

var app = builder.Build();

// =======================================
// Swagger
// =======================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// =======================================
// Middleware HTTP e Auth
// =======================================
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// =======================================
// Aplicar migrações automaticamente
// =======================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    db.Database.Migrate();
}

// =======================================
// Map Controllers
// =======================================
app.MapControllers();

// =======================================
// Run
// =======================================
app.Run();
