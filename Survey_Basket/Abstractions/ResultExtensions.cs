namespace Survey_Basket.Abstractions
{
	public static class ResultExtensions
	{
		public static ObjectResult ToProblem(this Result result)
		{
			if (result.IsSuccess)
				throw new InvalidOperationException("Cannot Convert Success result to a problem ");

			var Problem = Results.Problem(statusCode: result.Error.StatusCode);
			var Problemdetails = Problem.GetType().GetProperty(nameof(ProblemDetails))!.GetValue(Problem) as ProblemDetails;
			Problemdetails!.Extensions = new Dictionary<string, object?>
			{
				{
					"errors",new[]{ new
					{
						result.Error.Code,
						result.Error.Description
					} }
				}
			};
			return new ObjectResult(Problemdetails);
		}
	}
}
