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
			_localytics = new Localytics(application.Adapter, new LocalyticsInfo("19121dc67d0744823bcf572-3a407cd6-ca5c-11e3-9c53-009c5fda0a25"));
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
