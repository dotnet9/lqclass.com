using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.Api.Models
{
  public class TouristRoute
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    /// <summary>
    /// 原价
    /// </summary>
    public decimal OriginalPrice { get; set; }
    /// <summary>
    /// 折扣
    /// </summary>
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
    public string Features { get; set; }
    /// <summary>
    /// 费用说明
    /// </summary>
    public string Fees { get; set; }
    public string Notes { get; set; }
    public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }
  }
}
