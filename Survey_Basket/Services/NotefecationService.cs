namespace Survey_Basket.Services
{
	public class NotefecationService(ApplicationDbContext Context, UserManager<ApplicationUser> UserManager, IHttpContextAccessor HttpContextAccessor, IEmailSender EmailSender) : INotefecationService
	{
		private readonly ApplicationDbContext _context = Context;
		private readonly UserManager<ApplicationUser> _usermanager = UserManager;
		private readonly IHttpContextAccessor _httpContextAccessor = HttpContextAccessor;
		private readonly IEmailSender _emailSender = EmailSender;

		public async Task SendPollNotificationsAsync(int? PollId = null)
		{
			IEnumerable<Poll> polls = [];
			if (PollId.HasValue)
			{
				var poll = await _context.polls.SingleOrDefaultAsync(p => p.Id == PollId && p.IsPublished);
				polls = [poll!];
			}
			else
			{
				polls = await _context.polls.Where(p => p.IsPublished && p.StartAt == DateOnly.FromDateTime(DateTime.UtcNow))
					.AsNoTracking()
					.ToListAsync();
			}
			var users = await _usermanager.GetUsersInRoleAsync(DefaultRole.Member);
			var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
			foreach (var pol in polls)
			{
				foreach (var user in users)
				{
					var PlaceHolders = new Dictionary<string, string>
					{
						{"{{name}}",$"{user.FirstName} {user.LastName}"},
						{"{{pollTill}}",pol.Title},
						{"{{endDate}}",pol.EndAt.ToString()},
						{"{{url}}",$"{origin}/polls/start/{pol.Id}"},
					};
					var Body = EmailBodyBuilder.GenerateEmailBody("PollNotification", PlaceHolders);

					await _emailSender.SendEmailAsync(user.Email, $"📌Survey Basket : New Poll - {pol.Title}", Body);

				}
			}
		}
	}
}
