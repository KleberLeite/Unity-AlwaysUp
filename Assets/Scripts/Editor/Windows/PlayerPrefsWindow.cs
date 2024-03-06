using UnityEngine;
using UnityEditor;
using AlwaysUp.Save;
using System;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace AlwaysUp.Editor
{
    public class PlayerPrefsWindow : EditorWindow
    {
        private const string INT_PP_PATH = "Assets/Scriptables/PlayerPrefs/Int";
        private const string STRING_PP_PATH = "Assets/Scriptables/PlayerPrefs/String";
        private const string BOOLEAN_PP_PATH = "Assets/Scriptables/PlayerPrefs/Boolean";

        private object[] _defaultValuesByType = new object[] { 0, "", false };

        private int _selectedPPType = 0;
        private Object _targetPP;
        private string _key;
        private object _defaultValue = 0;

        private string _nameOfPlayerPref;

        private List<string> _usedKeys;

        private void Awake()
        {
            _usedKeys = new List<string>();
            for (int i = 0; i < 3; i++)
            {
                foreach (Object obj in AssetDatabase.LoadAllAssetsAtPath(GetFolderPathBySelected(i)))
                    _usedKeys.Add(obj.name);
            }
        }

        [MenuItem("My Windows/PlayerPrefs Controller")]
        public static void ShowExample()
        {
            PlayerPrefsWindow window = GetWindow<PlayerPrefsWindow>();
            window.titleContent = new GUIContent("PlayerPrefs Controller");
        }

        public void OnGUI()
        {
            string[] playerPrefTypes = new string[] { "int", "string", "bool" };
            int newSelectedType = EditorGUILayout.Popup(new GUIContent("Type:"), _selectedPPType, playerPrefTypes);
            if (newSelectedType != _selectedPPType)
                HandleNewTypeSelected(newSelectedType);

            Object newPP = EditorGUILayout.ObjectField(new GUIContent("Target:"), _targetPP, GetPPTypeBySelected(_selectedPPType), false);
            if (newPP != _targetPP)
                HandleNewTargetSelected(newPP);

            _key = EditorGUILayout.TextField(new GUIContent("Key:"), _key);
            _defaultValue = GetDefaultValueBySelection(_selectedPPType);

            _nameOfPlayerPref = EditorGUILayout.TextField(new GUIContent("Name:"), _nameOfPlayerPref);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create"))
                HandleCreate(_nameOfPlayerPref, _selectedPPType, _key, _defaultValue);

            EditorGUI.BeginDisabledGroup(_targetPP == null);
            if (GUILayout.Button("Save"))
                HandleSave(_targetPP, _selectedPPType, _key, _defaultValue);
            if (GUILayout.Button("Rename"))
                HandleRename(_targetPP, _selectedPPType, _nameOfPlayerPref);
            if (GUILayout.Button("Delete"))
                HandleDelete(_targetPP, _selectedPPType);
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.EndHorizontal();
        }

        private void HandleNewTypeSelected(int newSelectedType)
        {
            _defaultValue = _defaultValuesByType[newSelectedType];
            _selectedPPType = newSelectedType;
        }

        private void HandleNewTargetSelected(Object newPP)
        {
            switch (_selectedPPType)
            {
                case 0:
                    IntPlayerPrefSO pp = ((IntPlayerPrefSO)newPP);
                    _key = pp.Key;
                    _defaultValue = pp.DefaultValue;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
            _targetPP = newPP;
        }

        private Type GetPPTypeBySelected(int selected)
            => selected switch
            {
                0 => typeof(IntPlayerPrefSO),
                _ => throw new IndexOutOfRangeException()
            };

        private object GetDefaultValueBySelection(int selected)
        {
            GUIContent label = new GUIContent("DefaultValue:");
            switch (selected)
            {
                case 0:
                    return EditorGUILayout.IntField(label, (int)_defaultValue);
                default:
                    throw new IndexOutOfRangeException();
            }
        }

        private void HandleCreate(string name, int selected, string key, object defaultValue)
        {
            string path = GetFolderPathBySelected(selected);
            if (AssetDatabase.LoadAssetAtPath($"{path}/{name}.asset", GetPPTypeBySelected(selected)))
            {
                Debug.LogError($"Can't create a PlayerPref named as {name} with this type because already exists an PlayerPref with this name!");
                return;
            }
            if (name.Trim() == string.Empty)
            {
                Debug.LogError("Can't create a PlayerPref with empty or all white space name.");
                return;
            }
            if (key.Trim() == string.Empty)
            {
                Debug.LogError("Can't create a PlayerPref with empty or all white space name.");
                return;
            }
            if (_usedKeys.Contains(key.Trim()))
            {
                Debug.LogError($"Can't create a PlayerPref with {nameof(key)} = {key} because already exists an PlayerPref with this key!");
            }

            Object playerPref;
            switch (selected)
            {
                case 0:
                    playerPref = (IntPlayerPrefSO)ScriptableObject.CreateInstance(typeof(IntPlayerPrefSO));
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            SavePlayerPref(playerPref, selected, key, defaultValue);

            AssetDatabase.CreateAsset(playerPref, $"{path}/{name}.asset");
            AssetDatabase.Refresh();
            EditorGUIUtility.PingObject(playerPref);

            _targetPP = playerPref;
        }

        private void HandleSave(Object playerPref, int selected, string key, object defaultValue)
        {
            if (!playerPref)
            {
                Debug.LogError("A object need to be assigned to save!");
                return;
            }
            if (key.Trim() == string.Empty)
            {
                Debug.LogError("Can't save a PlayerPref with empty or all white space name.");
                return;
            }
            if (_usedKeys.Contains(key.Trim()))
            {
                Debug.LogError($"Can't save a PlayerPref with {nameof(key)} = {key} because already exists an PlayerPref with this key!");
            }

            RemoveUsedKeyByPlayerPref(playerPref, selected);
            // Update usedKeys
            _usedKeys.Add(key.Trim());

            SavePlayerPref(playerPref, selected, key, defaultValue);

            EditorGUIUtility.PingObject(playerPref);
        }

        private void HandleRename(Object playerPref, int selected, string newName)
        {
            if (!playerPref)
            {
                Debug.LogError("A object need to be assigned to rename!");
                return;
            }
            if (newName.Trim() == string.Empty)
            {
                Debug.LogError("Can't rename a PlayerPref with empty or all white space name.");
                return;
            }
            string folderPath = GetFolderPathBySelected(selected);
            if (AssetDatabase.LoadAssetAtPath($"{folderPath}/{newName}.asset", GetPPTypeBySelected(selected)))
            {
                Debug.LogError($"Can't rename a PlayerPref to \"{newName}\" with this type because already exists an PlayerPref with this name!");
                return;
            }
            string path = $"{folderPath}/{playerPref.name}.asset";
            AssetDatabase.RenameAsset(path, newName.Trim());
            AssetDatabase.Refresh();

            EditorGUIUtility.PingObject(playerPref);
        }

        private void HandleDelete(Object playerPref, int selected)
        {
            if (!playerPref)
            {
                Debug.LogError("A object need to be assigned to delete!");
                return;
            }
            string folderPath = GetFolderPathBySelected(selected);
            AssetDatabase.DeleteAsset($"{folderPath}/{playerPref.name}.asset");
            AssetDatabase.Refresh();

            RemoveUsedKeyByPlayerPref(playerPref, selected);

            _targetPP = null;
        }

        private string GetFolderPathBySelected(int selected)
            => selected switch
            {
                0 => INT_PP_PATH,
                1 => STRING_PP_PATH,
                2 => BOOLEAN_PP_PATH,
                _ => throw new IndexOutOfRangeException()
            };

        private void RemoveUsedKeyByPlayerPref(Object playerPref, int selected)
        {
            switch (selected)
            {
                case 0:
                    _usedKeys.Remove(((IntPlayerPrefSO)playerPref).Key);
                    break;
            }
        }

        private void SavePlayerPref(Object playerPref, int selected, string key, object defaultValue)
        {
            SerializedObject so;
            switch (selected)
            {
                case 0:
                    PlayerPrefSO<int> pp = (IntPlayerPrefSO)playerPref;
                    so = new SerializedObject(pp);
                    SerializedProperty defaultSP = so.FindProperty("_defaultValue");
                    defaultSP.intValue = (int)defaultValue;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }

            SerializedProperty keySP = so.FindProperty("_key");
            keySP.stringValue = key.Trim();

            so.ApplyModifiedProperties();
        }
    }
}
