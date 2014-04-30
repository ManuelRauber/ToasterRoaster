namespace ToasterRoaster.Game.Services.Achievements
{
	public class FirstGameWonAchievement : BaseAchievement
	{
		public override string Title
		{
			get { return "Just the beginning"; }
		}

		public override string Description
		{
			get { return "First game won"; }
		}

		protected override void DoStep(GameStep step)
		{
			if (GameStep.GameWon == step)
			{
				PercentageCompleted = 100;
			}
		}
	}
}