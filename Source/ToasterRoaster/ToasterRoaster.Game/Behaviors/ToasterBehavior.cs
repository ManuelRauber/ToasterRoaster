using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Input;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace ToasterRoaster.Game.Behaviors
{
    public class ToasterBehavior : Behavior
    {
        private KeyboardState _state;

        [RequiredComponent]
        public Transform3D ToasterTransform;

        public ToasterBehavior()
            : base("ToasterBehavior")
        { }
                protected override void Update(TimeSpan gameTime)
        {
            _state = WaveServices.Input.KeyboardState;

            if (_state.Left == ButtonState.Pressed)
                ToasterTransform.Rotation.Y -= 0.03f;
            if (_state.Right == ButtonState.Pressed)
                ToasterTransform.Rotation.Y += 0.03f;

            //EntityManager.Find<TextBlock>("ToasterPosition").Text = "Toaster Position: " + ToasterTransform.Position.ToString() + "\tRotation: " + ToasterTransform.Rotation.ToString();
    
        }
    }
}
