//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Mission
{
    public Image DepartmentMission1Badge;
    public Slider DepartmentMission1Progress;

    public void UpdateProgress(int _progress)
    {
        DepartmentMission1Progress.value = _progress;
    }
}

[System.Serializable]
public class Department
{ 
    public Image DepartmentBadge;
    public List<Mission> missions = new List<Mission>();
}

public class MissionsMenu : Menu
{
    public List<Department> mDepartments = new List<Department>();
}
