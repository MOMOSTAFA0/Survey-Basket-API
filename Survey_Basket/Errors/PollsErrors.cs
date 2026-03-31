namespace Survey_Basket.Errors
{
	public static class PollsErrors
	{
		public static readonly Error Empty = new("Polls.Empty", Description: "Polls Entity is Empty", StatusCodes.Status404NotFound);
		public static readonly Error NotFound = new("Polls.NOtFound", "Poll record not found by givine id", StatusCodes.Status404NotFound);
		public static readonly Error InvalidData = new("Polls.InvalidData", "invalid or uncompleated uploaded Data", StatusCodes.Status400BadRequest);
		public static readonly Error DublicatedTitle = new("Polls.DublicatedTitle", "Another poll with the same title is already exists", StatusCodes.Status409Conflict);
	}
}
