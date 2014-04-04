using System;
using System.Runtime.Serialization;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Common
{
	public delegate void ConfigurationChangedHandler();
	public class Configuration
	{
		[DataContract]
		private class ConfigurationObject
		{
			[DataMember]
			public bool IsMusicEnabled = true;

			[DataMember]
			public int MusicVolume = 100;

			[DataMember]
			public string PlayerName = "Alfredo";

			[DataMember]
			public bool AutomaticHighscoreUpload;
		}

		private const string CONFIGURATION_FILE_NAME = "Config.conf";
		private readonly Storage _storage;
		private static Lazy<Configuration> _instance = new Lazy<Configuration>(() => new Configuration());
		private ConfigurationObject _configurationObject;

		public event ConfigurationChangedHandler OnConfigurationChanged;

		public static Configuration Instance
		{
			get { return _instance.Value; }
		}

		private Configuration()
		{
			_storage = WaveServices.Storage;
			LoadConfiguration();
		}

		private void LoadConfiguration()
		{
			_configurationObject = new ConfigurationObject();

			if (_storage.ExistsStorageFile(CONFIGURATION_FILE_NAME))
			{
				try
				{
					_configurationObject = _storage.Read<ConfigurationObject>(CONFIGURATION_FILE_NAME);
				}
// ReSharper disable EmptyGeneralCatchClause
				catch
// ReSharper restore EmptyGeneralCatchClause
				{
					// Ignore invalid configuration files, it will be written after the next save!
				}
			}

			DoConfigurationChanged();
		}

		private void SaveConfiguration()
		{
			_storage.Write(CONFIGURATION_FILE_NAME, _configurationObject);
		}

		public bool IsMusicEnabled
		{
			get { return _configurationObject.IsMusicEnabled; }
			set
			{
				_configurationObject.IsMusicEnabled = value;
				SaveConfiguration();
				DoConfigurationChanged();
			}
		}

		public int MusicVolume
		{
			get { return _configurationObject.MusicVolume; }
			set
			{
				if ((value < 0)
				    || (value > 100))
				{
					return;
				}

				_configurationObject.MusicVolume = value;
				SaveConfiguration();
				DoConfigurationChanged();
			}
		}

		public string PlayerName
		{
			get { return _configurationObject.PlayerName; }
			set
			{
				if (String.IsNullOrWhiteSpace(value))
				{
					return;
				}

				_configurationObject.PlayerName = value;
				SaveConfiguration();
				DoConfigurationChanged();
			}
		}

		public bool AutomaticHighscoreUpload
		{
			get { return _configurationObject.AutomaticHighscoreUpload; }
			set
			{
				_configurationObject.AutomaticHighscoreUpload = value;
				SaveConfiguration();
				DoConfigurationChanged();
			}
		}

		private void DoConfigurationChanged()
		{
			var handler = OnConfigurationChanged;
			if (null != handler)
			{
				handler();
			}
		}
	}
}