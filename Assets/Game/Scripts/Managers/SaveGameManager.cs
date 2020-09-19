///-------------------------------------------------------------------------------------------------
// File: SaveGameManager.cs
//
// Author: Dakshvir Singh Rehill
// Date: 11/6/2020
//
// Summary:	Singleton Class for Storing and Retrieving persistent data
///-------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveGameManager : Singleton<SaveGameManager>
{
    public void LoadPlayerData()
    {
        string aPath = Application.persistentDataPath;
        try
        {
            using (StreamReader aReader = new StreamReader(aPath))
            {
                string aJsonString = aReader.ReadToEnd();
            }
        }
        catch (Exception aE)
        {
            Debug.LogFormat("No Save File Exists\n{0}:{1}", aE.Message, aE.StackTrace);
        }
       
    }

    public void SavePlayerData()
    {
        try
        {
            string aPath = Application.persistentDataPath;
            using (StreamWriter aWriter = new StreamWriter(aPath))
            {
                aWriter.WriteLine("");
            }
        }
        catch (Exception aE)
        {
            Debug.LogErrorFormat("Couldn't Save File\n {0}:{1}", aE.Message, aE.StackTrace);
        }
    }

}
