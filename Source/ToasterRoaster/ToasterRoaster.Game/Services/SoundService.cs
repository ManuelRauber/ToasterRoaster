using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common;
using WaveEngine.Framework.Sound;

namespace ToasterRoaster.Game.Services
{
    class SoundService : Service
    {
        public Dictionary<string, SoundInfo> Sound { get; private set;}
        protected override void Initialize()
        {
            Sound = new Dictionary<string, SoundInfo>();

            Sound.Add("buttonClick", new SoundInfo("Assets/Effects/buttonClick.mp3"));
            Sound.Add("failToast1", new SoundInfo("Assets/Effects/failToast1.mp3"));
            Sound.Add("failToast2", new SoundInfo("Assets/Effects/failToast2.mp3"));
            Sound.Add("failToast3", new SoundInfo("Assets/Effects/failToast3.mp3"));
            Sound.Add("Roasting", new SoundInfo("Assets/Effects/Roasting.mp3"));
            Sound.Add("successToast1", new SoundInfo("Assets/Effects/successToast1.mp3"));
            Sound.Add("successToast2", new SoundInfo("Assets/Effects/successToast2.mp3"));
            Sound.Add("successToast3", new SoundInfo("Assets/Effects/successToast3.mp3"));
            Sound.Add("toasterPull", new SoundInfo("Assets/Effects/toasterPull.mp3"));
        }
    }
}
