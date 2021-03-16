using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Models
{
	public class TouristRoute
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Title { get; set; }

		[Required]
		[MaxLength(1500)]
		public string Description { get; set; }

		/// <summary>
		/// 原价
		/// </summary>
		[Column(TypeName = "decimal(18, 2)")]
		public decimal OriginalPrice { get; set; }

		/// <summary>
		/// 折扣
		/// </summary>
		[Range(0.0, 1.0)]
		public double? DiscountPresent { get; set; }

		public DateTime CreateTime { get; set; }
		public DateTime? UpdateTime { get; set; }
		/// <summary>
		/// 出发时间
		/// </summary>
		public DateTime? DepartureTime { get; set; }

		/// <summary>
		/// 卖点介绍
		/// </summary>
		[MaxLength]
		public string Features { get; set; }
		/// <summary>
		/// 费用说明
		/// </summary>
		[MaxLength]
		public string Fees { get; set; }
		[MaxLength]
		public string Notes { get; set; }
		public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
			= new List<TouristRoutePicture>();

    public TravelDays? TravelDays { get; set; }

    public TripType? TripType { get; set; }

    public DepartureCity? DepartureCity { get; set; }
  }
}
