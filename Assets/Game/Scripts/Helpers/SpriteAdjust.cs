//-----------------------------------------------------------------------
// <author>
//      Prashat Gajre [gajre@sheridancollege.ca]
// </author>
//-----------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAdjust : MonoBehaviour
{
    RectTransform rect;
    SpriteRenderer sprite;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        sprite.size = new Vector2(rect.rect.width, rect.rect.height);
    }
}
