using System;

namespace Client
{
    public interface IUserInputController
    {
        event Action OnLeftMouseHold;
        event Action OnRightMouseHold;
    }
}