using System.Collections.Generic;

using UnityEngine;

namespace GameDevsSourceCode
{
    namespace InputHandling
    {
        public class PlayerController : MonoBehaviour
        {
            [Header("Object references")]
            [SerializeField] protected GameObject _target;

            protected virtual void Awake()
            {
                if (!_target) _target = gameObject;
            }

            protected virtual void Update()
            {

            }
        }
    }
}