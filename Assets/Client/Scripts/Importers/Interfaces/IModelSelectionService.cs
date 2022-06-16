using System;
using UnityEngine;

namespace Client
{
    public interface IModelSelectionService
    {
        void LoadModel(Action<GameObject> onLoad, Action<string> onError);
    }
}