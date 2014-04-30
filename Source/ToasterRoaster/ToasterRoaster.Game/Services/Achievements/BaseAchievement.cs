using System.Runtime.Serialization;

namespace ToasterRoaster.Game.Services.Achievements
{
	[DataContract]
	public abstract class BaseAchievement : IAchievement
	{
		public abstract string Title { get; }
		public abstract string Description { get; }

		[DataMember]
		public int PercentageCompleted { get; set; }

		public void Calculate(GameStep step)
		{
			if (100 == PercentageCompleted)
			{
				return;
			}

			DoStep(step);
			CheckForCompletion();
		}

		private void CheckForCompletion()
		{
			if (100 == PercentageCompleted)
			{
				DoAchievementCompleted();
			}
		}

		protected abstract void DoStep(GameStep step);

		public event AchievementCompletedHandler AchievementCompleted;

		protected void DoAchievementCompleted()
		{
			var handler = AchievementCompleted;
			if (null != handler)
			{
				handler(this);
			}
		}
	}
}