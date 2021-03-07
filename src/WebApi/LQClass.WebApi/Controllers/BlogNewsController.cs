using LQClass.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LQClass.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogNewsController : ControllerBase
	{
		private readonly IBlogNewsService blogNewsService;
		public BlogNewsController(IBlogNewsService blogNewsService)
		{
			this.blogNewsService = blogNewsService;
		}

		[HttpGet]
		public async Task<ActionResult> GetBlogNews()
		{
			var data = await blogNewsService.QueryAsync();

			return Ok(data);
		}
	}
}
