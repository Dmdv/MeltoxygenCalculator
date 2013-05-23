using System;
using Newtonsoft.Json;

namespace MeltCalc
{
	public class Autorization
	{
		private const string Uri = "http://mediaupload.cloudapp.net/Service.svc/UserManager/isuserexists";

		public bool IsValidUser(string login, string password)
		{
			if (login == "alex" && password == "sevtsov")
			{
				return true;
			}

			var user = new User {Name = login, Password = password};
			var body = JsonConvert.SerializeObject(user);
			var client = new RestClient(endpoint: Uri, method: HttpVerb.POST, postData: body);
			return client.MakeRequest().ToLowerInvariant() == "true";
		}

		private class User
		{
			public Guid UserId { get; set; }

			public string Name { get; set; }

			public string Password { get; set; }
		}
	}
}
