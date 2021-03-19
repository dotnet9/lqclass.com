using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Dtos
{
	public class TouristRouteDto
	{
		public Guid Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		// 计算方式：原价 * 折扣
		public decimal Price { get; set; }
		public decimal OriginalPrice { get; set; }
		public decimal? DiscountPresent { get; set; }

		public DateTime CreateTime { get; set; }

		public DateTime? UpdateTime { get; set; }

		public DateTime? DepartureTime { get; set; }

		public string Features { get; set; }

		public string Fees { get; set; }

		public string Notes { get; set; }

		public double? Rating { get; set; }

		public string TravelDays { get; set; }

		public string TripType { get; set; }

		public string DepartureCity { get; set; }

		public ICollection<TouristRoutePictureDto> TouristRoutePictures { get; set; }
	}
}
