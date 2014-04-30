namespace ToasterRoaster.Game.Services.Achievements
{
	public class FirstGameLostAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "A toast for the bin"; }
		}

		public override string Description
		{
			get { return "First game lost"; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameLost == step)
			{
				PercentageCompleted = 100;
			}
		}
	}
}