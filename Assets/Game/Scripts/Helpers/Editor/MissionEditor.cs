//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MissionEditor : EditorWindow
{
    NPCMissionOrganizer _NPCMissionOrganizer;
    [UnityEditor.ShortcutManagement.Shortcut("Mission Editor")]
    [MenuItem("Tools/Mission Editor")]
    public static void Init()
    {
        MissionEditor window = GetWindow<MissionEditor>();
        window.titleContent = new GUIContent("Mission Editor Window");
    }
    private void OnEnable()
    {
        string[] guids = AssetDatabase.FindAssets("t:NPCMissionOrganizer");

        if (guids != null && guids.Length > 0)
        {
            _NPCMissionOrganizer = (NPCMissionOrganizer)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]), typeof(NPCMissionOrganizer));
        }
    }

    private void OnGUI()
    {
        if (_NPCMissionOrganizer != null)
        {
            //var editor = Editor.CreateEditor(_NPCMissionOrganizer);
            //if (editor != null)
            //{
            //    editor.OnInspectorGUI();
            //}
            foreach (NPCDepartment department in _NPCMissionOrganizer.NPCDepartments)
            {
                var editor = Editor.CreateEditor(department);
                if (editor != null)
                {
                    editor.OnInspectorGUI();
                }
            }

        }
    }
}
