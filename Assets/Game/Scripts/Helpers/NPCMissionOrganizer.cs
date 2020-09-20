//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NPCMissionOrganizer", menuName = "NPC/NPC Mission Organizer")]
public class NPCMissionOrganizer : ScriptableObject
{
    public List<NPCDepartment> NPCDepartments;
}
