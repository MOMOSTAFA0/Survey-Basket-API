using HangfireBasicAuthenticationFilter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

namespace Survey_Basket
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddDependencies(builder.Configuration);

			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Host.UseSerilog((context, configuration) =>
			{
				configuration.ReadFrom.Configuration(context.Configuration);
			});

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
				app.UseHangfireDashboard();
			}
			app.UseHangfireDashboard("/jobs", new DashboardOptions
			{
				Authorization =
				[
					new HangfireCustomBasicAuthenticationFilter
					{
						User=app.Configuration.GetValue<string>("HangFireSettings:UserName"),
						Pass=app.Configuration.GetValue<string>("HangFireSettings:Password")
					}
				]
			});

			var ScobeFactory = app.Services.GetService<IServiceScopeFactory>();
			using var scope = ScobeFactory?.CreateScope();
			var NotificationService = scope?.ServiceProvider.GetRequiredService<INotefecationService>();

			RecurringJob.AddOrUpdate("SendNewPollNotification", () => NotificationService!.SendPollNotificationsAsync(null), Cron.Daily());

			app.UseSerilogRequestLogging();
			app.UseExceptionHandler();

			app.UseHttpsRedirection();
			app.UseCors();
			app.UseRateLimiter();
			app.UseAuthorization();

			app.MapControllers();
			app.MapHealthChecks("health", new HealthCheckOptions
			{
				ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
			});
			app.Run();
		}
	}
}
