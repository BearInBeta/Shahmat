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
    public List<Movement> movements;
    public Sprite spriteBlack, spriteWhite;

    public List<Vector2Int> GetPossibleMovements(Vector2Int position, Piece[,] board)
    {
        List <Vector2Int> possibleMovements = new List <Vector2Int>();
        foreach (var movement in movements)
        {
            List<Vector2Int> newPossibleMovements = movement.GetPossibleMovements(position, board);
            foreach(Vector2Int newPossibleMovement in newPossibleMovements){
                possibleMovements.Add(newPossibleMovement);
            }
        }
        return possibleMovements;
    }
}
