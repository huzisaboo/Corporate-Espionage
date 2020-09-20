//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NPCDepartment", menuName = "NPC/NPC Department")]
public class NPCDepartment : ScriptableObject
{
    public string departmentName;
    public Sprite departmentBadge;
    public List<NPCMission> departmentMissions;
}
