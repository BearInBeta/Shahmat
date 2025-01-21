using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public int row;
    public int col;

    public bool white;

    [SerializeField] Sprite whiteColor, BlackColor;

    private void Start()
    {
        DetermineColor();
    }
    
    public void DetermineColor()
    {
        if (white)
        {
            GetComponent<SpriteRenderer>().sprite = whiteColor;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = BlackColor;
        }
    }

}
