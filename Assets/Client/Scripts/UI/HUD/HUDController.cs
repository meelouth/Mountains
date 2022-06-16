using UnityEngine;

namespace Client
{
    public class HUDController : IHUDController
    {
        private readonly HUDView hudView;
        private readonly IModelSelectionService modelSelector;
        private readonly IAutomaticPainter automaticPainter;
        private readonly ITableService tableService;

        public HUDController(HUDView hudView, IModelSelectionService modelSelector, IAutomaticPainter automaticPainter,
            ITableService tableService)
        {
            this.hudView = hudView;
            this.modelSelector = modelSelector;
            this.automaticPainter = automaticPainter;
            this.tableService = tableService;
        }
        
        public void Initialize()
        {
            hudView.OnLoadButtonClicked += OnLoadButtonClicked;
            hudView.OnPaintButtonClicked += OnPaintButtonClicked;
        }

        private void OnPaintButtonClicked()
        {
            automaticPainter.Paint(tableService.GetPlacedModel());
        }

        private void OnLoadButtonClicked()
        {
            modelSelector.LoadModel(OnLoad, OnError);
        }

        private void OnLoad(GameObject gameObject)
        {
            
        }

        private void OnError(string error)
        {
            Debug.LogError("Not right format.");
        }
    }
}