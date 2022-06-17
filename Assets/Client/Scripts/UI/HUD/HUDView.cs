using System;
using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Client
{
    public class HUDView : MonoBehaviour
    {
        [SerializeField] private Button loadButton;
        [SerializeField] private Button paintButton;
        [SerializeField] private RectTransform windowRoot;
        [SerializeField] private ProgressWindow progressWindowPrefab;

        [SerializeField] private ToggleButtonGroup toggleGroup;
        [SerializeField] private ToggleButton cameraToggle;
        [SerializeField] private ToggleButton handToggle;

        public event Action OnLoadButtonClicked;
        public event Action OnPaintButtonClicked;
        public event Action<bool> OnCameraToggleUpdated;
        public event Action<bool> OnHandToggleUpdated;

        private void Start()
        {
            toggleGroup.RegisterToggle(cameraToggle);
            toggleGroup.RegisterToggle(handToggle);

            cameraToggle.SetInteractable(false);
            handToggle.SetInteractable(false);

            paintButton.interactable = false;
        }

        private void OnEnable()
        {
            loadButton.onClick.AddListener(OnLoadButtonClick);
            paintButton.onClick.AddListener(OnPaintButtonClick);
            cameraToggle.checkedChanged.AddListener(OnCameraToggleChanged);
            handToggle.checkedChanged.AddListener(OnHandToggleChanged);
        }

        private void OnDisable()
        {
            loadButton.onClick.RemoveListener(OnLoadButtonClick);
            paintButton.onClick.RemoveListener(OnPaintButtonClick);
            cameraToggle.checkedChanged.RemoveListener(OnCameraToggleChanged);
            handToggle.checkedChanged.RemoveListener(OnHandToggleChanged);
        }

        public void SetStatePaintButton(bool state)
        {
            paintButton.interactable = state;
        }
        
        public void EnableCameraToggle()
        {
            cameraToggle.SetInteractable(true);
        }
        
        public void DisableToggles()
        {
            toggleGroup.SetAllTogglesOff();
            
            cameraToggle.SetInteractable(false);
            handToggle.SetInteractable(false);

            cameraToggle.Checked = false;
            handToggle.Checked = false;
        }
        
        public void EnableToggles()
        {
            cameraToggle.SetInteractable(true);
            handToggle.SetInteractable( true);
        }

        public ProgressWindow OpenProgressWindow()
        {
            return Instantiate(progressWindowPrefab, windowRoot);
        }
        
        private void OnCameraToggleChanged(ToggleButton button)
        {
            OnCameraToggleUpdated?.Invoke(button.Checked);
        }

        private void OnHandToggleChanged(ToggleButton button)
        {
            OnHandToggleUpdated?.Invoke(button.Checked);
        }
        
        private void OnLoadButtonClick()
        {
            OnLoadButtonClicked?.Invoke();
        }

        private void OnPaintButtonClick()
        {
            OnPaintButtonClicked?.Invoke();
        }
    }
}