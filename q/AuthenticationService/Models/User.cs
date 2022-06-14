using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AuthenticationService.Models


{
	public class User
	{
		public User(){}
		#nullable disable
		public int Id { get; set; }
		public string Email { get; set; }
		[JsonIgnore]
		public string Password { get; set; }
	}
}




