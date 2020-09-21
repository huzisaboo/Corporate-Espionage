using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eavesdropper : MonoBehaviour
{
    public float m_minEavesdropDist = 2.0f;
    private readonly List<PlayerDialog> playerDialogs = new List<PlayerDialog>();
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Populate the PlayerDialogs list (This if code will execute only once, Did this here instead of start because CompanyNPCs add themselves on Start so
        //I was getting an empty list)
        if(playerDialogs.Count <=0)
        {
            foreach (CompanyNPC npc in NPCManager.Instance.mTotalNPCs)
            {
                PlayerDialog playerDialog = npc.GetComponentInChildren<PlayerDialog>();
                if (playerDialog != null)
                {
                    playerDialogs.Add(playerDialog);
                }
            }
        }

        foreach (PlayerDialog npcDialog in playerDialogs)
        {
            if (Vector3.Distance(this.transform.position, npcDialog.transform.root.position) <= m_minEavesdropDist)
            {
                npcDialog.MakeReadable();
            }
        }
    }
}
