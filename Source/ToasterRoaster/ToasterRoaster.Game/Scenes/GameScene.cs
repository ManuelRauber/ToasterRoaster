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
			RenderManager.BackgroundColor = Color.Black;
			RenderManager.DebugLines = true;

			CreateCamera();
			CreateToaster();
		}

		private void CreateToaster()
		{
			var toaster = new Entity("toaster")
				.AddComponent(new Model("Assets/Models/toaster_template.wpk"))
				.AddComponent(new MaterialsMap(new BasicMaterial(Color.White)))
				.AddComponent(new Transform3D())
				.AddComponent(new ModelRenderer());

			EntityManager.Add(toaster);
		}

		private void CreateCamera()
		{
			FreeCamera mainCamera = new FreeCamera("MainCamera", new Vector3(0, 50, 80), Vector3.Zero);
			EntityManager.Add(mainCamera);
			RenderManager.SetActiveCamera(mainCamera.Entity);
		}
	}
}