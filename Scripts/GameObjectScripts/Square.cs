using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public bool white;
    public bool marked;
    [SerializeField] Sprite whiteColor, BlackColor, whiteMarkedColor, blackMarkedColor;

    private void Start()
    {
        DetermineColor();
    }
    public void mark(bool marked)
    {
        this.marked = marked;
        DetermineColor ();
    }
    public void DetermineColor()
    {
        if (white)
        {
            if(!marked)
                GetComponent<SpriteRenderer>().sprite = whiteColor;
            else
                GetComponent<SpriteRenderer>().sprite = whiteMarkedColor;

        }
        else
        {
            if (!marked)
                GetComponent<SpriteRenderer>().sprite = BlackColor;
            else
                GetComponent<SpriteRenderer>().sprite = blackMarkedColor;
        }
    }

}
