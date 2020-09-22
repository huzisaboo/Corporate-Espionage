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

                MissionsManager.Instance.UpdateMissionProgress();
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

    //Mission Variables
    public MenuClassifier mMissionMenuClassifier;
    //[SerializeField]
    private int _mDeptMissionCount = 2; //Hardcoded mission count

    private int _mMissionProgress = 0;
    private int _mTotalMissionProgress = 0;
    private float _mCompletionPercentage = 0;
    [SerializeField] private float _mMinWinPercentage = 50;

    //Timer
    [Tooltip("Server mission time in minutes")]
    [SerializeField]private float _mServerMissionTime = 6f; //minutes
    private bool _mStartTimer = false;
    //private float _mTimerStartTime = 0;
    [SerializeField] TMPro.TextMeshProUGUI _mTimerDisplayText;

    void Awake()
    {
        GenerateNewMissions();
        _mTimerDisplayText.text = "";
        NPCManager.Instance.mGameModeChanged.AddListener(StartTimer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.BackQuote))
        {
            MenuManager.Instance.ShowMenu(mMissionMenuClassifier);
        }
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyUp(KeyCode.BackQuote))
        { 
            MenuManager.Instance.HideMenu(mMissionMenuClassifier);
        }

        if (_mStartTimer)
        {
            if (_mServerMissionTime >= Time.time)
            {
                float timeRemaining = _mServerMissionTime - Time.time;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                _mTimerDisplayText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
            }
            else
            {
                _mStartTimer = false;
                OnTimeOver();
            }
        }
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
                _mTotalMissionProgress += PlayerMission.missionProgressMax;
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

    public void UpdateMissionProgress()
    {
        _mMissionProgress = 0;
        foreach (PlayerMission mission in mPlayerMissions)
        {
            _mMissionProgress += mission.MissionProgress;
        }

        mMissionsMenu.mprogressText.text = _mMissionProgress + " / " + _mTotalMissionProgress;
        _mCompletionPercentage = ((float)_mMissionProgress / (float)_mTotalMissionProgress) * 100;

        if (_mCompletionPercentage >= 100)
        {
            //GameWinScreen
        }
    }

    public void StartTimer(GameMode gameMode)
    {
        //_mTimerStartTime = Time.time;   // Set the mission start time
        _mServerMissionTime *= 60;   // Set the mission duration in seconds
        _mServerMissionTime += Time.time;   // Set the mission end time
        _mStartTimer = true;
    }

    public void OnTimeOver()
    {
        if (_mCompletionPercentage >= _mMinWinPercentage)
        {
            //GameWinScreen            
            Debug.Log("YOU HAVE WON");
        }
        else
        {
            //GameLostScreen
            Debug.Log("YOU HAVE LOST");
        }
    }
}
