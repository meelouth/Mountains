using System;
using UnityEngine;

namespace Client
{
    public class AutomaticPainter : IAutomaticPainter
    {
        private readonly IConfiguration configuration;

        public AutomaticPainter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Paint(ModelView model)
        {
            var mesh = model.Mesh;

            model.Colors = new Color[mesh.vertices.Length];

            var (min, max) = FindMinMaxY(mesh);

            Debug.Log($"Min : {min}");
            Debug.Log($"Max : {max}");
            
            for (var index = 0; index < mesh.vertices.Length; index++)
            {
                var vert = mesh.vertices[index];
                var height = Mathf.InverseLerp(min,max,vert.z);
                model.Colors[index] = configuration.Gradient.Evaluate(height);
            }
            
            mesh.colors = model.Colors;
        }

        private static Tuple<float, float> FindMinMaxY(Mesh mesh)
        {
            var min = float.MaxValue;
            var max = float.MinValue;
            
            for (var index = 0; index < mesh.vertices.Length; index++)
            {
                var vert = mesh.vertices[index];

                if (vert.z > max)
                {
                    max = vert.z;
                }

                if (vert.z < min)
                {
                    min = vert.z;
                }
            }

            return new Tuple<float, float>(min, max);
        }
    }
}