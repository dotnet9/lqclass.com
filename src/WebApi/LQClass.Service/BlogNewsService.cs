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
	public class BlogNewsService : BaseService<BlogNews>, IBlogNewsService
	{
		public BlogNewsService(IBlogNewsRepository blogNewsRepository)
		{
			base.baseRepository = blogNewsRepository;
		}
	}
}
