using System;
using UnityEngine;

namespace Client
{
    public class ModelSelectionService : IModelSelectionService
    {
        private readonly IModelImporter modelImporter;
        private readonly ITableService tableService;
        private readonly IModelFactory modelFactory;

        public ModelSelectionService(IModelImporter modelImporter, ITableService tableService, IModelFactory modelFactory)
        {
            this.modelImporter = modelImporter;
            this.tableService = tableService;
            this.modelFactory = modelFactory;
        }

        public void LoadModel(Action<GameObject> onLoad, Action<string> onError)
        {
            modelImporter.LoadModel(gameObject =>
            {
                var model = modelFactory.Create(gameObject);
                
                tableService.PlaceModel(model);
                
                onLoad?.Invoke(gameObject);
            }, OnError);
        }

        private void OnError(string error)
        {
            Debug.LogError("Wrong format. Error : " + error);
        }
    }
}