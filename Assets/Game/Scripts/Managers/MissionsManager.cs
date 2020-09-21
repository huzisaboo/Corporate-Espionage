//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMission
{
    public static readonly int missionProgressMax = 3;
    public string missionName;
    public Sprite missionBadge;

    public string departmentName;

    private int missionProgress = 0;

    public int MissionProgress
    {
        get { return missionProgress; }
        set
        {
            if (value > missionProgressMax)
            {
                return;
            }
            else
            {
                missionProgress = value;

                OnChangeProgress?.Invoke(missionProgress);
            }
        }
    }

    public PlayerMission(string name, Sprite badge, string dept)
    {
        missionName = name;
        missionBadge = badge;
        departmentName = dept;
        MissionProgress = 0;
    }

    public void UpdateProgress(int _progress)   //Subscribe this to the PlayerDialog UpdateMissionProgress wherever declared and linked
    {
        MissionProgress = _progress;
    }


    public delegate void ChangeProgress(int _progress);
    public event ChangeProgress OnChangeProgress;
}

public class MissionsManager : Singleton<MissionsManager>
{
    public NPCMissionOrganizer mNPCMissionOrganizer;

    public List<PlayerMission> mPlayerMissions = new List<PlayerMission>();

    public MissionsMenu mMissionsMenu;

    [SerializeField]
    private int _mDeptMissionCount = 2;

    void Start()
    {
        GenerateNewMissions();
    }

    public void GenerateNewMissions()
    {
        //Hardcoding some stuff here for teh lack of time
        for (int i = 0; i < mNPCMissionOrganizer.NPCDepartments.Count; i++)
        {
            mMissionsMenu.mDepartments[i].DepartmentBadge.sprite = mNPCMissionOrganizer.NPCDepartments[i].departmentBadge;
            List<int> missions = new List<int>();
            for (int j = 0; j < _mDeptMissionCount; j++)
            {
                missions.Add(GetRandomIntExcluding(mNPCMissionOrganizer.NPCDepartments[i].departmentMissions.Count, missions));
            }
            int count = 0;
            foreach (int k in missions)
            {
                PlayerMission playerMission = new PlayerMission(mNPCMissionOrganizer.NPCDepartments[i].departmentMissions[k].missionName, mNPCMissionOrganizer.NPCDepartments[i].departmentMissions[k].missionBadge, mNPCMissionOrganizer.NPCDepartments[i].departmentName);
                mPlayerMissions.Add(playerMission);

                mMissionsMenu.mDepartments[i].missions[count].DepartmentMission1Badge.sprite = playerMission.missionBadge;
                mMissionsMenu.mDepartments[i].missions[count].DepartmentMission1Progress.maxValue = PlayerMission.missionProgressMax;
                mMissionsMenu.mDepartments[i].missions[count].DepartmentMission1Progress.value = 0;
                playerMission.OnChangeProgress += mMissionsMenu.mDepartments[i].missions[count].UpdateProgress;
                count++;
            }
        }

        //foreach (PlayerMission mission in mPlayerMissions)
        //{
        //    mission.MissionProgress = Random.Range(0, PlayerMission.missionProgressMax);
        //}
    }

    public int GetRandomIntExcluding(int range, List<int> exclusion)
    {
        int randomNum = 0;
        bool found = false;
        while (!found)
        {
            randomNum = Random.Range(0, range);
            if (!exclusion.Contains(randomNum))
            {
                found = true;
            }
        }
        return randomNum;
    }
}
