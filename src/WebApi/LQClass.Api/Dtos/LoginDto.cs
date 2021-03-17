using System.ComponentModel.DataAnnotations;

namespace LQClass.Api.Dtos
{
	public class LoginDto
	{
		[Required]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
