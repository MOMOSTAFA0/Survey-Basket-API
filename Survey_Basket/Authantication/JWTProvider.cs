using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Survey_Basket.Authantication
{
	public class JWTProvider : IJWTProvider
	{
		private readonly IOptions<JWTOptions> _jwtoptions;

		public JWTProvider(IOptions<JWTOptions> jwtoptions)
		{
			_jwtoptions = jwtoptions;
		}

		public (string token, int expiresIn) GenerateToken(ApplicationUser user, IEnumerable<string> roles, IEnumerable<string> permissions)
		{

			Claim[] claims = [
				new(JwtRegisteredClaimNames.Sub,user.Id),
				new(JwtRegisteredClaimNames.Email,user.Email!),
				new(JwtRegisteredClaimNames.GivenName,user.FirstName),
				new(JwtRegisteredClaimNames.FamilyName,user.LastName),
				new(nameof(roles),JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),
				new(nameof(permissions),JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray),
				new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
			];
			var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtoptions?.Value.Key!));
			var siginigCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
			var expiresIn = 30;
			var expertionDate = DateTime.UtcNow.AddMinutes(expiresIn);
			var token = new JwtSecurityToken(
				issuer: _jwtoptions?.Value.Issuer,
				audience: _jwtoptions?.Value.Audience,
				claims: claims,
				expires: expertionDate,
				signingCredentials: siginigCredentials
			);
			return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: expiresIn * 60);

		}

		public string? ValidateToken(string token)
		{
			var tokenHandeler = new JwtSecurityTokenHandler();
			var SemmatricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtoptions?.Value.Key!));
			try
			{
				tokenHandeler.ValidateToken(token, new TokenValidationParameters
				{
					IssuerSigningKey = SemmatricKey,
					ValidateIssuerSigningKey = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);
				var jwtToken = (JwtSecurityToken)validatedToken;
				return jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value;
			}
			catch
			{
				return null;
			}
		}
	}
}
