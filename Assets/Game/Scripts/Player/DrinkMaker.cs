using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMaker : MonoBehaviour
{
    public GameObject drinkGlassPrefab;
    public Vector3 glassSpawnPosition;
    public Drink m_DrinkScriptableObj;
    private LongMouseClick m_longMouseClick;
    DrinkGlass drinkGlass;
    // Start is called before the first frame update
    void Start()
    {
        m_longMouseClick = GetComponent<LongMouseClick>();
    }

    // Update is called once per frame
    void Update()
    {
        if(m_longMouseClick.IsButtonPressed())
        {
            if(drinkGlass == null)
            {
                drinkGlass = Instantiate(drinkGlassPrefab, glassSpawnPosition, Quaternion.identity).GetComponent<DrinkGlass>();
                Color a_color = m_DrinkScriptableObj.m_drinkColor;
                drinkGlass.SetLiquidColor(a_color);
            }
            if (drinkGlass != null)
            {
                float a_concentration = m_DrinkScriptableObj.m_concentration;
                drinkGlass.IncAlcoholPercentage(a_concentration);
               
            }
        }   
    }
}
