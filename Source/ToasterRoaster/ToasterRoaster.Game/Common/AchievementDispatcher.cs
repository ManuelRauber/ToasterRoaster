using ToasterRoaster.Game.Services;
using ToasterRoaster.Game.Services.Achievements;
using WaveEngine.Common;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Common
{
	public class AchievementDispatcher : Service
	{
		private AchievementService _service;

		private StackPanel CreateBadge(IAchievement achievement, EntityManager entityManager)
		{
			var background = new StackPanel("badge") { Width = WaveServices.Platform.ScreenWidth, Height = 150, DrawOrder = 1 };
			
			var headline = new TextBlock()
			{
				Text = "Achievement completed!",
			};

			var title = new TextBlock()
			{
				Text = achievement.Title,
			};

			var description = new TextBlock()
			{
				Text = achievement.Description,
			};

			var dismissButton = new Button()
			{
				Text = "Dismiss",
			};

			dismissButton.Click += (sender, args) => entityManager.Remove("badge");

			background.Add(headline);
			background.Add(title);
			background.Add(description);

			return background;
		}

		private void AchievementCompleted(IAchievement achievement)
		{
			var entityManager = WaveServices.ScreenContextManager.CurrentContext[0].EntityManager;
			var badge = CreateBadge(achievement, entityManager);
			entityManager.Add(badge);
		}

		protected override void Initialize()
		{
			_service = WaveServices.GetService<AchievementService>();
			_service.AchievementCompleted += AchievementCompleted;
		}
	}
}