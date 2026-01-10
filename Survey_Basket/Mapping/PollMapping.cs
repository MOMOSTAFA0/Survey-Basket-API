using Mapster;
using MapsterMapper;
using Survey_Basket.Contracts.Requests;
using Survey_Basket.Contracts.Responses;
using Survey_Basket.Model;

namespace Survey_Basket.Mapping
{
	public class PollMapping : IRegister
	{
		public void Register(TypeAdapterConfig config)
		{
			config.NewConfig<Poll, PollResponse>().Map(dest => dest._discreption, Src => Src._discreotion);
			
		}
	}
}
 