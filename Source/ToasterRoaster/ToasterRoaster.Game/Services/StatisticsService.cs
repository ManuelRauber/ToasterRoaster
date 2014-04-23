using System.Runtime.Serialization;
using WaveEngine.Common;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Services
{
	public class StatisticsService : Service
	{
		private const string STATISTICS_FILE_NAME = "statistics.conf";
		private StatisticsObject _statistics;

		[DataContract]
		private class StatisticsObject
		{
			[DataMember]
			public double AccumulatedAccuracy = 0;

			[DataMember]
			public int TotalGamesPlayed = 0;

			[DataMember]
			public int GamesWon = 0;

			[DataMember]
			public int GamesLost = 0;

			[DataMember]
			public int GamesAborted = 0;

			[DataMember]
			public int ThreeStarGames = 0;

			[DataMember]
			public int TwoStarGames = 0;

			[DataMember]
			public int OneStarGames = 0;
		}

		private void LoadStatistics()
		{
			_statistics = new StatisticsObject();

			if (WaveServices.Storage.ExistsStorageFile(STATISTICS_FILE_NAME))
			{
				_statistics = WaveServices.Storage.Read<StatisticsObject>(STATISTICS_FILE_NAME);
			}
		}

		/// <summary>
		/// Usage: Just add the accuracy, don't do any calculations
		/// </summary>
		public double OverallAccuracy
		{
			get { return _statistics.AccumulatedAccuracy / _statistics.TotalGamesPlayed; }
			set
			{
				_statistics.AccumulatedAccuracy = value;
				SaveStatistics();
			}
		}

		public int TotalGamesPlayed
		{
			get { return _statistics.TotalGamesPlayed; }
			set
			{
				_statistics.TotalGamesPlayed = value;
				SaveStatistics();
			}
		}

		public int GamesWon
		{
			get { return _statistics.GamesWon; }
			set
			{
				_statistics.GamesWon = value;
				SaveStatistics();
			}
		}

		public int GamesLost
		{
			get { return _statistics.GamesLost; }
			set
			{
				_statistics.GamesLost = value;
				SaveStatistics();
			}
		}

		public int ThreeStarGames
		{
			get { return _statistics.ThreeStarGames; }
			set
			{
				_statistics.ThreeStarGames = value;
				SaveStatistics();
			}
		}

		public int TwoStarGames
		{
			get { return _statistics.TwoStarGames; }
			set
			{
				_statistics.TwoStarGames = value;
				SaveStatistics();
			}
		}

		public int GamesAborted
		{
			get { return _statistics.GamesAborted; }
			set
			{
				_statistics.GamesAborted = value;
				SaveStatistics();
			}
		}

		public int OneStarGames
		{
			get { return _statistics.OneStarGames; }
			set
			{
				_statistics.OneStarGames = value;
				SaveStatistics();
			}
		}

		public void ResetStatistic()
		{
			_statistics = new StatisticsObject();
			SaveStatistics();
		}

		private void SaveStatistics()
		{
			WaveServices.Storage.Write(STATISTICS_FILE_NAME, _statistics);
		}

		protected override void Initialize()
		{
			LoadStatistics();
		}
	}
}