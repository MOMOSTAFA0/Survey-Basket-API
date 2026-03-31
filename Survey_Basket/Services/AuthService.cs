namespace Survey_Basket.Services
{
	public class AuthService(
		UserManager<ApplicationUser> usermanger,
		IJWTProvider jWTProvider,
		ILogger<AuthService> logger,
		SignInManager<ApplicationUser> signInManager,
		IEmailSender emailSender,
		IHttpContextAccessor httpContextAccessor,
		ApplicationDbContext context) : IAuthService
	{
		private readonly UserManager<ApplicationUser>? _userManager = usermanger;
		private readonly IJWTProvider? _jwtProvider = jWTProvider;
		private readonly int _RefreshTokenExpiryDate = 14;
		private readonly ILogger<AuthService> _logger = logger;
		private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
		private readonly IEmailSender _emailSender = emailSender;
		private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
		private readonly ApplicationDbContext _context = context;


		public async Task<Result<AuthResponse>> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken)
		{

			if (await _userManager!.FindByEmailAsync(Email) is not { } user)
				return Result.Failure<AuthResponse>(UserErrors.InvalidCredintials);

			if (user.IsDesabled)
				return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

			var PasswordValidation = await _signInManager.PasswordSignInAsync(user, Password, false, true);

			if (!PasswordValidation.Succeeded)
				return Result.Failure<AuthResponse>(PasswordValidation.IsNotAllowed
					? UserErrors.EmailNotConfirmed
					: PasswordValidation.IsLockedOut ?
					UserErrors.SignInLocked :
					UserErrors.InvalidCredintials
				);

			var Response = await GenerateToken(user);

			return Result.Success(Response);

		}
		public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken)
		{
			var userID = _jwtProvider?.ValidateToken(Token);
			if (userID is null)
				return Result.Failure<AuthResponse>(UserErrors.InvalidToken);

			if (await _userManager!.FindByIdAsync(userID) is not { } user)
				return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

			if (user.IsDesabled)
				return Result.Failure<AuthResponse>(UserErrors.DisabledUser);

			var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);
			if (UserRefreshToken == null)
				return Result.Failure<AuthResponse>(UserErrors.RefreshTokenNotFound);

			UserRefreshToken.RevokedOn = DateTime.UtcNow;

			var Response = await GenerateToken(user);
			return Result.Success(Response);

		}
		private static string GenerateRefreshToken()
		{
			return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
		}

		public async Task<Result> RegisterAsync(RegistrationRequest request, CancellationToken cancellationToken)
		{
			var emailExists = await _userManager.Users.AnyAsync(U => U.Email == request.Email, cancellationToken);
			if (emailExists)
				return Result.Failure(UserErrors.DuplicatedEmail);

			var User = new ApplicationUser
			{
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				UserName = request.Email
			};
			var result = await _userManager.CreateAsync(User, request.Password);
			if (!result.Succeeded)
			{
				var Error = result.Errors.FirstOrDefault();
				return Result.Failure(new Error(Error.Code, Error.Description, StatusCodes.Status400BadRequest));
			}

			var code = await EmailProvider.GenerateEmailVervicationCode(User, _userManager);
			_logger.LogInformation("Configuration Code:{code}", code);

			await SendConfirmationEmail(User, code);


			return Result.Success();
		}
		public async Task<Result> ResendConfirmationEmailAsync(ResentConfirmationEmailRequest request, CancellationToken cancellationToken)
		{
			if (await _userManager!.FindByEmailAsync(request.Email) is not { } user)
				return Result.Success();

			if (user.EmailConfirmed)
				return Result.Failure(UserErrors.DuplicatedEmailConfirmation);
			var code = await EmailProvider.GenerateEmailVervicationCode(user, _userManager);
			await SendConfirmationEmail(user, code);
			return Result.Success();

		}

		public async Task<Result> ConfirmEmailAsync(EmailConfirmationRequest request, CancellationToken cancellationToken)
		{
			if (await _userManager!.FindByIdAsync(request.UserID) is not { } user)
				return Result.Failure(UserErrors.InvaildCode);

			if (user.EmailConfirmed)
				return Result.Failure(UserErrors.DuplicatedEmailConfirmation);

			var code = request.Code;
			try
			{
				code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
			}
			catch (FormatException)
			{
				return Result.Failure(UserErrors.InvaildCode);
			}

			var result = await _userManager.ConfirmEmailAsync(user, code);
			if (result.Succeeded)
			{
				await _userManager.AddToRoleAsync(user, DefaultRole.Admin);
				return Result.Success();
			}
			var error = result.Errors.FirstOrDefault();
			return Result.Failure(new Error(error!.Code, error.Description, StatusCodes.Status400BadRequest));
		}
		public async Task<Result> SendResetPasswordCodeAsync(ForgetPasswordRequest request, CancellationToken cancellationToken)
		{
			if (await _userManager!.FindByEmailAsync(request.Email) is not { } user)
				return Result.Success();// to trick the hacker

			if (!user.EmailConfirmed)
				return Result.Failure(UserErrors.EmailNotConfirmed);

			var code = await _userManager!.GeneratePasswordResetTokenAsync(user);
			code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
			_logger.LogInformation("Reset password code: {code}", code);

			var Code = await EmailProvider.GenerateEmailVervicationCode(user, _userManager);

			StringValues? origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
			await EmailProvider.SendEmailAsync(user,
				_userManager!,
				_emailSender,
				"ForgetPassword",
				origin,
				"auth/ForgetPassword",
				Code,
				"Survey Basket:ForgetPassword"
			);
			return Result.Success();

		}
		public async Task<Result> ResetPasswordAsync(ResetPassword_Request request, CancellationToken cancellationToken)
		{
			if (await _userManager!.FindByEmailAsync(request.Email) is not { } user)
				return Result.Success();//to trick the hacker

			IdentityResult result;

			try
			{
				var Code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
				result = await _userManager.ResetPasswordAsync(user, Code, request.NewPassword);
			}
			catch (FormatException ex)
			{
				result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
			}
			if (result.Succeeded)
				return Result.Success();
			var ResetPasswordError = result.Errors.First();

			return Result.Failure(new Error(ResetPasswordError.Code, ResetPasswordError.Description, StatusCodes.Status401Unauthorized));


		}
		public async Task<Result> RevokeRefreshTokenAsync(string token, string RefreshToken, CancellationToken cancellationToken)
		{
			var userID = _jwtProvider?.ValidateToken(token);
			if (userID is null)
				return Result.Failure(UserErrors.InvalidToken);

			var user = await _userManager!.FindByIdAsync(userID);

			if (user == null)
				return Result.Failure(UserErrors.UserNotFound);

			var UserRefreshToken = user.RefreshTokens.SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);
			if (UserRefreshToken == null)
				return Result.Failure(UserErrors.RefreshTokenNotFound);

			UserRefreshToken.RevokedOn = DateTime.UtcNow;
			await _userManager.UpdateAsync(user);
			return Result.Success();
		}

		private async Task<AuthResponse> GenerateToken(ApplicationUser user)
		{
			var (userRoles, Permissions) = await GetUserRolesANDPermissions(user);
			var tokent = _jwtProvider!.GenerateToken(user, userRoles, Permissions);
			var refreshtoken = GenerateRefreshToken();
			var ExpiryDate = DateTime.UtcNow.AddDays(_RefreshTokenExpiryDate);

			user.RefreshTokens.Add(new RefreshToken
			{
				Token = refreshtoken,
				ExpiresOn = ExpiryDate
			});
			await _userManager!.UpdateAsync(user);

			return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, tokent.token,
									tokent.expiresIn, refreshtoken, ExpiryDate);

		}

		private async Task SendConfirmationEmail(ApplicationUser User, string Code)
		{

			StringValues? origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;
			await EmailProvider.SendEmailAsync(User,
				_userManager!,
				_emailSender,
				"EmailConfirmation",
				origin,
				"auth/emailConfirmation",
				Code,
				"Survey Basket:Email Confirmation"
			);
			await Task.CompletedTask;

		}
		private async Task<(IEnumerable<string>, IEnumerable<string>)> GetUserRolesANDPermissions(ApplicationUser User)
		{
			var userRole = await _userManager!.GetRolesAsync(User);

			var RolesAndPermissions = await (from R in _context.Roles
											 join UR in _context.RoleClaims
											 on R.Id equals UR.RoleId
											 where userRole.Contains(R.Name!)
											 select UR.ClaimValue!)
										  .Distinct()
										  .ToListAsync();

			return (userRole, RolesAndPermissions);
		}
	}
}
