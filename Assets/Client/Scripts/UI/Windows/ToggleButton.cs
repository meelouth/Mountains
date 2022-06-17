using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Client
{
    public class ToggleButton : MonoBehaviour, IPointerClickHandler
    {
        public ToggleEvent checkedChanged = new ToggleEvent();

        private Image image;
        private Color originalColor;

        private bool @checked;
        [SerializeField] private Color checkedColor;
        [SerializeField] private Color uninteractableColor;
        [SerializeField] private ToggleButtonGroup @group;

        private bool interactable = true;
        
        [SerializeField]
        public bool Checked
        {
            get
            {
                return @checked;
            }
            set
            {
                if (@checked != value)
                {
                    @checked = value;
                    UpdateVisual();
                    checkedChanged.Invoke(this);
                }
            }
        }

        private void Start()
        {
            image = GetComponent<Image>();
            originalColor = image.color;

            if (@group != null)
                @group.RegisterToggle(this);
        }

        public void SetInteractable(bool state)
        {
            interactable = state;

            if (state)
            {
                image.color = originalColor;
                return;
            }

            image.color = uninteractableColor;
        }

        private void UpdateVisual()
        {
            image.color = Checked ? checkedColor : originalColor;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!interactable)
            {
                return;
            }
            
            Checked = !Checked;
        }

        [Serializable]
        public class ToggleEvent : UnityEvent<ToggleButton>
        {
        }
    }
}