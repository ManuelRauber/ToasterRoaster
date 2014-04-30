namespace ToasterRoaster.Game.Services.Achievements
{
	public class PlayedForTheFirstTimeAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "Go Roast a Toast!"; }
		}

		public override string Description
		{
			get { return "First time starting a single player game"; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameFinished == step)
			{
				PercentageCompleted = 100;
			}
		}
	}
}