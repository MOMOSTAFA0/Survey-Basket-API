using FluentValidation;
using Mapster;
using MapsterMapper;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Survey_Basket.Services;
using System.Reflection;

namespace Survey_Basket
{
	public static class Dependency_Injection
	{
		public static IServiceCollection AddDependencies(this IServiceCollection services)
		{
			// Add services to the container.
			services.AddControllers();
			services.AddSwager();
			//Add Mapper
			services.AddMapper();
			//Add Validator
			services.AddFluentValidator();

			services.AddScoped<IPollService, PollService>();

			return services;

		}
		private static IServiceCollection AddSwager(this IServiceCollection services)
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
			return services;
		}	
		private static IServiceCollection AddMapper(this IServiceCollection services)
		{
			var Mapper = TypeAdapterConfig.GlobalSettings;
			Mapper.Scan(Assembly.GetExecutingAssembly());
			services.AddSingleton<IMapper>(new Mapper(Mapper));

			return services;
		}
		private static IServiceCollection AddFluentValidator(this IServiceCollection services)
		{
			services
					.AddFluentValidationAutoValidation()
					.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}
