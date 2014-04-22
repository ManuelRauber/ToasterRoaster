using System;
using System.Collections.Generic;
using System.Linq;

namespace ToasterRoaster.Game.Services.Highscore
{
	public class HighscoreManager
	{
		private static readonly Lazy<HighscoreManager> _instance = new Lazy<HighscoreManager>(() => new HighscoreManager());

		private readonly Dictionary<string, IHighscoreService> _services; 

		public static HighscoreManager Instance
		{
			get { return _instance.Value; }
		}

		private HighscoreManager()
		{
			_services = new Dictionary<string, IHighscoreService>();
		}

		public void RegisterHighscoreService(string name, IHighscoreService service)
		{
			_services.Add(name, service);
		}

		public void RegisterDefaultServices()
		{
			RegisterHighscoreService("local", new LocalHighscoreService());

			// Enable this for registering the online highscore service
			// Does currently not work due to missing azure subscription
			//RegisterHighscoreService("online", new AzureHighscoreService());
		}

		public List<HighscoreEntry> GetTopTenFrom(string serviceName)
		{
			var service = _services.SingleOrDefault(x => x.Key.Equals(serviceName));

			if (service.Equals(default(KeyValuePair<string, IHighscoreService>)))
			{
				return new List<HighscoreEntry>();
			}

			return service.Value.GetTopTen();
		}

		public void AddHighscore(HighscoreEntry entry)
		{
			foreach (var service in _services.Values)
			{
				service.Add(entry);
			}
		}
	}
}