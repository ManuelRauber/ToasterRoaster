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
			PreloadBackgroundMusic();
			WaveServices.MusicPlayer.OnSongCompleted += MusicPlayerSongCompleted;
		}

		private void MusicPlayerSongCompleted(object sender, EventArgs e)
		{
            WaveServices.MusicPlayer.Play(GetNextTitle());
		}

		private void PreloadBackgroundMusic()
		{
			_musicList = new List<MusicInfo>()
			{
				new MusicInfo("Assets/Music/BushwickTarantella.mp3"),
				new MusicInfo("Assets/Music/FunInABottle.mp3"),
				new MusicInfo("Assets/Music/GaslampFunworks.mp3"),
				new MusicInfo("Assets/Music/MonkeysSpinningMonkeys.mp3"),
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
            //WaveServices.MusicPlayer.Play(GetNextTitle());
		}
	}
}