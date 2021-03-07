using LQClass.IRepository;
using LQClass.IService;
using LQClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.Service
{
	public class TypeInfoService : BaseService<TypeInfo>, ITypeInfoService
	{
		public TypeInfoService(ITypeInfoRepository typeInfoRepository)
		{
			base.baseRepository = typeInfoRepository;
		}
	}
}
