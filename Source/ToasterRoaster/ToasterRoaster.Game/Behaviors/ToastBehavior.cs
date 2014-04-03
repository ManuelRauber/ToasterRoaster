using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WaveEngine.Components.Cameras;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Scenes
{
    class ToastBehavior : Behavior
    {
        private const int SPEED = 25;
        private Entity _toast;
        private Transform3D _transform3D;
        private Transform2D _transform2D;

        private double _elapsedTime;

        [RequiredComponent]
        public Camera Camera;

        public ToastBehavior(Entity toast)
            : base("ToastBehavior")
        {
            _toast = toast;
            _transform3D = toast.FindComponent<Transform3D>();
            _transform2D = toast.FindComponent<Transform2D>();
            _elapsedTime = 0;
            Camera = null;
        }

        protected override void Update(TimeSpan gameTime)
        {
            if (_transform3D.Position.Y >= 0)
            {
                _elapsedTime += gameTime.TotalSeconds;
                _transform3D.Position.Y += (float)(SPEED * _elapsedTime - 0.5 * 9.81 * Math.Pow(_elapsedTime, 2));
                Camera.Position.Y = _transform3D.Position.Y;
                Camera.LookAt = _transform3D.Position;       
            }
            else
            {
                _elapsedTime = 0;
                _transform3D.Position.Y = 0;
            }
        }

        public void Reset()
        {
            _transform3D.Position.Y = 0;
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
        }
    }
}
