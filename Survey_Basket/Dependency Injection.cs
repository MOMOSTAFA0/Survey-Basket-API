using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Survey_Basket.Health;
using System.Reflection;
using System.Threading.RateLimiting;
namespace Survey_Basket
{
	public static class Dependency_Injection
	{
		public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
		{
			// Add services to the container.
			services.AddControllers();
			services.AddDatabase(configuration);
			services.AddSwager();
			//Add Mapper
			services.AddMapper();
			//Add Validator
			services.AddFluentValidator();

			services.AddDistributedMemoryCache();

			services.AddScoped<IPollService, PollService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IQuestionService, QuestionService>();
			services.AddScoped<IVoteService, VoteService>();
			services.AddScoped<IResultService, ResultService>();
			services.AddScoped<ICacheService, CacheService>();
			services.AddScoped<IEmailSender, EmailService>();
			services.AddScoped<INotefecationService, NotefecationService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRoleService, RoleService>();

			services.AddIdentity(configuration);
			services.AddExceptionHandler<GlobalExeptionHandeler>();
			services.AddProblemDetails();
			services.AddHttpContextAccessor();
			services.AddHangFireJobs(configuration);
			services.AddHangfireServer();
			services.AddHealthChecksForAllAppDebendencies(configuration);
			services.AddRateLimmiterOptions();
			services.AddMailSettings();
			return services;

		}
		private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
		{
			var configurations = configuration.GetConnectionString("DefaultConnection") ??
								throw new InvalidOperationException("Connetion string 'DefaultConnection' not found.");
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configurations));

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
			services.AddFluentValidationAutoValidation()
					.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			return services;
		}
		private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddIdentity<ApplicationUser, ApplicationRole>()
					.AddEntityFrameworkStores<ApplicationDbContext>()
					.AddDefaultTokenProviders();
			services.AddSingleton<IJWTProvider, JWTProvider>();

			//services.Configure<JWTOptions>(configuration.GetSection(JWTOptions.JWTSection));
			services.AddOptions<JWTOptions>()
					.BindConfiguration(JWTOptions.JWTSection)
					.ValidateDataAnnotations()
					.ValidateOnStart();

			var JWTSettings = configuration.GetSection(JWTOptions.JWTSection).Get<JWTOptions>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(o =>
			{
				o.SaveToken = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings?.Key!)),
					ValidIssuer = JWTSettings?.Issuer,
					ValidAudience = JWTSettings?.Audience
				};
			});
			services.Configure<IdentityOptions>(options =>
			{
				// Default Password settings.
				options.Password.RequiredLength = 6;
				options.SignIn.RequireConfirmedEmail = true;
				options.User.RequireUniqueEmail = true;

			});

			services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandeler>();
			services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

			return services;
		}
		private static IServiceCollection AddCORSOptions(this IServiceCollection services, IConfiguration configuration)
		{
			var AllowedOrigin = configuration.GetSection("Allowed Origins").Get<string[]>();
			services.AddCors(options =>
							 options.AddDefaultPolicy(builder =>
													  builder.AllowAnyHeader()
															 .AllowAnyMethod()
															 .WithOrigins(AllowedOrigin!)
							 ));
			return services;
		}
		private static IServiceCollection AddHangFireJobs(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHangfire(conf => conf
		   .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
		   .UseSimpleAssemblyNameTypeSerializer()
		   .UseRecommendedSerializerSettings()
		   .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));
			return services;
		}
		private static IServiceCollection AddHealthChecksForAllAppDebendencies(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddHealthChecks()
				.AddSqlServer(name: "Database", connectionString: configuration.GetConnectionString("DefaultConnection")!, tags: ["DataBase"])
				.AddHangfire(options =>
				{
					options.MinimumAvailableServers = 1;
				})
				.AddCheck<MailProviderHealthChecks>("Mail Provider", tags: ["Mail Service"]);
			return services;
		}
		private static IServiceCollection AddRateLimmiterOptions(this IServiceCollection services)
		{
			services.AddRateLimiter(RateLimmiterOptions =>
			{
				RateLimmiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
				RateLimmiterOptions.AddConcurrencyLimiter(RateLimiterPolicies.ConcurrancyLimit, Options =>
				{
					Options.QueueLimit = 100;
					Options.PermitLimit = 1000;
					Options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;

				});
				RateLimmiterOptions.AddPolicy(RateLimiterPolicies.IPLimit, HttpContext =>

				  RateLimitPartition.GetFixedWindowLimiter(
				  partitionKey: HttpContext.Connection.RemoteIpAddress?.ToString(),
				  factory: _ => new FixedWindowRateLimiterOptions
				  {
					  PermitLimit = 5,
					  Window = TimeSpan.FromSeconds(20),

				  })
				);

				RateLimmiterOptions.AddPolicy(RateLimiterPolicies.UserLimit, HttpContext =>
					 RateLimitPartition.GetFixedWindowLimiter(
						 partitionKey: HttpContext.User.Identity?.Name?.ToString(),
						 factory: _ => new FixedWindowRateLimiterOptions
						 {
							 PermitLimit = 1,
							 Window = TimeSpan.FromSeconds(20)
						 }
					 )
				);
			});
			return services;
		}
		private static IServiceCollection AddMailSettings(this IServiceCollection services)
		{
			services.AddOptions<MailSettings>()
			.BindConfiguration(nameof(MailSettings))
			.ValidateDataAnnotations()
			.ValidateOnStart();
			return services;
		}
	}
}