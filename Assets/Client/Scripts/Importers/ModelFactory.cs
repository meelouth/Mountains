using UnityEngine;

namespace Client
{
    public class ModelFactory : IModelFactory
    {
        private readonly IConfiguration configuration;

        public ModelFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ModelView Create(GameObject gameObject)
        {
            var landscape = FindLandscape(gameObject);
            
            DeleteExtra(gameObject, landscape);

            var model = gameObject.AddComponent<ModelView>();
            landscape.AddComponent<MeshCollider>();
            
            var mesh = landscape.GetComponent<MeshFilter>().mesh;
            var meshRenderer = landscape.GetComponent<MeshRenderer>();
            meshRenderer.material = configuration.HeightMapMaterial;
            
            model.Initialize(mesh, landscape.transform);

            return model;
        }

        private static void DeleteExtra(GameObject model, GameObject mesh)
        {
            foreach (Transform child in model.transform)
            {
                if (child != mesh.transform)
                {
                    Object.Destroy(child.gameObject);
                }
            }
        }
        
        private static GameObject FindLandscape(GameObject gameObject)
        {
            var mesh = gameObject.transform.GetComponentInChildren<MeshRenderer>();

            return mesh.gameObject;
        }
    }
}