using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Models
{
  public class LineItem
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("TouristRouteId")]
    public Guid TouristRouteId { get; set; }
    public TouristRoute TouristRoute { get; set; }

    public Guid? ShoppingCardId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }

    [Range(0.0, 1.0)]
    public double? DiscountPresent { get; set; }
  }
}
