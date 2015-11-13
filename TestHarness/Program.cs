using System;
using System.IO;
using System.Net;
using System.Threading;
using Intacct;
using Intacct.Entities;
using Intacct.Operations;
using Newtonsoft.Json;

namespace TestHarness
{
	internal class Program
	{
		private static void Main()
		{
			var settings = LoadSettings();

			var serverUri = new Uri(settings.ServerUri);
			var serverCredential = new NetworkCredential(settings.AccountUsername, settings.AccountPassword);
			var userCredential = new IntacctUserCredential(settings.CompanyName, settings.Username, settings.Password);

			var client = new IntacctClient(serverUri, serverCredential);

			var session = client.InitiateApiSession(userCredential).Result;

			var customer = new IntacctCustomer("C0003", "MT Test " + Guid.NewGuid())
			{
				ExternalId = "1337",
				PrimaryContact = new IntacctContact(Guid.NewGuid().ToString(), "Random")
			};
			var response = client.ExecuteOperations(new[] { new CreateCustomerOperation(session, customer) }, CancellationToken.None).Result;

			Console.WriteLine($"Success: {response.Success}");

			Console.WriteLine(session.SessionId);
			Console.WriteLine(session.EndpointUri);
		}

		private static TestHarnessSettings LoadSettings()
		{
			var settings = new TestHarnessSettings();
			if (File.Exists("Settings.json"))
			{
				settings = JsonConvert.DeserializeObject<TestHarnessSettings>(File.ReadAllText("Settings.json"));
			}

			Console.Write("Server URI: ");
			Console.Write(settings.ServerUri);
			settings.ServerUri = Console.ReadLine().OrDefault(settings.ServerUri);

			Console.Write("Account username: ");
			Console.Write(settings.AccountUsername);
			settings.AccountUsername = Console.ReadLine().OrDefault(settings.AccountUsername);
			Console.Write("Account password: ");
			Console.Write(settings.AccountPassword);
			settings.AccountPassword = Console.ReadLine().OrDefault(settings.AccountPassword);

			Console.Write("Company name: ");
			Console.Write(settings.CompanyName);
			settings.CompanyName = Console.ReadLine().OrDefault(settings.CompanyName);
			Console.Write("Username: ");
			Console.Write(settings.Username);
			settings.Username = Console.ReadLine().OrDefault(settings.Username);
			Console.Write("Password: ");
			Console.Write(settings.Password);
			settings.Password = Console.ReadLine().OrDefault(settings.Password);

			File.WriteAllText("Settings.json", JsonConvert.SerializeObject(settings));

			return settings;
		}
	}

	internal class TestHarnessSettings
	{
		public string ServerUri { get; set; }
		public string AccountUsername { get; set; }
		public string AccountPassword { get; set; }
		public string CompanyName { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
	}

	internal static class ReadLineExtension
	{
		public static string OrDefault(this string value, string defaultValue)
		{
			return (value.Length > 0) ? value : defaultValue;
		}
	}
}
