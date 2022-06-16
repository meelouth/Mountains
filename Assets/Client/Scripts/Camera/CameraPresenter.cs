using VContainer.Unity;

namespace Client
{
    public class CameraPresenter : IStartable
    {
        private readonly ICameraController cameraController;
        private readonly ITableService tableService;

        public CameraPresenter(ICameraController cameraController, ITableService tableService)
        {
            this.cameraController = cameraController;
            this.tableService = tableService;
        }
        
        public void Start()
        {
            tableService.OnPlacedModel += OnPlacedModel;
        }

        private void OnPlacedModel(ModelView model)
        {
            cameraController.SetTarget(model.RootCachedTransform);
        }
    }
}