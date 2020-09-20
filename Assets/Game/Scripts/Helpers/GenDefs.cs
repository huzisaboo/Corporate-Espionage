///-------------------------------------------------------------------------------------------------
// File: GenDefs.cs
//
// Author: Dakshvir Singh Rehill
// Date: 19/5/2020
//
// Summary:	Has Static Helper Functions and common Data Structures Defined
///-------------------------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections.Generic;
public class GenHelpers
{
    /// <summary>
    /// Used to shuffle a list of objects
    /// </summary>
    /// <typeparam name="T">Type of list</typeparam>
    /// <param name="pList">List variable</param>
    public static void Shuffle<T>(ref List<T> pList)
    {
        int aN = pList.Count;
        while (aN > 1)
        {
            aN--;
            int aK = UnityEngine.Random.Range(0, aN + 1);
            T aValue = pList[aK];
            pList[aK] = pList[aN];
            pList[aN] = aValue;
        }
    }
    /// <summary>
    /// Function to get the closest point on a line
    /// </summary>
    /// <param name="pOrigin">Line start</param>
    /// <param name="pEnd">Line end</param>
    /// <param name="pPoint">Point to check</param>
    /// <returns>Closest point</returns>
    public static Vector2 GetClosestPoint(Vector2 pOrigin, Vector2 pEnd, Vector2 pPoint)
    {
        Vector2 aDirection = pEnd - pOrigin;
        float aMaxMag = aDirection.magnitude;
        aDirection.Normalize();
        Vector2 aDistFromOrigin = pPoint - pOrigin;
        float aDotProduct = Vector2.Dot(aDistFromOrigin, aDirection);
        aDotProduct = Mathf.Clamp(aDotProduct, 0f, aMaxMag);
        return pOrigin + aDirection * aDotProduct;
    }
    /// <summary>
    /// Function to check if layer is in layermask
    /// </summary>
    /// <param name="layer">layer</param>
    /// <param name="layermask">layermask</param>
    /// <returns></returns>
    public static bool IsInLayerMask(int pLayer, LayerMask pLayermask)
    {
        return pLayermask == (pLayermask | (1 << pLayer));
    }


}

/// <summary>
/// Used for pooled objects that need to set some default functionality while they are inactive 
/// </summary>
public interface IInitable
{
    void Init();
}

/// <summary>
/// Used on all animation listeners so that state behaviours could inform animation completion
/// </summary>
public interface IAnimationCompleted
{
    void AnimationCompleted(int pShortHashName);
}

[Serializable]
public enum NPCType
{
    MissionNPC,
    NormalNPC
}
[Serializable]
public enum GameMode
{
    Bartender,
    Server
}

[Serializable]
public enum DrinkBase
{
    Rum,
    Whiskey,
    Vodka,
    Gin,
    Beer,
    Wine,
    Tequila
}

[Serializable]
public enum DrinkMixer
{
    Coffee,
    Chocolate,
    LemonWater,
    GingerAle,
    TomatoJuice,
    OrangeJuice,
    SoftDrink
}