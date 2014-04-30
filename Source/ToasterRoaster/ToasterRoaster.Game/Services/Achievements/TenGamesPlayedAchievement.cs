namespace ToasterRoaster.Game.Services.Achievements
{
	public class TenGamesPlayedAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "Getting fat"; }
		}

		public override string Description
		{
			get { return "Ten games played."; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameFinished == step)
			{
				PercentageCompleted += 10;
			}
		}
	}
}