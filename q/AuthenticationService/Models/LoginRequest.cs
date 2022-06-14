using System;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationService.Models
{
	public class LoginRequest
	{

			[Required]
			[DataType(DataType.EmailAddress)]
			[StringLength(255)]
			public string Email { get; set; }

			[Required]
			public string Password { get; set; }
	}
}

