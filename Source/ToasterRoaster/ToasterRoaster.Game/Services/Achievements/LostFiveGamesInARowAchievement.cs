namespace ToasterRoaster.Game.Services.Achievements
{
	public class LostFiveGamesInARowAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "Maybe buy a new toaster?"; }
		}

		public override string Description
		{
			get { return "Lost five games in a row"; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameLost == step)
			{
				PercentageCompleted += 20;
				return;
			}

			if (GameStep.GameWon == step)
			{
				PercentageCompleted = 0;
			}
		}
	}
}