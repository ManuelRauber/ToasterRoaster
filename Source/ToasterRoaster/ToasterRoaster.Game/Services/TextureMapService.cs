using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common;

namespace ToasterRoaster.Game.Services
{
    class TextureMapService : Service
    {
        public bool[,] GivenTexture { get; set; }
        public bool[,] DrawnTexture { get; set; }

        protected override void Initialize()
        {
        }
    }
}
