using System;
using UnityEngine;

namespace Client
{
    public interface IModelImporter : IInitializable
    {
        void LoadModel(Action<GameObject> onLoad, Action<string> onError);
    }
}