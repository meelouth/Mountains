using System;
using UnityEngine;
using VContainer;

namespace Client
{
    public class TableService : MonoBehaviour, ITableService
    {
        [SerializeField] private Transform modelRoot;

        private IConfiguration configuration;

        private ModelView placedModel;
        
        public event Action<ModelView> OnPlacedModel;

        [Inject]
        public void Construct(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public void PlaceModel(ModelView model)
        {
            RemoveModel();

            placedModel = model;
            model.RootCachedTransform.SetParent(modelRoot);
            SetDefault(model);
            OnPlacedModel?.Invoke(model);
        }

        public ModelView GetPlacedModel()
        {
            return placedModel;
        }

        private void RemoveModel()
        {
            if (placedModel != null)
                Destroy(placedModel.gameObject);
        }

        private void SetDefault(ModelView model)
        {
            model.RootCachedTransform.rotation = Quaternion.Euler(configuration.DefaultRotation);
            model.RootCachedTransform.position = Vector3.zero;
            model.LandScapeCachedTransform.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
}