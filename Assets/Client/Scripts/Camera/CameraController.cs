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
        
        private Transform target;

        [Inject]
        private void Construct(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        public void RemoveTarget()
        {
            target = null;
        }
        
        private void Update()
        {
            if (target == null)
            {
                return;
            }
            
            Drag();
            Rotate();
        }


        private void Rotate()
        {
            rotationX = Mathf.Clamp(rotationY, configuration.CameraMinHeight, configuration.CameraMaxHeight);

            var nextRotation = new Vector3(rotationX, rotationY);

            currentRotation = Vector3.SmoothDamp(currentRotation, nextRotation, ref smoothVelocity,
                configuration.CameraSmooth);

            cameraTransform.localEulerAngles = currentRotation;

            cameraTransform.position = target.position - cameraTransform.forward * configuration.CameraDistance;
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