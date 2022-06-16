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

        public event Action OnLoadButtonClicked;
        public event Action OnPaintButtonClicked;

        private void OnEnable()
        {
            loadButton.onClick.AddListener(OnLoadButtonClick);
            paintButton.onClick.AddListener(OnPaintButtonClick);
        }

        private void OnDisable()
        {
            loadButton.onClick.RemoveListener(OnLoadButtonClick);
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