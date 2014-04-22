using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToasterRoaster.Game.Behaviors;
using ToasterRoaster.Game.Scenes;
using ToasterRoaster.Game.Services;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Components.Animation;
using WaveEngine.Components.Cameras;
using WaveEngine.Components.Gestures;
using WaveEngine.Components.Graphics2D;
using WaveEngine.Components.Graphics3D;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics2D;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.UI;
using WaveEngine.Materials;

namespace ToasterRoaster.Game.Scenes
{
    public class GameScene : Scene
    {
        private bool _started = false;

        protected override void CreateScene()
        {
            RenderManager.BackgroundColor = Color.CornflowerBlue;
            //RenderManager.DebugLines = true;
            
            TextBlock levelText = new TextBlock("levelText")
            {
                Foreground = Color.Black,
                Text = "Level " + WaveServices.GetService<LevelInformationService>().Level,
                Margin = new Thickness(20f),
                HorizontalAlignment = HorizontalAlignment.Left,
            };
            EntityManager.Add(levelText);

            TextBlock toasterPosition = new TextBlock("ToasterPosition")
            {
                Text = "Klicken Sie auf den Toaster um zu beginnen!",
                Margin = new Thickness(20, 50, 0, 0),
            };
            EntityManager.Add(toasterPosition);

            CreateLight();
            CreateCamera();
            CreateToaster();
			//CreatePlate();

			WaveServices.GetService<AnalyticsService>().TagEvent("Page opened", "Page", "In-game");
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
            FixedCamera mainCamera = new FixedCamera("MainCamera", new Vector3(0, 15, 25), Vector3.Zero);
            //FreeCamera mainCamera = new FreeCamera("MainCamera", new Vector3(0, 15, 25), Vector3.Zero);
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
            
            Entity toastModel = new Entity("toastModel")
                .AddComponent(new Transform3D())
                .AddComponent(new MaterialsMap(new BasicMaterial(Color.Gray)))
                .AddComponent(Model.CreateCube())
                .AddComponent(new SphereCollider())
                .AddComponent(new RigidBody3D() { Mass = 1, EnableContinuousContact = true })
                .AddComponent(new ModelRenderer());
            EntityManager.Add(toastModel);

            RigidBody3D rigidBody = toastModel.FindComponent<RigidBody3D>();
            rigidBody.ResetPosition(Vector3.Zero);
            rigidBody.ApplyLinearImpulse(25 * Vector3.UnitY);

            WaveServices.Input.TouchPanelState.Clear();
            
            EntityManager.Find<FixedCamera>("MainCamera").Entity.AddComponent(new FlightBehavior(toastModel));
            //EntityManager.Find<FixedCamera>("MainCamera").Entity.AddComponent(new RigidBody3D() { IsKinematic = true, });
        }
    }
}