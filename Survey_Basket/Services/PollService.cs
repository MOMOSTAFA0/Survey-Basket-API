using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Survey_Basket.Model;
using System.Collections.Immutable;

namespace Survey_Basket.Services
{
	public class PollService : IPollService
	{
		static List<Poll> pollList = new List<Poll>
	   {
		   new Poll(1,"kfkfkfk kfkffkkl","Law1"),
	       new Poll(2,"kfkfkfk kfkffkkl","Law2"),

	   };

		public Poll CreatPoll(Poll poll)
		{
			poll._id =pollList.Count+1;
			pollList.Add(poll);
			return poll;
		}

		public bool DeletePoll(int id)
		{
			Poll Poll= pollList.FirstOrDefault(p=>p._id==id)!;
            if (Poll!=null)
            {
				pollList.Remove(Poll);
				return true;
            }
			return false;
        }

		public IEnumerable<Poll> GetAll()=> pollList;

		public Poll GetById(int id) => pollList.FirstOrDefault(p => p._id == id);

		public bool UpdatePoll(int id,Poll poll)
		{
			var updatablePoll = pollList.FirstOrDefault(p => p._id == id);
			bool Poll = updatablePoll != null ? true : false;
			
			if (Poll)
			{
				pollList[id-1]._title = poll._title;	
				pollList[id-1]._discreotion = poll._discreotion;	
				return true;
			}
			return false;
		}

	}
}
