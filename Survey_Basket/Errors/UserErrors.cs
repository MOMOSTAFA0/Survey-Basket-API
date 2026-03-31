namespace Survey_Basket.Errors
{
	public static class UserErrors
	{
		public static readonly Error InvalidCredintials = new("User.InvalidCredintials", "Invalid Email/Password", StatusCodes.Status401Unauthorized);
		public static readonly Error DisabledUser = new("User.DisabledUser", "Disabled User, Pleases Contact Your Author", StatusCodes.Status401Unauthorized);
		public static readonly Error SignInLocked = new("User.SignInLocked", "SignIn is Locked, Pleases Contact Your Author", StatusCodes.Status401Unauthorized);
		public static readonly Error InvalidToken = new("User.InvalidToken", "this Token is Invalid or Expired", StatusCodes.Status400BadRequest);
		public static readonly Error UserNotFound = new("User.NotFound", "User not found by gevin ID", StatusCodes.Status404NotFound);
		public static readonly Error RefreshTokenNotFound = new("User.RefreshTokenNotFound", "RefreshToken NotFound or Expired", StatusCodes.Status400BadRequest);
		public static readonly Error DuplicatedEmail = new("User.DuplicatedEmail", "Another user with the same email is already existed", StatusCodes.Status409Conflict);
		public static readonly Error EmailNotConfirmed = new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status400BadRequest);
		public static readonly Error InvaildCode = new("User.InvaildCode", "invalid Code", StatusCodes.Status400BadRequest);
		public static readonly Error DuplicatedEmailConfirmation = new("User.DuplicatedEmailConfirmation", "this email is confirmed before", StatusCodes.Status409Conflict);



	}
}
