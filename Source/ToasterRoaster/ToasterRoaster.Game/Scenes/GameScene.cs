using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics3D;

namespace ToasterRoaster.Game.Scenes
{
    public class GameScene : Scene
    {

        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.Orange;

            FreeCamera mainCamera = new FreeCamera("MainCamera", new Vector3(0, 50, 80), Vector3.Zero);
            EntityManager.Add(mainCamera);
            RenderManager.SetActiveCamera(mainCamera.Entity);

            var toaster = new Entity("toaster");
            toaster.AddComponent(new Transform3D());
            toaster.AddComponent(new BoxCollider());
            toaster.AddComponent(new Model("Content/toaster_template.wpk"));
            toaster.AddComponent(new MaterialsMap());
            toaster.AddComponent(new ModelRenderer());

            EntityManager.Add(toaster);
        }
    }
}
