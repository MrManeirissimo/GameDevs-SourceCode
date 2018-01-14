using UnityEngine;

using System.Collections.Generic;

using GameDevsSourceCode.InputHandling.ActionSystem;


namespace GameDevsSourceCode
{
    namespace InputHandling
    {
        [CreateAssetMenu(fileName = "Controll Scheme", menuName = "Input Handling/Controll Scheme")]
        public class ControllScheme : ScriptableObject
        {
            [SerializeField] protected Dictionary<KeyConfiguration, List<Action>> _controllScheme;

            #region Accessors

            public Dictionary<KeyConfiguration, List<Action>> Scheme { get { return _controllScheme; } }

            #endregion


            #region Public methods

            public void AddKeyToControllScheme(KeyConfiguration key)
            {
                if (_controllScheme == null)
                    _controllScheme = new Dictionary<KeyConfiguration, List<Action>>();

                _controllScheme.Add(key, new List<Action>());
            }

            public void ClearControllScheme()
            {
                if(_controllScheme != null)
                {
                    _controllScheme.Clear();
                }
            }

            public void AddEmptyActionToKey(KeyConfiguration key)
            {
                if (_controllScheme == null)
                    throw new System.Exception("@" + GetType() + " - Controll scheme is null. Could not add action to key");

                if(_controllScheme.ContainsKey(key))
                {
                    _controllScheme[key].Add(new EmptyAction());
                }
            }

            public void SetActionToKey(Action action, KeyConfiguration key, int index)
            {
                if (_controllScheme == null)
                    return;

                if(_controllScheme.ContainsKey(key))
                {
                    if (index < 0 || index >= _controllScheme[key].Count)
                        throw new System.Exception("@" + GetType() + " - Index out of range. Could not assign action to index: " + index + " on key: " + key);

                    _controllScheme[key][index] = action;
                }
            }

            #endregion


            ///Conmment Section
            //[SerializeField] protected List<KeyConfiguration> _keyList;
            //[SerializeField] protected List<List<Action>> _actions;
            //[SerializeField] protected Dictionary<KeyConfiguration, List<Action>> _controllScheme;

            //#region Accesssors

            //public KeyConfiguration[] KeyList
            //{
            //    get
            //    {
            //        if(_keyList == null)
            //        {
            //            _keyList = new List<KeyConfiguration>();
            //        }

            //        return _keyList.ToArray();
            //    }
            //}
            //public Dictionary<KeyConfiguration, List<Action>> Scheme
            //{
            //    get
            //    {
            //        if(_controllScheme == null)
            //        {
            //            _controllScheme = new Dictionary<KeyConfiguration, List<Action>>();
            //        }

            //        return _controllScheme;
            //    }
            //}

            //#endregion


            //#region Public methods

            //public void AddKey(string name, KeyCode key, KeyState state)
            //{
            //    if (_keyList == null)
            //        _keyList = new List<KeyConfiguration>();

            //    if (_controllScheme == null)
            //        _controllScheme = new Dictionary<KeyConfiguration, List<Action>>();

            //    if (_actions == null)
            //        _actions = new List<List<Action>>();

            //    KeyConfiguration __key = new KeyConfiguration(name, key, state);

            //    _keyList.Add(__key);
            //    _controllScheme.Add(__key, new List<Action>());
            //    _actions.Add(new List<Action>());
            //}

            //public void RemoveKey(int keyIndex)
            //{
            //    if (keyIndex < 0 || keyIndex >= KeyList.Length)
            //        throw new System.Exception("@" + GetType() + " - Out of range exception. Index cannot fit inside KeyList container");

            //    _keyList.RemoveAt(keyIndex);
            //    //KeyList.RemoveAll(item => item == null);
            //}

            //public void ClearControllScheme()
            //{
            //    if(_keyList != null)
            //    {
            //        _keyList.Clear();
            //    }

            //    if(_controllScheme != null)
            //    {
            //        _controllScheme.Clear();
            //    }

            //    if(_actions != null)
            //    {
            //        _actions.Clear();
            //    }
            //}

            //public void AddEmptyActionToKey(KeyConfiguration key)
            //{
            //    if (_controllScheme == null)
            //        return;

            //    if(_controllScheme.ContainsKey(key))
            //    {
            //        _controllScheme[key].Add(new EmptyAction());
            //    }


            //}

            //#endregion
        }
    }
}