using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LQClass.Api.ResourceParameters
{
	public class TouristRouteResourceParameters
	{
		public string Key { get; set; }

		public string RatingOperator { get; set; }

		public int? RatingValue { get; set; }

		private string _rating;

		/// <summary>
		/// 小于lessThan，大于largerThan，等于equalTo lessThan3，largerThan4，equalTo5
		/// </summary>
		public string Rating
		{
			get { return _rating; }
			set
			{
				if(string.IsNullOrWhiteSpace(value))
				{
					_rating = value;
					return;
				}
				Regex regex = new Regex(@"([A-Za-z0-9\-]+)(\d+)");
				Match match = regex.Match(value);
				if (match.Success)
				{
					RatingOperator = match.Groups[1].Value;
					RatingValue = int.Parse(match.Groups[2].Value);
				}
				_rating = value;
			}
		}
	}
}
