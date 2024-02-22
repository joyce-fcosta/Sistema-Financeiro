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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using WebApi.Token;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.OpenApi.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;

internal class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(c =>
		{
			c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });

			//Ao especificar a definição de segurança, o Swagger levará em consideração que deverá adicionar o recurso de autorização.
			//Este recurso consiste em um botão “Autorizar” no topo da página que definirá o cabeçalho de autorização. 
			c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				Type = SecuritySchemeType.ApiKey,
				Scheme = "Bearer",
				BearerFormat = "JWT",
				In = ParameterLocation.Header,
				Description = string.Format("JWT Authorization header using the Bearer scheme." +
							  "\r\n\r\n Enter 'Bearer'[space] and then your token in the text input below." +
							   " \r\n\r\nExample: \"Bearer 12345abcdef"),
			});

			// O método de extensão AddSecurityRequirement adicionará um cabeçalho de autorização a cada endpoint quando a solicitação for enviada pelo swagger.
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

		builder.Services.AddDbContext<ApplicationContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

		builder.Services.AddDefaultIdentity<ApplicationUser>(option => option.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationContext>();

		builder.Services.Configure<IdentityOptions>(options =>
		{
			// Default Lockout settings.
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.AllowedForNewUsers = true;
		});

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

		builder.Services.Configure<IdentityOptions>(options =>
		{
			// Default Password settings.
			options.Password.RequireDigit = false;
			options.Password.RequireLowercase = false;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequireUppercase = false;
			options.Password.RequiredLength = 6;
			options.Password.RequiredUniqueChars = 1;
		});


		//Token JWT
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(option =>
			{
				option.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = "Teste.Security.Bearer",
					ValidAudience = "Teste.Security.Bearer",
					IssuerSigningKey = JwtSecurityKey.Create("this is my custom Secret key for authentication") //Metodo que foi criado
				};

				option.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
						return Task.CompletedTask;
					},

					OnTokenValidated = context =>
					{
						Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
						return Task.CompletedTask;
					}
				};
			});

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI(/*c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1")*/);
		}

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}