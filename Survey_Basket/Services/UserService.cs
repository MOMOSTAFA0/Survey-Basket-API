namespace Survey_Basket.Services
{
	public class UserService(

		UserManager<ApplicationUser> userManager,
		IEmailSender emailSender,
		IHttpContextAccessor httpContextAccessor,
		ILogger<UserService> logger,
		ApplicationDbContext Context,
		IRoleService roleService,
		IAuthService authService
		) : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly IEmailSender _emailSender = emailSender;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly ILogger _logger = logger;
		private readonly ApplicationDbContext _context = Context;
		private readonly IRoleService _roleService = roleService;
		private readonly IAuthService _authService = authService;
		public async Task<Result<UserProfileResponse>> GetUserDataAsync(string UsertId, CancellationToken cancellationToken)
		{
			var User = await _userManager.Users.Where(U => U.Id == UsertId)
				.Select(U => new UserProfileResponse(
					U.UserName!,
					U.Email!,
					U.FirstName,
					U.LastName
				))
				.SingleAsync();

			return Result.Success(User);
		}
		public async Task<List<UserResponse>> GEtAllUsersAsync()
		{
			var users = await (from u in _context.Users.AsNoTracking()
							   join UR in _context.UserRoles.AsNoTracking()
							   on u.Id equals UR.UserId
							   join R in _context.Roles.AsNoTracking()
							   on UR.RoleId equals R.Id
							   where R.Name != DefaultRole.Member
							   group R by new
							   {
								   Id = u.Id,
								   FirstName = u.FirstName,
								   LastName = u.LastName,
								   Email = u.Email,
								   IsDesabled = u.IsDesabled
							   } into Users
							   select new UserResponse(
								   Users.Key.Id,
								   Users.Key.FirstName,
								   Users.Key.LastName,
								   Users.Key.Email,
								   Users.Key.IsDesabled,
								   Users.Select(R => R.Name).ToList()!
							   )).ToListAsync();
			return users;
		}
		public async Task<Result<UserResponse>> GetUserDetailAsync(string Id)
		{
			if (await _userManager.FindByIdAsync(Id) is not { } user)
			{
				return Result.Failure<UserResponse>(UserErrors.UserNotFound);
			}
			var userRoles = await _userManager.GetRolesAsync(user);
			var Response = new UserResponse
			(
				user.Id,
				user.FirstName,
				user.LastName,
				user.Email!,
				user.IsDesabled,
				userRoles
			);
			return Result.Success(Response);
		}
		public async Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
		{
			var EmailIsExisted = await _context.Users.AnyAsync(U => U.Email == request.Email);

			if (EmailIsExisted)
				return Result.Failure<UserResponse>(UserErrors.DuplicatedEmail);

			var AllowedRoles = await _roleService.GetAllAsync(cancellationToken);
			var RoleNames = AllowedRoles.Select(R => R.Name).ToList();
			if (request.Roles.Except(RoleNames).Any())
				return Result.Failure<UserResponse>(RoleErrors.InvalidRoles);

			var user = new ApplicationUser
			{
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				UserName = request.Email
			};

			var Creatingresult = await _userManager.CreateAsync(user);

			if (Creatingresult.Succeeded)
			{
				await _userManager.AddToRolesAsync(user, request.Roles);
				var Response = new UserResponse(
					user.Id,
					user.FirstName,
					user.LastName,
					user.Email,
					user.IsDesabled,
					request.Roles
				);
				var code = await EmailProvider.GenerateEmailVervicationCode(user, _userManager);
				_logger.LogInformation("Configuration Code:{ code}", code);
				var Origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
				await EmailProvider.SendEmailAsync(user,
					_userManager,
					_emailSender,
					"EmailConfirmation",
					Origin,
					"auth/emailConfirmation",
					code,
					"Survey Basket:Email Confirmation"
				);
				return Result.Success(Response);
			}
			var Error = Creatingresult.Errors.First();

			return Result.Failure<UserResponse>(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<Result> UpdateUserAsync(string id, UpdateUserRequestRequest request, CancellationToken cancellationToken)
		{
			var EmailIsExisted = await _context.Users.AnyAsync(U => U.Email == request.Email && U.Id != id);

			if (EmailIsExisted)
				return Result.Failure(UserErrors.DuplicatedEmail);

			var AllowedRoles = await _roleService.GetAllAsync(cancellationToken);

			if (request.UserRoles.Except(AllowedRoles.Select(R => R.Name)).Any())
				return Result.Failure(RoleErrors.InvalidRoles);


			if (await _userManager.FindByIdAsync(id) is not { } user)
				return Result.Failure<UserResponse>(UserErrors.UserNotFound);

			user.FirstName = request.FirstName;
			user.LastName = request.Lastname;
			user.Email = request.Email;
			user.NormalizedEmail = request.Email.ToUpper();
			user.UserName = request.Email;
			user.NormalizedUserName = request.Email.ToUpper();

			var Updatingresult = await _userManager.UpdateAsync(user);

			if (Updatingresult.Succeeded)
			{
				await _context.UserRoles.Where(u => u.UserId == user.Id).ExecuteDeleteAsync(cancellationToken);
				await _userManager.AddToRolesAsync(user, request.UserRoles);
				return Result.Success();
			}
			var Error = Updatingresult.Errors.First();

			return Result.Failure<UserResponse>(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<Result> AddPasswordToUser(AddUserPasswordRequist requist, CancellationToken cancellationToken)
		{
			if (await _userManager.FindByIdAsync(requist.id) is not { } user)
				return Result.Failure(UserErrors.UserNotFound);

			var EmailIsConfirmed = await _authService.ConfirmEmailAsync(new EmailConfirmationRequest(requist.id, requist.Code), cancellationToken);

			if (EmailIsConfirmed.IsFailure)
				return Result.Failure(UserErrors.EmailNotConfirmed);

			var AddingPasswordResult = await _userManager.AddPasswordAsync(user, requist.Password);

			if (AddingPasswordResult.Succeeded)
				return Result.Success();

			var Error = AddingPasswordResult.Errors.First();
			return Result.Failure(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));

		}
		public async Task<Result> UpdateUserProfileAsync(UpdateUserProfileRequest request, string UserID, CancellationToken cancellationToken)
		{
			//var user=await _userManager.FindByIdAsync(UserID);
			//user=request.Adapt(user);
			//await _userManager.UpdateAsync(user!);
			await _userManager.Users
				.Where(u => u.Id == UserID)
				.ExecuteUpdateAsync(setters =>

				  setters
				 .SetProperty(u => u.FirstName, request.FirstName)
				 .SetProperty(u => u.LastName, request.LastName)

				);
			return Result.Success();
		}
		public async Task<Result> UpdateUserEmailAsync(UpdatEmailRequest request, string UserID, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(UserID);

			bool isEmailExisted = await _userManager.Users.AnyAsync(u => u.Email == request.Email);

			if (isEmailExisted)
				return Result.Failure(UserErrors.DuplicatedEmail);

			user = request.Adapt(user);
			user.EmailConfirmed = false;
			await _userManager.UpdateAsync(user!);


			var code = await EmailProvider.GenerateEmailVervicationCode(user, _userManager);
			logger.LogInformation("Confirmation code: {code}", code);


			var origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

			await EmailProvider.SendEmailAsync(user,
				_userManager,
				_emailSender,
				"EmailConfirmation",
				origin,
				"auth/emailConfirmation",
				code,
				"Survey Basket:Email Confirmation"
			);

			return Result.Success();
		}
		public async Task<Result> ChangePasswordAsync(string UserID, ChangePasswordRequest request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(UserID);
			var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword, request.NewPassword);
			if (result.Succeeded)
				return Result.Success();
			var Error = result.Errors.FirstOrDefault();
			return Result.Failure(new Error(Error!.Code, Error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<Result> ToggleStatusAsync(string userID, CancellationToken cancellationToken)
		{
			if (await _userManager.FindByIdAsync(userID) is not { } user)
				return Result.Failure(UserErrors.UserNotFound);

			user.IsDesabled = !user.IsDesabled;
			await _userManager.UpdateAsync(user);
			return Result.Success();
		}
		public async Task<Result> UnLockUserAsync(string UserID, CancellationToken cancellationToken)
		{
			if (await _userManager.FindByIdAsync(UserID) is not { } user)
				return Result.Failure(UserErrors.UserNotFound);
			user.LockoutEnd = null;
			await _userManager.SetLockoutEndDateAsync(user, null);
			return Result.Success();
		}
	}
}
