using System.Collections.Generic;
using System.Runtime.Serialization;
using ToasterRoaster.Game.Services.Achievements;
using WaveEngine.Common;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Services
{
	public delegate void AchievementCompletedHandler(IAchievement achievement);

	public class AchievementService : Service
	{
		private AchievementList _achievementList;
		private const string ACHIEVEMENTS_FILE_NAME = "Achievements.conf";

		public event AchievementCompletedHandler AchievementCompleted;

		[DataContract]
		private class AchievementList
		{
			private List<IAchievement> _achievements;

			[DataMember]
			public List<IAchievement> Achievements
			{
				get { return _achievements; }
				set { _achievements = value; }
			}
		}

		protected override void Initialize()
		{
			_achievementList = new AchievementList();

			LoadAchievements();

			if (0 == _achievementList.Achievements.Count)
			{
				RegisterAchievements();
			}
		}

		private void RegisterAchievements()
		{
			RegisterAchievement<FirstGameLostAchievement>();
			RegisterAchievement<FirstGameWonAchievement>();
			RegisterAchievement<LostFiveGamesInARow>();
			RegisterAchievement<OneHundredGamesPlayedAchievement>();
			RegisterAchievement<PlayedForTheFirstTimeAchievement>();
			RegisterAchievement<TenGamesPlayedAchievement>();
			RegisterAchievement<WonFiveGamesInARowAchievement>();

			AttachAchievementHandler();
		}

		private void RegisterAchievement<T>()
			where T: IAchievement, new()
		{
			_achievementList.Achievements.Add(new T());
		}

		private void LoadAchievements()
		{
			if (!WaveServices.Storage.ExistsStorageFile(ACHIEVEMENTS_FILE_NAME))
			{
				return;
			}

			_achievementList = WaveServices.Storage.Read<AchievementList>(ACHIEVEMENTS_FILE_NAME);
			AttachAchievementHandler();
		}

		private void AttachAchievementHandler()
		{
			foreach (var achievement in _achievementList.Achievements)
			{
				achievement.AchievementCompleted += DoAchievementCompleted;
			}
		}

		private void DoAchievementCompleted(IAchievement achievement)
		{
			var handler = AchievementCompleted;

			if (null != handler)
			{
				handler(achievement);
			}
		}

		public void Steps(params GameStep[] steps)
		{
			foreach (var step in steps)
			{
				Step(step);
			}

			SaveAchievements();
		}

		private void Step(GameStep step)
		{
			foreach (var achievement in _achievementList.Achievements)
			{
				achievement.Calculate(step);
			}
		}

		private void SaveAchievements()
		{
			WaveServices.Storage.Write(ACHIEVEMENTS_FILE_NAME, _achievementList);
		}
	}
}