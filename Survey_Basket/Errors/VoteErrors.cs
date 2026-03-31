namespace Survey_Basket.Errors
{
	public static class VoteErrors
	{
		public static readonly Error DublicatedVote = new("Vote.DublicatedTitle", "This user has already voted before for the same poll", StatusCodes.Status409Conflict);
		public static readonly Error InvalidQuestions = new("Vote.InvalidQuestions", "invalid Questions", StatusCodes.Status400BadRequest);

	}
}
