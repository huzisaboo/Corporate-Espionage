//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDialog : MonoBehaviour
{
    public static readonly float dialogDisplayDelay = 6.3f;

    public GameObject dialogBox;
    public Transform dialogContentParent;
    public GameObject emptySprite;
    public GameObject[] scribbles;
    public GameObject symbol;
    public Sprite symbolSprite;

    public bool showDialog = false;
    public bool isReadable = false;

    bool isShowingDefault = false;
    bool showingInformation = false;

    private int missionProgress = 0;
    public int MissionProgress
    {
        get { return missionProgress; }
        set { missionProgress = value; OnUpdateMissionProgress?.Invoke(missionProgress); }
    }

    //Time
    [Range(0,1)]public float symbolDisplayFactor = 1;
    public float startTime;

    void Start()
    {
        ShowDefaultDialog();
    }

    public void MakeVisible()
    {
        showDialog = true;
    }

    public void MakeReadable()
    {
        isReadable = true;
        startTime = Time.time;
        ShowScribbles();
    }

    private void Update()
    {
        if (showDialog)
        {
            dialogBox.SetActive(true);
            if (isReadable)
            {
                if (Time.time > startTime + (dialogDisplayDelay * symbolDisplayFactor))
                {
                    startTime = Time.time;
                    if (!showingInformation)
                    {
                        ShowInformation();
                        isShowingDefault = false;
                        showingInformation = true;
                    }
                    else
                    {
                        ShowScribbles();
                        isShowingDefault = false;
                        showingInformation = false;
                    }
                }
                else
                {
                    //ShowScribbles();
                }
            }
            else
            {
                if (!isShowingDefault)
                {
                    ShowDefaultDialog();
                    isShowingDefault = true;
                    showingInformation = false;
                }
            }
        }
        else
        {
            ResetDialogBox();
            dialogBox.SetActive(false);
            isShowingDefault = false;
            showingInformation = false;
        }
    }

    public void ResetDialogBox()
    {
        for (int i = dialogContentParent.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(dialogContentParent.GetChild(i).gameObject);
        }
    }

    public void ShowDefaultDialog()
    {
        ResetDialogBox();
        Image sprite = GameObject.Instantiate(emptySprite, dialogContentParent).GetComponent<Image>();
    }

    public void ShowScribbles()
    {
        ResetDialogBox();
        for (int i = 0; i < 12; i++)
        {
            Image sprite = GameObject.Instantiate(scribbles[Random.Range(0, scribbles.Length)], dialogContentParent).GetComponent<Image>();
        }
    }

    public void ShowInformation()
    {
        ResetDialogBox();
        for (int i = 0; i < 11; i++)
        {
            Image sprite = GameObject.Instantiate(scribbles[Random.Range(0, scribbles.Length)], dialogContentParent).GetComponent<Image>();
        }
        Image spriteSymbol = GameObject.Instantiate(symbol, dialogContentParent).GetComponent<Image>();
        spriteSymbol.transform.SetSiblingIndex(Random.Range(0, dialogContentParent.childCount));
        spriteSymbol.sprite = symbolSprite;
        MissionProgress++;
        Debug.LogError("INFORMATION SHOWN" + MissionProgress);
    }

    public delegate void UpdateProgress(int _progress);
    public event UpdateProgress OnUpdateMissionProgress;
}
