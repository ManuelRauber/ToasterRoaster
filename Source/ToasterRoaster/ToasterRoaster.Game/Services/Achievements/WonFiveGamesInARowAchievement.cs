namespace ToasterRoaster.Game.Services.Achievements
{
	public class WonFiveGamesInARowAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "C-C-C-Combo"; }
		}

		public override string Description
		{
			get { return "Won five games in a row"; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameWon == step)
			{
				PercentageCompleted += 20;
				return;
			}

			if ((GameStep.GameLost == step)
				|| (GameStep.GameAbandoned == step))
			{
				PercentageCompleted = 0;
			}
		}
	}
}