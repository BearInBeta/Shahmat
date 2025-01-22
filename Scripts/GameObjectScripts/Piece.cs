using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Piece", menuName = "ScriptableObjects/Piece", order = 1)]

public class Piece : ScriptableObject
{
    public bool isBlack;
    public Movement[] movements;
    public Sprite spriteBlack, spriteWhite;
}
