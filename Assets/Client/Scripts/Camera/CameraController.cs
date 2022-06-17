using System;
using UnityEngine;
using VContainer;

namespace Client
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Camera camera;

        public Camera Camera => camera;

        private IConfiguration configuration;
        
        private const int MouseButton = 1;
        
        private Vector3 currentRotation;
        private Vector3 smoothVelocity;
        private float rotationX;
        private float rotationY;

        private float cameraDistance;
        
        private Transform target;

        private bool enabled;

        [Inject]
        private void Construct(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        private void Start()
        {
            SetDistance(configuration.CameraMaxDistance);
        }

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            Rotate();
        }

        public void SetDistance(float value)
        {
            var distance = Mathf.Lerp(configuration.CameraMaxDistance, configuration.CameraMinDistance, value);
            cameraDistance = distance;
        }

        public void RemoveTarget()
        {
            target = null;
        }
        
        public void Enable()
        {
            enabled = true;
        }

        public void Disable()
        {
            enabled = false;
        }
        
        private void Update()
        {
            if (target == null)
            {
                return;
            }
            
            if (enabled)
            {
                Drag();
            }
            
            Rotate();
        }


        private void Rotate()
        {
            rotationX = Mathf.Clamp(rotationY, configuration.CameraMinHeight, configuration.CameraMaxHeight);

            var nextRotation = new Vector3(rotationX, rotationY);

            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity,
                configuration.CameraSmooth);

            cameraTransform.localEulerAngles = currentRotation;

            cameraTransform.position = target.position - cameraTransform.forward * cameraDistance;
        }
        
        private void Drag()
        {
            if (Input.GetMouseButton(MouseButton))
            {
                var mouseX = Input.GetAxis("Mouse X") * configuration.MouseSensitivity;
                var mouseY = Input.GetAxis("Mouse Y") * configuration.MouseSensitivity;

                rotationX += mouseY;
                rotationY += mouseX;
            }
        }
    }
}