using System;
using UnityEngine;

namespace Client
{
    public class UserInput : MonoBehaviour, IUserInputController
    {
        public event Action OnLeftMouseHold;
        public event Action OnRightMouseHold;

        private const int LeftMouseButtonKey = 0;
        private const int RightMouseButtonKey = 1;
        
        private void Update()
        {
            if (Input.GetMouseButton(LeftMouseButtonKey))
                OnLeftMouseHold?.Invoke();
            
            if (Input.GetMouseButton(RightMouseButtonKey))
                OnRightMouseHold?.Invoke();
        }
    }
}