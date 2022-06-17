using System;
using Michsky.UI.ModernUIPack;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Client
{
    public class CameraDistanceController : MonoBehaviour, ICameraDistanceController
    {
        [SerializeField] private Slider slider;
        
        private IConfiguration configuration;
        private ICameraController cameraController;
        
        [Inject]
        private void Construct(IConfiguration configuration, ICameraController cameraController)
        {
            this.configuration = configuration;
            this.cameraController = cameraController;
        }

        private void OnEnable()
        {
            slider.onValueChanged.AddListener(OnSliderUpdated);
        }

        private void OnDisable()
        {
            slider.onValueChanged.RemoveListener(OnSliderUpdated);
        }

        private void Start()
        {
            slider.value = slider.maxValue;
        }

        private void OnSliderUpdated(float value)
        {
            cameraController.SetDistance(value);
        }
    }
}