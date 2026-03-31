namespace Survey_Basket.Contracts.Common
{
	public class Filters
	{
		public int pageNumber { get; init; } = 1;
		public int pageSize { get; init; } = 10;
		public string? SerarchValue { get; set; }
		public string? SortColumn { get; init; }
		public string? SortDirection { get; init; } = "ASC";
	}
}
