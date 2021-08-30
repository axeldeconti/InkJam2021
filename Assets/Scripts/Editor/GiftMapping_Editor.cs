using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GiftMapping))]
public class GiftMapping_Editor : Editor
{
    private SerializedObject _object;
    private SerializedProperty _mappingCount;

    private void OnEnable()
    {
        _object = new SerializedObject(target);
        _mappingCount = _object.FindProperty("mapping.Array.size");
    }

    public override void OnInspectorGUI()
    {
        _object.Update();

        GUILayout.Label("Mappings", EditorStyles.boldLabel);

        var arrayProperty = _object.FindProperty("mapping");

        for (int i = 0; i < arrayProperty.arraySize; i++)
        {
            GUILayout.BeginHorizontal();
            var element = arrayProperty.GetArrayElementAtIndex(i);
            GiftMappingItem item = ((GiftMapping)target).mapping[i];
            item.name = EditorGUILayout.TextArea(item.name, GUILayout.Width(200));
            item.clip = (AudioClip)EditorGUILayout.ObjectField(item.clip, typeof(AudioClip));

            if (GUILayout.Button("x", GUILayout.Width(20f)))
            {
                if (RemoveMappingAtIndex(i))
                    break;
            }
            GUILayout.EndHorizontal();
        }

        if (GUILayout.Button("Add Item"))
        {
            _mappingCount.intValue++;
        }

        _object.ApplyModifiedProperties();
    }

    private bool RemoveMappingAtIndex(int index)
    {
        var myObj = (InventoryMapping)target;
        List<InventoryMappingItem> mapping = new List<InventoryMappingItem>(myObj.mapping);

        mapping.RemoveAt(index);

        if (GUI.changed)
        {
            Undo.RecordObject(myObj, "Property modification");
            myObj.mapping = mapping.ToArray();
            EditorUtility.SetDirty(myObj);

            return true;
        }

        return false;
    }
}