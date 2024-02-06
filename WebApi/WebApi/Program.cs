using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Infra.Configuration;
using Entities.Entidades;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces.Generics;
using Infra.Repository.Generics;
using Domain.Interfaces;
using Infra.Repository;
using Domain.Interfaces.Services;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddDefaultIdentity<ApplicationUser>(option => option.SignIn.RequireConfirmedAccount = true);

//Repository
builder.Services.AddSingleton(typeof(IGenerics<>), typeof(GenericsRepository<>));
builder.Services.AddSingleton<ICategoria, CategoriaRepository>();
builder.Services.AddSingleton<IDespesa, DespesaRepository>();
builder.Services.AddSingleton<ISistemaFinanceiro, SistemaFinanceiroRepository>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiro, UsuarioSistemaFinanceiroRepository>();

//Services
builder.Services.AddSingleton<ICategoriaService, CategoriaService>();
builder.Services.AddSingleton<IDespesaService, DespesaService>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiroService, UsuarioSistemaFinanceiroService>();
builder.Services.AddSingleton<ISistemaFinanceiroService, SistemaFinanceiroService>();

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
