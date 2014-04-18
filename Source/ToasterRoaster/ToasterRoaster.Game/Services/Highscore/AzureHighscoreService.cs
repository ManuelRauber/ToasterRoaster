using System.Collections.Generic;
using ToasterRoaster.Game.Common;

namespace ToasterRoaster.Game.Services.Highscore
{
	public class AzureHighscoreService : IHighscoreService
	{
		public List<HighscoreEntry> GetTopTen()
		{
			throw new System.NotImplementedException();
		}

		public void Add(HighscoreEntry entry)
		{
			if (!Configuration.Instance.AutomaticHighscoreUpload)
			{
				return;
			}
			throw new System.NotImplementedException();
		}
	}
}