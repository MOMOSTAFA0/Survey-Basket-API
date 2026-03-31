namespace Survey_Basket.Abstractions
{
	public class PagginatedList<T>(List<T> itmes, int pageNumber, int count, int PageSize)
	{
		public List<T> Items { get; private set; } = itmes;
		public int PageNumber { get; private set; } = pageNumber;
		public int TotalPages { get; private set; } = (int)Math.Ceiling(count / (double)PageSize);
		public bool HavePriviousePage => PageNumber > 1;
		public bool HaveNextPage => PageNumber < TotalPages;

		public static async Task<PagginatedList<T>> CreatePage(IQueryable<T> Source, int pageNumber, int pageSize, CancellationToken cancellationToken)
		{
			var skippages = (pageNumber - 1) * pageSize;
			var Count = await Source.CountAsync(cancellationToken);
			var Items = await Source.Skip(skippages).Take(pageSize).ToListAsync(cancellationToken);
			return new PagginatedList<T>(Items, pageNumber, Count, pageSize);
		}

	}
}
