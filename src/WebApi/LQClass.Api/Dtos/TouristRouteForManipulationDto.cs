using LQClass.Api.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LQClass.Api.Dtos
{
  [TouristRouteTitleMustBeDifferentFromDescription]
  public abstract class TouristRouteForManipulationDto
  {
    [Required(ErrorMessage = "Title不可为空")]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(1500)]
    public virtual string Description { get; set; }

    // 计算方式：原价 * 折扣
    public decimal Price { get; set; }

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

    public ICollection<TouristRoutePictureForCreationDto> TouristRoutePictures { get; set; }
      = new List<TouristRoutePictureForCreationDto>();
  }
}
