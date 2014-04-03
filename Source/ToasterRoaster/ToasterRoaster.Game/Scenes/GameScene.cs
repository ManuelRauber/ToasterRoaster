using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Behaviors;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.UI;
using WaveEngine.Materials;

namespace ToasterRoaster.Game.Scenes
{
    public class GameScene : Scene
    {
        private bool _started = false;

        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.Green;
            RenderManager.DebugLines = true;

            TextBlock toasterPosition = new TextBlock("ToasterPosition")
            {
                Text = "Toaster Position: ",
                Margin = new Thickness(20f),
            };
            EntityManager.Add(toasterPosition);

            CreateLight();
            CreateCamera();
            CreateToaster();
            //CreatePlate();
        }

        private void CreatePlate()
        {
            var plate = new Entity("plate")
                .AddComponent(new Model("Assets/Models/teller_firsttry.wpk"))
                .AddComponent(new MaterialsMap(new BasicMaterial("Assets/Textures/Plate.wpk") { LightingEnabled = true, DiffuseColor = Color.White, }))
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
                .AddComponent(new SkinnedModel("Assets/Animation/toaster_template_animation_uv_fbx.wpk"))
                .AddComponent(new MaterialsMap(new BasicMaterial("Assets/Textures/toaster_red.wpk") { LightingEnabled = true, }))
                .AddComponent(new Transform3D() { Scale = new Vector3(1.5f, 1, 0.67f), })
                .AddComponent(new Animation3D("Assets/Animation/toaster_template_animation_uv_fbx_animation.wpk"))
                .AddComponent(new SkinnedModelRenderer())
                .AddComponent(new ToasterBehavior())
                .AddComponent(new Transform2D())
                .AddComponent(new RectangleCollider())
                .AddComponent(new TouchGestures());

            toaster.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(StartGame);

            EntityManager.Add(toaster);

            Animation3D anim = toaster.FindComponent<Animation3D>();
            anim.PlayAnimation("StartRoast", true);
        }

        private void CreateCamera()
        {
            //FixedCamera mainCamera = new FixedCamera("MainCamera", new Vector3(0, 15, 25), Vector3.Zero);
            FreeCamera mainCamera = new FreeCamera("MainCamera", new Vector3(0, 15, 25), Vector3.Zero);
            EntityManager.Add(mainCamera);
            RenderManager.SetActiveCamera(mainCamera.Entity);
        }
        private void StartGame(object sender, GestureEventArgs e)
        {
            if (_started)
            {
                return;
            }

            _started = true;

            EntityManager.Find<TextBlock>("ToasterPosition").Text = "Start";

            Entity toast = new Entity("toast")
                .AddComponent(new Transform3D())
                .AddComponent(Model.CreatePlane(Vector3.UnitZ, 5))
                .AddComponent(new MaterialsMap(new BasicMaterial(Color.Gold)))
                .AddComponent(new ModelRenderer())
                .AddComponent(new Transform2D())
                .AddComponent(new RectangleCollider())
                .AddComponent(new TouchGestures());

            toast.FindComponent<TouchGestures>().TouchPressed += new EventHandler<GestureEventArgs>(DrawOnToast);

            EntityManager.Add(toast);
            
            ThirdPersonCamera camera = new ThirdPersonCamera("ToasterCamera", toast);
            EntityManager.Add(camera);
            camera.Entity.AddComponent(new ToastBehavior(toast));
            RenderManager.SetActiveCamera(camera.Entity);
        }

        private void DrawOnToast(object sender, GestureEventArgs e)
        {
            Entity toast = sender as Entity;
            MaterialsMap materialsMap = toast.FindComponent<MaterialsMap>();
            
        }
    }
}