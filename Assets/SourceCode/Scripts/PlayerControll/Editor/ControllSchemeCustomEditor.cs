using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using GameDevsSourceCode.Utility;
using GameDevsSourceCode.InputHandling;
using GameDevsSourceCode.InputHandling.ActionSystem;

namespace GameDevsSourceCode
{
    namespace EditorScripts
    {
        namespace InputHandling
        {
            [CustomEditor(typeof(ControllScheme))]
            public class ControllSchemeCustomEditor : Editor
            {
                #region Member variables

                //Target script
                private ControllScheme _controllScheme;

                //Containers
                private static TwoWayList<string, int> _inGameAction_Name_Index;
                private static TwoWayList<string, int> _inGameAction_FullName_Index;
                private static TwoWayList<KeyConfiguration, List<int>> _selectedActions;

                //Colors
                private static Color _backgroundColor = new Color(0.6f, 0.6f, 0.6f, 1);

                #endregion

                #region Core methods

                //Buttons - ADD
                private void AddKeyButton()
                {
                    if(GUILayout.Button("Add Key"))
                    {
                        _controllScheme.AddKeyToControllScheme(new KeyConfiguration("New key", 0, 0));

                        UpdateAction_IntIndexation();
                    }
                }
                private void AddActionButton(KeyConfiguration key)
                {
                    if(GUILayout.Button("Add Action"))
                    {
                        _controllScheme.AddEmptyActionToKey(key);

                        UpdateAction_IntIndexation();
                    }
                }

                //Buttons - REMOVE
                private void RemoveActionButton()
                {
                    if(GUILayout.Button("Remove Action"))
                    {

                    }
                }

                //Display - KEYS
                private void DisplayKeyConfigurations()
                {
                    if (_controllScheme.Scheme == null)
                        return;

                    foreach (var KEY_CONFIG  in _controllScheme.Scheme.Keys)
                    {
                        GUI.color = _backgroundColor;

                        GUILayout.BeginVertical("Box");

                        GUI.color = Color.white;
                        {
                            GUIStyle _normalLabelStyle;
                            _normalLabelStyle = GUI.skin.GetStyle("Label");
                            _normalLabelStyle.alignment = TextAnchor.MiddleLeft;
                            _normalLabelStyle.fontStyle = FontStyle.Normal;


                            KEY_CONFIG.Name = GUILayout.TextField(KEY_CONFIG.Name);


                            GUI.color = _backgroundColor;
                            GUILayout.BeginHorizontal("Box");
                            {
                                GUI.color = Color.white;

                                GUILayout.Label("Key", _normalLabelStyle);
                                KEY_CONFIG.Key = (KeyCode)EditorGUILayout.EnumPopup(KEY_CONFIG.Key);

                                GUILayout.Space(50);

                                GUILayout.Label("Key state", _normalLabelStyle);
                                KEY_CONFIG.KeyState = (KeyState)EditorGUILayout.EnumPopup(KEY_CONFIG.KeyState);
                            }
                            GUILayout.EndHorizontal();

                            GUI.color = _backgroundColor;
                            GUILayout.BeginHorizontal("Box");
                            {
                                GUI.color = Color.white;

                                AddActionButton(KEY_CONFIG);
                                RemoveActionButton();
                            }
                            GUILayout.EndHorizontal();

                            DisplayActionsByKey(KEY_CONFIG);
                        }
                        GUILayout.EndVertical();
                    }
                }

                //Display - ACTIONS
                private void DisplayActionsByKey(KeyConfiguration key)
                {
                    GUILayout.BeginVertical();
                    {
                        for (int i = 0; i < _controllScheme.Scheme[key].Count; i++)
                        {
                            string __curretnActionName = _controllScheme.Scheme[key][i].GetType().Name;

                            int __selectedOption = EditorGUILayout.Popup(_selectedActions[key][i], _inGameAction_Name_Index.Keys.ToArray());

                            if(_selectedActions[key][i] != __selectedOption)
                            {
                                _selectedActions[key][i] = __selectedOption;

                                Action __action = FindActionByName(_inGameAction_FullName_Index[__selectedOption]);

                                _controllScheme.SetActionToKey(__action, key, i);
                            }
                        }
                    }
                    GUILayout.EndVertical();
                }

                private void CreateActionName_IntIndexation()
                {
                    //Creates a container and stores the names of the founded actions coded in the assemblies
                    List<System.Type> __actionTypes = new List<System.Type>();
                    foreach (var assbly in System.AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (var type in assbly.GetTypes())
                        {
                            foreach (var xtendInterface in type.GetInterfaces())
                            {
                                if(xtendInterface == typeof(GameDevsSourceCode.InputHandling.ActionSystem.Action))
                                {
                                    __actionTypes.Add(type);
                                }
                            }
                        }
                    }

                    //Links the names to and index for simplicity sake in the future
                    _inGameAction_Name_Index = new TwoWayList<string, int>();
                    _inGameAction_FullName_Index = new TwoWayList<string, int>();
                    for (int i = 0; i < __actionTypes.Count; i++)
                    {
                        _inGameAction_Name_Index.Add(__actionTypes[i].Name, i);
                        _inGameAction_FullName_Index.Add(__actionTypes[i].FullName, i);
                    }
                }

                private void UpdateAction_IntIndexation()
                {
                    if (_controllScheme.Scheme == null)
                        return;

                    //Initializes the container of selected actions 
                    _selectedActions = new TwoWayList<KeyConfiguration, List<int>>();

                    foreach (var KEY_CONFIG in _controllScheme.Scheme.Keys)
                    {
                        _selectedActions.Add(KEY_CONFIG, new List<int>());
                        for (int i = 0; i < _controllScheme.Scheme[KEY_CONFIG].Count; i++)
                        {
                            _selectedActions[KEY_CONFIG].Add(_inGameAction_FullName_Index[_controllScheme.Scheme[KEY_CONFIG][i].GetType().FullName]);
                        }
                    }
                }

                #endregion

                #region Funcitonal methods

                private void ClearButton()
                {
                    if(GUILayout.Button("Clear"))
                    {
                        _controllScheme.ClearControllScheme();
                    }
                }
                private void DrawTittle()
                {
                    GUI.color = _backgroundColor;

                    GUIStyle _boldCenteredLabelStyle;
                    _boldCenteredLabelStyle = GUI.skin.GetStyle("Label");
                    _boldCenteredLabelStyle.alignment = TextAnchor.UpperCenter;
                    _boldCenteredLabelStyle.fontStyle = FontStyle.Bold;

                    GUILayout.BeginHorizontal("Box");
                    {
                        GUI.color = Color.white;

                        GUILayout.Label("Key Schemes", _boldCenteredLabelStyle);
                    }
                    GUILayout.EndHorizontal();
                }
                private Action FindActionByName(string actionName)
                {
                    foreach (var assbly in System.AppDomain.CurrentDomain.GetAssemblies())
                    {
                        foreach (var type in assbly.GetTypes())
                        {
                            if(type.FullName == actionName)
                            {
                                return ((Action)System.Activator.CreateInstance(type));
                            }
                        }
                    }

                    return null;
                }

                #endregion

                #region Unity bits

                private void OnEnable()
                {
                    _controllScheme = (ControllScheme)target;

                    CreateActionName_IntIndexation();
                    UpdateAction_IntIndexation();
                }

                public override void OnInspectorGUI()
                {
                    SerializedProperty __monoScript = serializedObject.FindProperty("m_Script");
                    EditorGUILayout.PropertyField(__monoScript, false);

                    GUILayout.BeginVertical("Box");

                    ClearButton();

                    DrawTittle();

                    AddKeyButton();

                    DisplayKeyConfigurations();

                    GUILayout.EndVertical();
                }

                #endregion
            }
        }
    }
}

