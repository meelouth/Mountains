using UnityEngine;

namespace Client
{
    public class Window : MonoBehaviour
    {
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}