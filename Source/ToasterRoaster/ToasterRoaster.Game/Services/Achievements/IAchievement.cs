namespace ToasterRoaster.Game.Services.Achievements
{
	public interface IAchievement
	{
		string Title { get; }
		string Description { get; }
		int PercentageCompleted { get; set; }

		void Calculate(GameStep step);

		event AchievementCompletedHandler AchievementCompleted;
	}
}