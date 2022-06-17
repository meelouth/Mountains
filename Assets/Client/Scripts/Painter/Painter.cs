using System.Linq;
using UnityEngine;
using VContainer;

namespace Client
{
    public class Painter : IPainter
    {
        private ICameraController cameraController;
        private IConfiguration configuration;
        private IUserInputController userInputController;

        private const int Positive = 1;
        private const int Negative = -1;

        [Inject]
        public void Construct(ICameraController cameraController, IConfiguration configuration, IUserInputController userInputController)
        {
            this.cameraController = cameraController;
            this.configuration = configuration;
            this.userInputController = userInputController;
        }

        public void Enable()
        {
            userInputController.OnLeftMouseHold += OnLeftMouseHold;
            userInputController.OnRightMouseHold += OnRightMouseHold;
        }

        public void Disable()
        {
            userInputController.OnLeftMouseHold -= OnLeftMouseHold;
            userInputController.OnRightMouseHold -= OnRightMouseHold;
        }

        private void OnLeftMouseHold()
        {
            Extrude();
        }

        private void OnRightMouseHold()
        {
            Dent();
        }

        private void Extrude()
        {
            Manipulate(Positive);
        }

        private void Dent()
        {
            Manipulate(Negative);
        }

        private void Manipulate(int direction)
        {
            var ray = cameraController.Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.transform.parent.TryGetComponent<ModelView>(out var model))
                {
                    var triangleIndex = hit.triangleIndex;

                    var vertices = GetVerticesByTriangle(model.Mesh, triangleIndex);

                    foreach (var vertex in vertices)
                    {
                        var vertexData = model.Vertices[vertex];

                        ModifyVertex(model, vertexData, direction);
                        
                        var height = Mathf.InverseLerp(model.LowestPoint,model.HighestPoint,vertexData.Height);
                        var nextColor = configuration.Gradient.Evaluate(height);
                        model.Colors[vertex] = nextColor;

                        model.Mesh.colors = model.Colors;
                    }
                }
            }
        }

        private void ModifyVertex(ModelView model, Vertex vertex, int direction)
        {
            var shift = GetShift(model, direction);

            vertex.Height = Mathf.Lerp(vertex.Height, shift, configuration.ModifyVertexSpeed * Time.deltaTime);
        }

        private static float GetShift(ModelView model, int direction)
        {
            return direction > 0 ? model.HighestPoint : model.LowestPoint;
        }

        private static int[] GetVerticesByTriangle(Mesh mesh, int triangleIndex)
        {
            var vertices = new int[3];
            
            var vertex1 = mesh.triangles[triangleIndex * 3 + 0];
            var vertex2 = mesh.triangles[triangleIndex * 3 + 1];
            var vertex3 = mesh.triangles[triangleIndex * 3 + 2];

            vertices[0] = vertex1;
            vertices[1] = vertex2;
            vertices[2] = vertex3;

            return vertices;
        }
    }
}