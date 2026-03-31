namespace Survey_Basket.Errors
{
	public static class QuestionErrors
	{
		public static readonly Error DublicatedQuestionContent = new("Question.DublicatedTitle", "Another Question with the same content is already exists", StatusCodes.Status409Conflict);
		public static readonly Error QuestionNotFound = new("Question.QuestionNotFound", "this Question Record Not Found with givin id", StatusCodes.Status404NotFound);

	}
}
