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
using WaveEngine.Materials;

namespace ToasterRoaster.Game.Scenes
{
	public class GameScene : Scene
	{
		protected override void CreateScene()
		{
			RenderManager.BackgroundColor = Color.Green;
			RenderManager.DebugLines = true;

            CreateLight();
			CreateCamera();
			CreateToaster();
            //CreatePlate();
		}

        private void CreatePlate()
        {
            var plate = new Entity("plate")
                .AddComponent(new Model("Assets/Models/teller_firsttry.wpk"))
                .AddComponent(new MaterialsMap(new BasicMaterial("Assets/Textures/Plate.wpk") { LightingEnabled = true, DiffuseColor = Color.White,}))
                .AddComponent(new Transform3D())
                .AddComponent(new ModelRenderer());

            EntityManager.Add(plate);
        }

        private void CreateLight()
        {
            DirectionalLight skylight = new DirectionalLight("SkyLight", new Vector3(20));
            skylight.Color = Color.White;
            EntityManager.Add(skylight);
        }

		private void CreateToaster()
		{
            var toaster = new Entity("toaster")
                .AddComponent(new Model("Assets/Models/toaster_template_uv_fbx.wpk"))
                .AddComponent(new MaterialsMap(new BasicMaterial("Assets/Textures/toaster_red.wpk") { LightingEnabled = true, }))
                .AddComponent(new Transform3D() { Scale = new Vector3(1.5f, 1, 0.67f), })
                .AddComponent(new ModelRenderer());

			EntityManager.Add(toaster);
		}

		private void CreateCamera()
		{
			FreeCamera mainCamera = new FreeCamera("MainCamera", new Vector3(0, 15, 25), Vector3.Zero);
			EntityManager.Add(mainCamera);
			RenderManager.SetActiveCamera(mainCamera.Entity);
		}
	}
}