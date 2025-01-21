using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    public int row;
    public int col;

    public bool white;

    [SerializeField] Color whiteColor, BlackColor;

    private void Start()
    {
        DetermineColor();
    }
    
    public void DetermineColor()
    {
        if (white)
        {
            GetComponent<SpriteRenderer>().color = whiteColor;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = BlackColor;
        }
    }

}
