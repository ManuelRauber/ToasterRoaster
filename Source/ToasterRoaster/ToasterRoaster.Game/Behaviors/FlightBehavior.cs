using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToasterRoaster.Game.Scenes;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Behaviors
{
    class FlightBehavior : Behavior
    {
        private Entity _toast;

        [RequiredComponent]
        public Camera Camera;

        public FlightBehavior(Entity toast)
            : base("FlightBehavior")
        {
            _toast = toast;
            Camera = null;
        }

        protected override void Initialize()
        {
            base.Initialize();

            Entity toast = new Entity("toast")
                .AddComponent(new Sprite("Assets/Textures/toast_2D.wpk"))
                .AddComponent(new SpriteRenderer(DefaultLayers.Alpha))
                .AddComponent(new RectangleCollider())
                .AddComponent(new Transform2D()
                {
                    X = WaveServices.Platform.ScreenWidth / 2,
                    Y = WaveServices.Platform.ScreenHeight / 2,
                    XScale = 0.2f,
                    YScale = 0.2f,
                    Origin = Vector2.Center,
                })
                .AddComponent(new ToastBehavior());

            EntityManager.Add(toast);
        }

        protected override void Update(TimeSpan gameTime)
        {
            RigidBody3D rigidBody = _toast.FindComponent<RigidBody3D>();
            
            if (rigidBody.Transform3D.Position.Y >= 0)
            {
                Camera.Position.Y = rigidBody.Transform3D.Position.Y;
                Camera.LookAt.Y = rigidBody.Transform3D.Position.Y;       
            }
            else
            {
                SceneManager.Instance.To<EvaluationScene>();
                this.IsActive = false;
            }

            EntityManager.Find<TextBlock>("ToasterPosition").Text = "Höhe: " + rigidBody.Transform3D.Position.Y;
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
        }
    }
}
