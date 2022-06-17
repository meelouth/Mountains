using UnityEngine;

namespace Client
{
    public class HUDController : IHUDController
    {
        private readonly HUDView hudView;
        private readonly IModelSelectionService modelSelector;
        private readonly IAutomaticPainter automaticPainter;
        private readonly ITableService tableService;
        
        private readonly ICameraController cameraController;
        private readonly IPainter painter;

        public HUDController(HUDView hudView, IModelSelectionService modelSelector, IAutomaticPainter automaticPainter, 
            ITableService tableService, ICameraController cameraController, IPainter painter)
        {
            this.hudView = hudView;
            this.modelSelector = modelSelector;
            this.automaticPainter = automaticPainter;
            this.tableService = tableService;
            this.cameraController = cameraController;
            this.painter = painter;
        }

        public void Initialize()
        {
            hudView.OnLoadButtonClicked += OnLoadButtonClicked;
            hudView.OnPaintButtonClicked += OnPaintButtonClicked;
            hudView.OnCameraToggleUpdated += OnCameraToggleUpdated;
            hudView.OnHandToggleUpdated += OnHandToggleUpdated;
        }

        private void OnHandToggleUpdated(bool state)
        {
            if (state)
                painter.Enable();
            else
                painter.Disable();
        }

        private void OnCameraToggleUpdated(bool state)
        {
            if (state)
                cameraController.Enable();
            else
                cameraController.Disable();
        }
        
        private void OnPaintButtonClicked()
        {
            var progressWindow = hudView.OpenProgressWindow();
            
            hudView.DisableToggles();
            
            automaticPainter.Paint(tableService.GetPlacedModel(), progress =>
            {
                progressWindow.SetTitle(progress.State.ToString());
                progressWindow.SetProgress(progress.Percent);
            }, () =>
            {
                progressWindow.Close();
                hudView.EnableToggles();
            });
        }

        private void OnLoadButtonClicked()
        {
            hudView.DisableToggles();
            
            modelSelector.LoadModel(OnLoad, OnError);
        }

        private void OnLoad(GameObject gameObject)
        {
            hudView.SetStatePaintButton(true);
            hudView.EnableCameraToggle();
        }
        

        private void OnError(string error)
        {
            hudView.DisableToggles();
            Debug.LogError("Not right format.");
        }
    }
}