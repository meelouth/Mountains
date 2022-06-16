using System;
using TriLibCore;
using UnityEngine;

namespace Client
{
    public class ModelImporter : IModelImporter
    {
        private AssetLoaderOptions options;
        
        public void Initialize()
        {
            CreateOptions();
        }
        
        public void LoadModel(Action<GameObject> onLoad, Action<string> onError)
        {
            var filePickerAssetLoader = AssetLoaderFilePicker.Create();
            
            filePickerAssetLoader.LoadModelFromFilePickerAsync("Select a File", context => { onLoad?.Invoke(context.RootGameObject);}, null,
                null, null, error => onError?.Invoke(error.ToString()), null, options);
        }

        private void CreateOptions()
        {
            options = AssetLoader.CreateDefaultLoaderOptions();
        }
    }
}