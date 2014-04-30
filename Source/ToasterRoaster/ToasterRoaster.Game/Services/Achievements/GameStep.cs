namespace ToasterRoaster.Game.Services.Achievements
{
	public enum GameStep
	{
		/// <summary>
		/// When a game has been started
		/// </summary>
		GameStarted,

		/// <summary>
		/// When a game has been won
		/// </summary>
		GameWon,

		/// <summary>
		/// When a game has been lost
		/// </summary>
		GameLost,

		/// <summary>
		/// When a game has been finished (no matter if won or lost)
		/// </summary>
		GameFinished,

		/// <summary>
		/// When the game has been finished as a three star game
		/// </summary>
		ThreeStarGameWon,

		/// <summary>
		/// When the game has been finished as a two star game
		/// </summary>
		TwoStarGameWon,

		/// <summary>
		/// When the game has been finished as a one star game
		/// </summary>
		OneStarGameWon,

		/// <summary>
		/// When the game has been aborted
		/// </summary>
		GameAbandoned,

		/// <summary>
		/// When a game has been finished with 100% accuracy
		/// </summary>
		OneHundredPercentAccuracy,

		/// <summary>
		/// When a game has been finished with 0% accuracy
		/// </summary>
		ZeroPercentAccuracy,

		/// <summary>
		/// When level 5 has been reached
		/// </summary>
		ReachedLevelFive,

		/// <summary>
		/// When level 10 has been reached
		/// </summary>
		ReachedLevelTen,

		/// <summary>
		/// When level 20 has been reached
		/// </summary>
		ReachedLevelTwenty,
	}
}