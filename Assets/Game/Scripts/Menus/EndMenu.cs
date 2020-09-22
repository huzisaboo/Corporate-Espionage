﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : Menu
{
    [Header("Static Text Values")]
    [SerializeField] string m_timeRemString = "Time Remaining: ";
    [SerializeField] string m_missionCompString = "Missions Completed: ";
    [SerializeField] string m_successMessage = "Congratulations! You Passed!";
    [SerializeField] string m_failureMessage = "You Failed! Better Luck Next Time!";
    [SerializeField] string m_completedMessage = "All missions completed!";
    [SerializeField] string m_timeupMessage = "You ran out of time!";
    [SerializeField] string m_spottedMessage = "You were spotted by the employee!";
    
    [Header("End Menu Variables")]
    [SerializeField] GameObject m_mainMenuPanel;
    [SerializeField] Text m_gameEndReasonText;
    [SerializeField] Text m_TimeRemaining;
    [SerializeField] Text m_missionCompleted;
    [SerializeField] Text m_passFailText;


    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
        m_mainMenuPanel.SetActive(true);
    }

    public void DisplayEndReason(GameEndReason p_reason)
    {
        if(p_reason == GameEndReason.MissionsOver)
        {
            m_gameEndReasonText.text = m_completedMessage;
        }
        else if(p_reason == GameEndReason.Spotted)
        {
            m_gameEndReasonText.text = m_spottedMessage;
        }
        else
        {
            m_gameEndReasonText.text = m_timeupMessage;
        }
    }

    public void DisplayRemainingTime(string time)
    {
        m_TimeRemaining.text = m_timeRemString + time +"s";
    }

    public void MissionsCompleted(int completedCount,int totalCount)
    {
        m_missionCompleted.text = m_completedMessage + completedCount.ToString() + "/" + totalCount.ToString();
    }

    public void DisplayResult(GameManager.Result p_result)
    {
        if(p_result == GameManager.Result.Won)
        {
            m_passFailText.text = m_successMessage;
        }
        else
        {
            m_passFailText.text = m_failureMessage;
        }
    }

}
