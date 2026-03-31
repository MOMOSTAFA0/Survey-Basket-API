using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace Survey_Basket.Persistance
{
	public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) :
		IdentityDbContext<ApplicationUser, ApplicationRole, string>(options)
	{
		public DbSet<Poll> polls { get; set; }
		public DbSet<Question> Questions { get; set; }
		public DbSet<Answer> Answers { get; set; }
		public DbSet<Vote> votes { get; set; }
		public DbSet<VoteAnswer> votesAnswer { get; set; }
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var CascadedFK = modelBuilder.Model.GetEntityTypes()
										   .SelectMany(M => M.GetForeignKeys())
										   .Where(FK => FK.DeleteBehavior == DeleteBehavior.Cascade && !FK.IsOwnership);
			foreach (var fk in CascadedFK)
				fk.DeleteBehavior = DeleteBehavior.Restrict;

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			base.OnModelCreating(modelBuilder);

		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			var Enteries = ChangeTracker.Entries<AuditableEntitiy>();
			foreach (var ent in Enteries)
			{
				var CurrentUserID = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
				if (ent.State == EntityState.Added)
				{
					ent.Property(x => x.CreatedById).CurrentValue = CurrentUserID!;
				}
				else if (ent.State == EntityState.Modified)
				{
					ent.Property(x => x.UpdatedyId).CurrentValue = CurrentUserID;
					ent.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;

				}
			}

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}
