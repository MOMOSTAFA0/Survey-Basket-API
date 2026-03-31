namespace Survey_Basket.Abstractions.Const.SeedingData
{
	public static class Permissions
	{
		public static string Type { get; } = "permissions";

		public const string GetPolls = "Polls:Read";
		public const string AddPolls = "Polls:Add";
		public const string UpdatePolls = "Polls:Update";
		public const string DeletePolls = "Polls:Delete";

		public const string GetQuestions = "Questions:Read";
		public const string AddQuestions = "Questions:Add";
		public const string UpdateQuestions = "Questions:Update";

		public const string GetUsers = "Accounts:Read";
		public const string AddUsers = "Accounts:Add";
		public const string updateUsers = "Accounts:Update";

		public const string GetRoles = "Roles:Read";
		public const string AddRoles = "Roles:Add";
		public const string updateRoles = "Roles:Update";

		public const string GetResults = "Results:Read";

		public static List<string?> GetAllPermissions() =>
			typeof(Permissions).GetFields().Select(F => F.GetValue(F) as string).ToList();
	}
}
