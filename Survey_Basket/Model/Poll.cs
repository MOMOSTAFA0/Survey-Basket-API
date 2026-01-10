using System.ComponentModel.DataAnnotations;

namespace Survey_Basket.Model
{
	public class Poll
	{
		public int _id { get; set; }
		public string _title { get; set; }
		public string _discreotion { get; set; }
		public DateTime AuthorAge { get; set; }
		public Poll() { }
		public Poll(int _id, string _title, string _discreotion) 
		{
	      this._id= _id;
		  this._title= _title;
		  this._discreotion= _discreotion;
		}
	}
}
