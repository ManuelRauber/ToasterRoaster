using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Scenes
{
    class FlightBehavior : Behavior
    {
        private const int SPEED = 25;
        private Entity _toast;
        private float _height;

        private double _elapsedTime;

        [RequiredComponent]
        public Camera Camera;

        public FlightBehavior(Entity toast)
            : base("FlightBehavior")
        {
            _toast = toast;
            _height = 0;
            _elapsedTime = 0;
            Camera = null;
        }

        protected override void Update(TimeSpan gameTime)
        {

            //RigidBody3D rigidBody = this.Owner.FindComponent<RigidBody3D>();
            //rigidBody.ApplyLinearImpulse(SPEED * Vector3.UnitY);

            if (_height >= 0)
            {
                _elapsedTime += gameTime.TotalSeconds;
                _height = (float)(SPEED * _elapsedTime - 0.5 * 9.81 * Math.Pow(_elapsedTime, 2));
                Camera.Position.Y = _height;
                Camera.LookAt.Y = _height;       
            }
            else
            {
                //Reset();
                SceneManager.Instance.To<EvaluationScene>();
                this.IsActive = false;
            }



            EntityManager.Find<TextBlock>("ToasterPosition").Text = "Höhe: " + _height;
        }

        public void Reset()
        {
            _elapsedTime = 0;
            _height = 0;
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
        }
    }
}
