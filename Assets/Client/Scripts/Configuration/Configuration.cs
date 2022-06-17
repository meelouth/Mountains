using UnityEngine;

namespace Client
{
    [CreateAssetMenu(menuName = "Configuration")]
    public class Configuration : ScriptableObject, IConfiguration
    {
        [SerializeField] private Vector3 defaultRotation = new(90, 0, 0);
        
        [Header("Camera Settings")]
        [SerializeField] private float cameraSpeed = 5f;
        [SerializeField] private float cameraMaxHeight = 40;
        [SerializeField] private float cameraMinHeight = -40;
        [SerializeField] private float cameraSmooth = 0.2f;
        [SerializeField] private float cameraDistance = 10f;
        [SerializeField] private float cameraMinDistance = 250;
        [SerializeField] private float cameraMaxDistance = 500;

        [Header("Map")] 
        [SerializeField] private Material heightMapMaterial;
        [SerializeField] private Gradient mapGradient;
        [SerializeField] private float modifyVertexSpeed = 2;
        [SerializeField] private int progressThreshold = 1000;

        public Vector3 DefaultRotation => defaultRotation;
        public float MouseSensitivity => cameraSpeed;
        public float CameraMaxHeight => cameraMaxHeight;
        public float CameraMinHeight => cameraMinHeight;
        public float CameraSmooth => cameraSmooth;
        public float CameraMaxDistance => cameraMaxDistance;
        public float CameraMinDistance => cameraMinDistance;
        public float CameraDistance => cameraDistance;
        public Material HeightMapMaterial => heightMapMaterial;
        public Gradient Gradient => mapGradient;
        public float ModifyVertexSpeed => modifyVertexSpeed;
        public int ProgressThreshold => progressThreshold;
    }
}