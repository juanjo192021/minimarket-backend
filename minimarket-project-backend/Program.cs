using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using tienda_project_backend.Dtos.Marca;
using tienda_project_backend.Models;
using tienda_project_backend.Services;
using tienda_project_backend.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1) Configuracion para la conexion de la BD creada en sql

builder.Services.AddDbContext<DbMinimarketContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL")));

// 2) Establecer las reglas cors para poder configurar la api desde cualquier dominio

var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// 5) Inyección de dependencia 

builder.Services.AddScoped<ICategoria, CategoriaService>();
builder.Services.AddScoped<IMarca, MarcaService>();
builder.Services.AddScoped<IProducto, ProductoService>();

// 6) Inyección de AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//7) Agregar todos validaciones que configuremos en la clase Program
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(misReglasCors);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
