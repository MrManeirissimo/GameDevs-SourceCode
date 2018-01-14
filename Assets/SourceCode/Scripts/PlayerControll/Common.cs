using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameDevsSourceCode
{
    namespace InputHandling
    {
        public enum KeyState
        {
            OnPressed, OnDown, OnUp
        }

        [System.Serializable]
        public class KeyConfiguration
        {
            [SerializeField] private string _name;
            [SerializeField] private KeyCode _key;
            [SerializeField] private KeyState _state;

            public string Name { get { return _name; } set { _name = value; } }
            public KeyCode Key { get { return _key; } set { _key = value; } }
            public KeyState KeyState { get { return _state; } set { _state = value; } }

            public KeyConfiguration(string name, KeyCode key, KeyState state)
            {
                this._name = name;
                this. _key = key;
                this._state = state;
            }
        }
    }
}