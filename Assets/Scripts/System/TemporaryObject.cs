using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Urxxx.System
{
    public class TemporaryObject : MonoBehaviour
    {
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}