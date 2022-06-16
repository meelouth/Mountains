using System.Linq;
using UnityEngine;

namespace Client
{
    public class Colorize : MonoBehaviour
    {
        [SerializeField] private Gradient gradient;
        [SerializeField] private float min;
        [SerializeField] private float max;
        
        public void Awake()
        {
            var mesh = GetComponent<MeshFilter>().mesh;

            var colors = new Color[mesh.vertices.Length];

            for (var index = 0; index < mesh.vertices.Length; index++)
            {
                var vert = mesh.vertices[index];
                var height = Mathf.InverseLerp(min,max,vert.y);
                colors[index] = gradient.Evaluate(height);
                Debug.Log(vert.y);
            }
            
            Debug.Log("MAX " + mesh.vertices.Max(x => x.y));

            mesh.colors = colors;
        }
    }
}