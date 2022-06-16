using System.Linq;
using UnityEngine;
using VContainer;

namespace Client
{
    public class Painter : MonoBehaviour, IPainter
    {
        private ICameraController cameraController;
        private IConfiguration configuration;

        [Inject]
        public void Construct(ICameraController cameraController, IConfiguration configuration)
        {
            this.cameraController = cameraController;
            this.configuration = configuration;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var ray = cameraController.Camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.transform.parent.TryGetComponent<ModelView>(out var model))
                    {
                        var triangleIndex = hit.triangleIndex;
                        
                        var triangle1 = model.Mesh.triangles[triangleIndex * 3 + 0];
                        var triangle2 = model.Mesh.triangles[triangleIndex * 3 + 1];
                        var triangle3 = model.Mesh.triangles[triangleIndex * 3 + 2];

                        var mesh = model.Mesh;
                        
                        model.Colors[triangle1] = Color.Lerp(model.Colors[triangle1],  
                            configuration.Gradient.Evaluate(GetTimeOfColor(configuration.Gradient,model.Colors[triangle1])) , Time.deltaTime * 5);
                        model.Colors[triangle2] = Color.Lerp(model.Colors[triangle2], 
                            configuration.Gradient.Evaluate(GetTimeOfColor(configuration.Gradient,model.Colors[triangle2])) , Time.deltaTime * 5);
                        model.Colors[triangle3] =Color.Lerp(model.Colors[triangle3], 
                            configuration.Gradient.Evaluate(GetTimeOfColor(configuration.Gradient,model.Colors[triangle3])) , Time.deltaTime * 5);

                        mesh.colors = model.Colors;
                    }
                }
            }
        }

        private float GetTimeOfColor(Gradient gradient, Color color)
        {
            var gradientKey = gradient.colorKeys.FirstOrDefault(x => x.color == color);

            return gradientKey.time;
        }
    }
}