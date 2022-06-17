using UnityEngine;

namespace Client
{
    public interface IConfiguration
    {
        Vector3 DefaultRotation { get; }
        float MouseSensitivity { get; }
        float CameraMaxHeight { get; }
        float CameraMinHeight { get; } 
        float CameraSmooth { get; }
        float CameraMaxDistance { get; }
        float CameraMinDistance { get; }
        Material HeightMapMaterial { get; }
        Gradient Gradient { get; }
        float ModifyVertexSpeed { get; }
        int ProgressThreshold { get; }
    }
}