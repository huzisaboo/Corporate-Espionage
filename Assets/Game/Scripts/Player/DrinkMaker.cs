using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DrinkMaker : MonoBehaviour
{
    public Drink m_DrinkScriptableObj;
    private LongMouseClick m_longMouseClick;
    [SerializeField] Image mIconImage;
    [SerializeField] MenuClassifier mBarMenuClass;
    BarMenu mBarMenu;
    // Start is called before the first frame update
    void Start()
    {
        m_longMouseClick = GetComponent<LongMouseClick>();
        mIconImage.color = m_DrinkScriptableObj.m_drinkColor;
        mBarMenu = MenuManager.Instance.GetMenu<BarMenu>(mBarMenuClass);
        mBarMenu.mDrinkMakers.Add(m_DrinkScriptableObj.m_drinkType, this);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_longMouseClick.IsButtonPressed())
        {
            if (mBarMenu.drinkGlass == null)
            {
                mBarMenu.drinkGlass = Instantiate(mBarMenu.drinkGlassPrefab, mBarMenu.glassSpawnPosition.position, Quaternion.identity).GetComponent<DrinkGlass>();
                Color a_color = m_DrinkScriptableObj.m_drinkColor;
                mBarMenu.drinkGlass.SetLiquidColor(a_color);
                mBarMenu.drinkGlass.mDrinkType = m_DrinkScriptableObj.m_drinkType;
                mBarMenu.mActiveDrinkMaker = this;
            }
            if (mBarMenu.drinkGlass != null && mBarMenu.mActiveDrinkMaker == this)
            {
                float a_concentration = m_DrinkScriptableObj.m_concentration;
                mBarMenu.drinkGlass.IncAlcoholPercentage(a_concentration);

            }
        }
    }
}
