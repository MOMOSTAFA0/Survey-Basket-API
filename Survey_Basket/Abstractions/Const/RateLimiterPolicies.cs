namespace Survey_Basket.Abstractions.Const
{
	public static class RateLimiterPolicies
	{
		public const string ConcurrancyLimit = "Concurrency";
		public const string BucketLimit = "Bucket";
		public const string FixedWindowLimit = "FixedWindow";
		public const string SlidingWindowLimit = "SlidingWindow";
		public const string IPLimit = "IPLimit";
		public const string UserLimit = "UserLimit";
	}
}
