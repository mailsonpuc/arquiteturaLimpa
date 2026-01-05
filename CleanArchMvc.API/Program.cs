using System.Reflection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using CleanArchMvc.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

// Register infrastructure services (repositories, application services, automapper, identity, etc.)
builder.Services.AddInfrastructure(builder.Configuration);

#region Swagger

builder.Services.AddSwaggerGen(static c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Shoop API - Clean Architecture",
        Version = "v1",
        Description = @"O projeto Shoop é uma <b>API RESTful</b> de e-commerce desenvolvida em <b>ASP.NET Core (Net 9.0)</b>, 
        <b>seguindo os princípios de Arquitetura Limpa (Clean Architecture)</b>. 
        <br>
        O sistema é modularizado em <b>camadas claras</b>,
        focando na separação de responsabilidades e na manutenibilidade.",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://t.me/mailsonssv")
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://example.com/license")
        },
    });

    // BLOCO XML 
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    // ----------------------------------------------------

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.",
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
#endregion swagger



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Shoop API v1");
    options.RoutePrefix = ""; // <-- Swagger at the root
    options.InjectStylesheet("/custom.css"); // load directly from wwwroot
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
