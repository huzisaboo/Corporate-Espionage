using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : Menu
{
   public GameObject m_mainMenuPanel;

    public void BackToMainMenu()
    {
        gameObject.SetActive(false);
        m_mainMenuPanel.SetActive(true);
    }
}
