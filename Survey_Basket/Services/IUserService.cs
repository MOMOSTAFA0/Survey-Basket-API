namespace Survey_Basket.Services
{
	public interface IUserService
	{
		public Task<Result<UserProfileResponse>> GetUserDataAsync(string UsertId, CancellationToken cancellationToken);
		public Task<Result> UpdateUserProfileAsync(UpdateUserProfileRequest request, string UserID, CancellationToken cancellationToken);
		public Task<Result> UpdateUserEmailAsync(UpdatEmailRequest request, string UserID, CancellationToken cancellationToken);
		public Task<Result> ChangePasswordAsync(string UserID, ChangePasswordRequest request, CancellationToken cancellationToken);
		public Task<List<UserResponse>> GEtAllUsersAsync();
		public Task<Result<UserResponse>> GetUserDetailAsync(string Id);
		public Task<Result<UserResponse>> AddUserAsync(CreateUserRequest request, CancellationToken cancellationToken);
		public Task<Result> AddPasswordToUser(AddUserPasswordRequist requist, CancellationToken cancellationToken);
		public Task<Result> UpdateUserAsync(string id, UpdateUserRequestRequest request, CancellationToken cancellationToken);
		public Task<Result> ToggleStatusAsync(string userID, CancellationToken cancellationToken);
		public Task<Result> UnLockUserAsync(string UserID, CancellationToken cancellationToken);


	}
}
