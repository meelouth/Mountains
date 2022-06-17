using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Client
{
    public class ApplicationLifetimeScope : LifetimeScope
    {
        [SerializeField] private CameraController cameraController;
        [SerializeField] private TableService tableService;
        [SerializeField] private Configuration configuration;
        [SerializeField] private HUDView hudView;
        [SerializeField] private UserInput userInput;
        [SerializeField] private AutomaticPainter automaticPainter;
        [SerializeField] private CameraDistanceController cameraDistanceController;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IModelSelectionService, ModelSelectionService>(Lifetime.Singleton);
            builder.Register<IModelImporter, ModelImporter>(Lifetime.Singleton);
            builder.Register<IHUDController, HUDController>(Lifetime.Singleton);
            builder.Register<IModelFactory, ModelFactory>(Lifetime.Singleton);
            builder.Register<IPainter, Painter>(Lifetime.Singleton);

            builder.RegisterComponent(hudView);
            builder.RegisterComponent(cameraController).As<ICameraController>();
            builder.RegisterComponent(tableService).As<ITableService>();
            builder.RegisterComponent(configuration).As<IConfiguration>();
            builder.RegisterComponent(userInput).As<IUserInputController>();
            builder.RegisterComponent(automaticPainter).As<IAutomaticPainter>();
            builder.RegisterComponent(cameraDistanceController).As<ICameraDistanceController>();

            builder.RegisterEntryPoint<HUDPresenter>();
            builder.RegisterEntryPoint<CameraPresenter>();
        }
    }
}    

