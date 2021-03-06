using System.ComponentModel.DataAnnotations;

namespace Bgc.ViewModels.Account
{
	public class ExternalLoginViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
