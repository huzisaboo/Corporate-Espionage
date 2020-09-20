//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NPCMission", menuName = "NPC/NPC Mission")][System.Serializable]
public class NPCMission : ScriptableObject
{
    public string missionName;
    public Sprite missionBadge;
}
