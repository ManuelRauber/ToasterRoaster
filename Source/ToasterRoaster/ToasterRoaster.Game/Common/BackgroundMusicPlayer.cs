using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using WaveEngine.Common.Media;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Common
{
	public class BackgroundMusicPlayer
	{
		private static readonly Lazy<BackgroundMusicPlayer> _instance =
			new Lazy<BackgroundMusicPlayer>(() => new BackgroundMusicPlayer());

		private IList<MusicInfo> _musicList;
		private int _indexLastPlayed = -1;

		public static BackgroundMusicPlayer Instance
		{
			get { return _instance.Value; }
		}

		private BackgroundMusicPlayer()
		{
			Configuration.Instance.OnConfigurationChanged += ConfigurationChanged;
			ConfigurationChanged();
			PreloadBackgroundMusic();
			WaveServices.MusicPlayer.OnSongCompleted += MusicPlayerSongCompleted;
		}

		private void ConfigurationChanged()
		{
			WaveServices.MusicPlayer.MusicEnabled = Configuration.Instance.IsMusicEnabled;
			WaveServices.MusicPlayer.Volume = Configuration.Instance.MusicVolume;
		}

		private void MusicPlayerSongCompleted(object sender, EventArgs e)
		{
			if (!Configuration.Instance.IsMusicEnabled)
			{
				return;
			}

			WaveServices.MusicPlayer.Play(GetNextTitle());
		}

		private void PreloadBackgroundMusic()
		{
			_musicList = new List<MusicInfo>()
			{
				new MusicInfo("Assets/Music/backgroundMusic1.mp3"),
				new MusicInfo("Assets/Music/backgroundMusic2.mp3"),
				new MusicInfo("Assets/Music/backgroundMusic3.mp3"),
				new MusicInfo("Assets/Music/backgroundMusic4.mp3"),
			};
		}

		private MusicInfo GetNextTitle()
		{
			int random;

			do
			{
				random = WaveServices.Random.Next(0, _musicList.Count);
			} while (random == _indexLastPlayed);

			_indexLastPlayed = random;
			return _musicList[random];
		}

		public void Start()
		{
			if (!Configuration.Instance.IsMusicEnabled)
			{
				return;
			}

			WaveServices.MusicPlayer.Play(GetNextTitle());
		}
	}
}