using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Services.Achievements;
using WaveEngine.Common;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Services
{
    public class LevelInformationService : Service
    {
        private ulong _level;
        private double _score;
        public ulong Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                TextureSize = 3 + (1 * Level);

	            CheckAchievements();
            }
        }

	    private void CheckAchievements()
	    {
		    var achievementService = WaveServices.GetService<AchievementService>();

		    if (_level == 5)
		    {
			    achievementService.Steps(GameStep.ReachedLevelFive);
			    return;
		    }

		    if (_level == 10)
		    {
			    achievementService.Steps(GameStep.ReachedLevelTen);
			    return;
		    }

		    if (_level == 20)
		    {
			    achievementService.Steps(GameStep.ReachedLevelTwenty);
		    }
	    }

	    public ulong TextureSize { get; private set; }
        public double Score
        { 
            get { return _score; }
            private set
            {
                _score = Math.Round(value);
            }
        }

        protected override void Initialize()
        {
            Reset();
        }

        public void IncreaseLevel()
        {
            Level++;
        }

        public void AddScore(double score)
        {
            Score += score;
        }

        public void Reset()
        {
            Level = 1;
            Score = 0;
        }
    }
}
