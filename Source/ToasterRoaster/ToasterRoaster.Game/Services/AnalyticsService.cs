using System.Collections.Generic;
using WaveEngine.Analytics;
using WaveEngine.Common;

namespace ToasterRoaster.Game.Services
{
	public class AnalyticsService : Service
	{
		private readonly Localytics _localytics;

		public AnalyticsService(IApplication application)
		{
			_localytics = new Localytics(application.Adapter, new LocalyticsInfo("0795f75e61fd8976e45b879-54089388-ca6c-11e3-99c4-005cf8cbabd8"));
		}

		protected override void Initialize()
		{
			_localytics.Open();
			_localytics.Upload();
		}

		public void TagEvent(string eventName, string attribute, string value)
		{
			_localytics.TagEvent(eventName, attribute, value);
			_localytics.Upload();
		}

		public void TagEvent(string eventName, Dictionary<string, string> attributes)
		{
			_localytics.TagEvent(eventName, attributes);
			_localytics.Upload();
		}

		public void Close()
		{
			_localytics.Close();
			_localytics.Upload();
		}
	}
}
