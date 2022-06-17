using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client
{
    public class ToggleButtonGroup : MonoBehaviour
    {
        List<ToggleButton> toggles = new List<ToggleButton>();

        public void RegisterToggle(ToggleButton toggle)
        {
            toggles.Add(toggle);
            toggle.checkedChanged.AddListener(HandleCheckedChanged);
        }

        private void HandleCheckedChanged(ToggleButton toggle)
        {
            if (toggle.Checked)
            {
                foreach (var item in toggles)
                {
                    if (item.GetInstanceID() != toggle.GetInstanceID())
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        public void SetAllTogglesOff()
        {
            foreach (var toggle in toggles)
            {
                toggle.Checked = false;
            }
        }
    }
}