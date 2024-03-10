using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
	public static class ApplicationServiceExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<DataContext>(opt =>
			{
				opt.UseSqlite(config.GetConnectionString("DefaultConnection")); // takes a configuration connection string
			});
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddCors();
			services.AddScoped<ITokenService, TokenService>(); // standard for http request
															   //builder.Services.AddSingleton(); // alive until app shuts down 
			return services;
		}
	}
}