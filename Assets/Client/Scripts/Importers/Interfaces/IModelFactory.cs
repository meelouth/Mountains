using UnityEngine;

namespace Client
{
    public interface IModelFactory
    {
        ModelView Create(GameObject gameObject);
    }
}