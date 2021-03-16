//using LQClass.Api.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LQClass.Api.Services
//{
//  public class MockTouristRouteRepository : ITouristRouteRepository
//  {
//    private List<TouristRoute> _routes = null;
//    public MockTouristRouteRepository()
//    {
//      if(_routes==null)
//      {
//        InitializeTouristRoutes();
//      }
//    }
//    private void InitializeTouristRoutes()
//    {
//      _routes = new List<TouristRoute>
//      {
//        new TouristRoute{
//          Id=Guid.NewGuid(),
//          Title="黄山",
//          OriginalPrice=1299,
//          Features="<p>吃住行游购娱</p>",
//          Fees="<p>交通费用自理</p>",
//          Notes="<p>小心危险</p>"
//        },
//        new TouristRoute{
//          Id=Guid.NewGuid(),
//          Title="华山",
//          OriginalPrice=1299,
//          Features="<p>吃住行游购娱</p>",
//          Fees="<p>交通费用自理</p>",
//          Notes="<p>小心危险</p>"
//        }
//      };
//    }
//    public IEnumerable<TouristRoute> GetRouristRoutes()
//    {
//      return _routes;
//    }

//    public TouristRoute GetTouristRoute(Guid touristRouteId)
//    {
//      return _routes.FirstOrDefault(n => n.Id == touristRouteId);// 3-4
//    }
//  }
//}
