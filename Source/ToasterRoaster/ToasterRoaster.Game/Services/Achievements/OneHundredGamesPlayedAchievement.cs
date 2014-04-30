namespace ToasterRoaster.Game.Services.Achievements
{
	public class OneHundredGamesPlayedAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "I'm obese now"; }
		}

		public override string Description
		{
			get { return "Played one hundred games."; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameFinished == step)
			{
				PercentageCompleted += 1;
			}
		}
	}
}