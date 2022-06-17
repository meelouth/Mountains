using UnityEngine;

namespace Client
{
    public interface ICameraController : ITool
    {
        Camera Camera { get; }
        void SetTarget(Transform newTarget);
        void SetDistance(float value);
    }
}