using Survey_Basket.Contracts.Answers;
using System.Linq.Dynamic.Core;

namespace Survey_Basket.Services
{
	public class QuestionService(ApplicationDbContext context, ICacheService cacheService, ILogger<QuestionService> logger) : IQuestionService
	{
		public readonly ApplicationDbContext _context = context;
		private readonly ICacheService _cacheService = cacheService;
		private readonly ILogger<QuestionService> _logger = logger;
		private readonly string _cashPrifix = "availableQuestions";
		public async Task<Result<PagginatedList<QuestionResponse>>> GetAllAsync(int PollID, Filters filters, CancellationToken cancellationToken)
		{
			var PollIsExitsts = await _context.polls.AnyAsync(p => p.Id == PollID, cancellationToken);
			if (!PollIsExitsts)
				return Result.Failure<PagginatedList<QuestionResponse>>(PollsErrors.NotFound);

			var Query = _context.Questions
				.Where(Q => Q.PollID == PollID && (string.IsNullOrEmpty(filters.SerarchValue) || Q.Content.Contains(filters.SerarchValue)))
				.OrderBy($"{filters.SortColumn}{filters.SortDirection}")
				.Include(Q => Q.Answers)
				.Select(Q => new QuestionResponse(
						Q.Id,
						Q.Content,
						Q.Answers.Where(A => A.IsActive).Select(A => new AnswerResponse(A.Id, A.Content))
				))
				.AsNoTracking();

			var Questions = await PagginatedList<QuestionResponse>.CreatePage(Query, filters.pageNumber, filters.pageSize, cancellationToken);

			return Result.Success(Questions);

		}
		public async Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int PollID, string UserID, CancellationToken cancellationToken)
		{
			var HasVote = await _context.votes.AnyAsync(V => V.PollID == PollID && V.UserID == UserID, cancellationToken);
			if (HasVote)
				return Result.Failure<IEnumerable<QuestionResponse>>(VoteErrors.DublicatedVote);

			var pollIsExisted = await _context.polls.AnyAsync(P => P.Id == PollID
																   && P.IsPublished
																   && P.StartAt <= DateOnly.FromDateTime(DateTime.UtcNow)
																   && P.EndAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);
			if (!pollIsExisted)
				return Result.Failure<IEnumerable<QuestionResponse>>(PollsErrors.NotFound);

			var cashkey = $"{_cashPrifix}-{PollID}";
			var CashedQuestions = await _cacheService.GetAsync<IEnumerable<QuestionResponse>>(cashkey, cancellationToken);
			IEnumerable<QuestionResponse> Questions = [];
			if (CashedQuestions is null)
			{
				_logger.LogInformation($"Adding new Cash within key {cashkey} From Data Base");
				Questions = await _context.Questions.Where(Q => Q.PollID == PollID && Q.IsActive)
												   .Include(Q => Q.Answers)
												   .Select(Q => new QuestionResponse(
													Q.Id,
													Q.Content,
													Q.Answers.Where(A => A.IsActive)
															.Select(A => new AnswerResponse(
																A.Id,
																A.Content
															))
												   )).AsNoTracking()
													 .ToListAsync(cancellationToken);

				await _cacheService.SetAsync(cashkey, Questions, cancellationToken);
			}
			else
			{
				_logger.LogInformation($"return Cach within key {cashkey}");
				Questions = CashedQuestions;
			}

			return Result.Success(Questions);
		}
		public async Task<Result<QuestionResponse>> CreateAsync(int PollID, QuestionRequest request, CancellationToken cancellationToken)
		{
			var PollIsExitsts = await _context.polls.AnyAsync(p => p.Id == PollID, cancellationToken);
			if (!PollIsExitsts)
				return Result.Failure<QuestionResponse>(PollsErrors.NotFound);

			var QuestionIsDuplicated = await _context.Questions.AnyAsync(Q => Q.Content == request.Content && Q.PollID == PollID, cancellationToken);
			if (QuestionIsDuplicated)
				return Result.Failure<QuestionResponse>(QuestionErrors.DublicatedQuestionContent);

			var question = request.Adapt<Question>();
			question.PollID = PollID;


			await _context.AddAsync(question, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation($"removing cash within key {_cashPrifix}-{PollID} in adding new questions ");

			await _cacheService.RemoveAsync($"{_cashPrifix}-{PollID}", cancellationToken);
			return Result.Success(question.Adapt<QuestionResponse>());
		}
		public async Task<Result<QuestionResponse>> GetAsync(int PollID, int QuestionID, CancellationToken cancellationToken)
		{
			var PollIsExitsts = await _context.polls.AnyAsync(p => p.Id == PollID, cancellationToken);
			if (!PollIsExitsts)
				return Result.Failure<QuestionResponse>(PollsErrors.NotFound);
			var Question = await _context.Questions.Where(Q => Q.PollID == PollID && Q.Id == QuestionID)
												   .Include(Q => Q.Answers)
												   .Select(Q => new QuestionResponse(
														   Q.Id,
														   Q.Content,
														   Q.Answers.Select(A => new AnswerResponse(A.Id, A.Content))
												   )).SingleOrDefaultAsync(cancellationToken);
			if (Question == null)
			{
				return Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound);
			}
			return Result.Success(Question);
		}
		public async Task<Result> ToggleStatusAsync(int PollID, int QuestionID, CancellationToken cancellationToken)
		{
			var Question = await _context.Questions.SingleOrDefaultAsync(Q => Q.Id == QuestionID && Q.PollID == PollID, cancellationToken);
			if (Question == null)
			{
				return Result.Failure(QuestionErrors.QuestionNotFound);
			}
			Question.IsActive = !Question.IsActive;
			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation($"removing cash within key {_cashPrifix}-{PollID} in ToggleStatuse ");

			await _cacheService.RemoveAsync($"{_cashPrifix}-{PollID}", cancellationToken);

			return Result.Success();
		}
		public async Task<Result> UpdateAsync(int PollID, int QuestionID, QuestionRequest request, CancellationToken cancellationToken)
		{
			var QuestionExistance = await _context.Questions.
				AnyAsync(Q => Q.PollID == PollID
				&& Q.Id != QuestionID
				&& Q.Content == request.Content);

			if (QuestionExistance)
				return Result.Failure(QuestionErrors.DublicatedQuestionContent);

			var Question = await _context.Questions.Where(Q => Q.PollID == PollID && Q.Id == QuestionID)
															   .Include(Q => Q.Answers)
															   .SingleOrDefaultAsync(cancellationToken);
			if (Question is null)
				return Result.Failure(QuestionErrors.QuestionNotFound);

			Question.Content = request.Content;

			//current answer
			var CurrentAnswers = Question.Answers.Select(Q => Q.Content).ToList();
			//Add newAnswer
			var newAnswers = request.Answers.Except(CurrentAnswers).ToList();
			foreach (var answer in newAnswers)
				Question.Answers.Add(new Answer { Content = answer });
			//Dactivate Answers
			foreach (var answer in Question.Answers)
				answer.IsActive = request.Answers.Contains(answer.Content);

			await _context.SaveChangesAsync(cancellationToken);
			_logger.LogInformation($"removing cash within key {_cashPrifix}-{PollID} in Updating questions ");

			await _cacheService.RemoveAsync($"{_cashPrifix}-{PollID}", cancellationToken);

			return Result.Success();
		}
	}
}
