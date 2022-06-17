using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class ModelView : MonoBehaviour
    {
        public Transform RootCachedTransform { get; private set; }
        public Transform LandScapeCachedTransform { get; private set; }
        public Mesh Mesh { get; private set; }
        public float HighestPoint { get; set; }
        public float LowestPoint { get; set; }
        public Dictionary<int, Vertex> Vertices { get; set; }
        public Color[] Colors { get; set; }

        private void Awake()
        {
            RootCachedTransform = transform;
        }

        public void Initialize(Mesh mesh, Transform landTransform)
        {
            Mesh = mesh;
            LandScapeCachedTransform = landTransform;
        }
    }
}