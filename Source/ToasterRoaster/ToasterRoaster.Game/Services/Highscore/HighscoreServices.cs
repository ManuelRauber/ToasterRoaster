using System;
using System.Collections.Generic;
using System.Linq;
using WaveEngine.Common;

namespace ToasterRoaster.Game.Services.Highscore
{
	public class HighscoreServices : Service
	{
		private Dictionary<string, IHighscoreService> _services; 

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

		protected override void Initialize()
		{
			_services = new Dictionary<string, IHighscoreService>();
		}
	}
}