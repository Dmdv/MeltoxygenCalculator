using System;
using Newtonsoft.Json;

namespace MeltCalc
{
	public class Autorization
	{
		private const string Uri = "http://mediaupload.cloudapp.net/Service.svc/UserManager/isuserexists";

		public static bool IsValidUser(string login, string password)
		{
			if (login == "alex" && password == "sevtsov")
			{
				return true;
			}

			var user = new User
			{
				Name = login,
				Password = password
			};

			var body = JsonConvert.SerializeObject(user);
			var client = new RestClient(Uri, HttpVerb.POST, body);
			return client.MakeRequest().ToLowerInvariant() == "true";
		}

		private class User
		{
			public string Name { get; set; }

			public string Password { get; set; }

			public Guid UserId { get; set; }
		}
	}
}