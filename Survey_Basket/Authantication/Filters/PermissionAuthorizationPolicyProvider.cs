using Microsoft.Extensions.Options;

namespace Survey_Basket.Authantication.Filters
{
	public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
	{
		private readonly AuthorizationOptions _options = options.Value;
		public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
		{
			var policy = await base.GetPolicyAsync(policyName);
			if (policy != null)
				return policy;

			var PermissionPolicy = new AuthorizationPolicyBuilder()
				.AddRequirements(new PermissionRequirment(policyName))
				.Build();
			_options.AddPolicy(policyName, PermissionPolicy);

			return PermissionPolicy;


		}
	}
}
