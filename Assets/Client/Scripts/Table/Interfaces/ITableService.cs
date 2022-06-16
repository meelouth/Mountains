using System;

namespace Client
{
    public interface ITableService
    {
        event Action<ModelView> OnPlacedModel;
        
        void PlaceModel(ModelView model);
        ModelView GetPlacedModel();
    }
}