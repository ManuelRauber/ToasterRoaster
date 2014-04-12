﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common;

namespace ToasterRoaster.Game.Services
{
    public class LevelInformationService : Service
    {
        private ulong _level;
        public ulong Level
        {
            get { return _level; }
            private set
            {
                _level = value;
                TextureSize = 3 + (2 * Level);
            }
        }
        public ulong TextureSize { get; private set; }
        public double Score { get; private set; }

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
