using System.Collections.Generic;

namespace ToasterRoaster.Game.Services.Highscore
{
	public interface IHighscoreService
	{
		List<HighscoreEntry> GetTopTen();
		void Add(HighscoreEntry entry);
	}
}