using System.Collections.Generic;
using Microsoft.WindowsAzure.MobileServices;
using ToasterRoaster.Game.Common;

namespace ToasterRoaster.Game.Services.Highscore
{
	public class AzureHighscoreService : IHighscoreService
	{
		private IMobileServiceTable<HighscoreEntry> _table;
		private MobileServiceClient _client;
		private const string AZURE_SERVICE_URL = "";

		public AzureHighscoreService()
		{
			_client = new MobileServiceClient(AZURE_SERVICE_URL);
			_table = _client.GetTable<HighscoreEntry>();
		}

		public List<HighscoreEntry> GetTopTen()
		{
			var task = _table.OrderByDescending(x => x.Points).Take(10).ToListAsync();
			var awaiter = task.ConfigureAwait(false).GetAwaiter();
			return awaiter.GetResult();
		}

		public void Add(HighscoreEntry entry)
		{
			if (!Configuration.Instance.AutomaticHighscoreUpload)
			{
				return;
			}

			_table.InsertAsync(entry);
		}
	}
}