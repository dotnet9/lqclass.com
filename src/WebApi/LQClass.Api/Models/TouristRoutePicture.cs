using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Models
{
	public class TouristRoutePicture
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[MaxLength(100)]
		public string Url { get; set; }

		[ForeignKey("TouristRouteId")]
		public Guid TouristRouteId { get; set; }
		public TouristRoute TouristRoute { get; set; }
	}
}
