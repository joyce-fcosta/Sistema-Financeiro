using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Infra.Configuration;
using Entities.Entidades;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Generics;
using Infra.Repository.Generics;
using Domain.Interfaces;
using Infra.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDefaultIdentity<ApplicationUser>(option => option.SignIn.RequireConfirmedAccount = true);

builder.Services.AddSingleton(typeof(IGenerics<>), typeof(GenericsRepository<>));
builder.Services.AddSingleton<ICategoria, CategoriaRepository>();
builder.Services.AddSingleton<IDespesa, DespesaRepository>();
builder.Services.AddSingleton<ISistemaFinanceiro, SistemaFinanceiroRepository>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiro, UsuarioSistemaFinanceiroRepository>();

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
