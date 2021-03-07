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
	public class WriterInfoService : BaseService<WriterInfo>, IWriterInfoService
	{
		public WriterInfoService(IWriterInfoRepository writerInfoRepository)
		{
			base.baseRepository = writerInfoRepository;
		}
	}
}
