using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Services.Highscore
{
	public class LocalHighscoreService : IHighscoreService
	{
		private const string HIGHSCORE_FILE_NAME = "Highscores.conf";
		private HighscoreList _highscoreList;

		[DataContract]
		private class HighscoreList
		{
			private List<HighscoreEntry> _highscores;

			[DataMember]
			public List<HighscoreEntry> Highscores
			{
				get { return _highscores ?? (_highscores = new List<HighscoreEntry>()); }
				set { _highscores = value; }
			}
		}

		public LocalHighscoreService()
		{
			LoadHighscores();
		}

		private void LoadHighscores()
		{
			_highscoreList = new HighscoreList();

			if (WaveServices.Storage.ExistsStorageFile(HIGHSCORE_FILE_NAME))
			{
				_highscoreList = WaveServices.Storage.Read<HighscoreList>(HIGHSCORE_FILE_NAME);
			}
		}

		public List<HighscoreEntry> GetTopTen()
		{
			return _highscoreList.Highscores.OrderByDescending(x => x.Points).Take(10).ToList();
		}

		public void Add(HighscoreEntry entry)
		{
			_highscoreList.Highscores.Add(entry);
			SaveHighscores();
		}

		private void SaveHighscores()
		{
			WaveServices.Storage.Write(HIGHSCORE_FILE_NAME, _highscoreList);
		}
	}
}