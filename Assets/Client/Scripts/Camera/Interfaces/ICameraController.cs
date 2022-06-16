using UnityEngine;

namespace Client
{
    public interface ICameraController
    {
        Camera Camera { get; }
        void SetTarget(Transform newTarget);
    }
}