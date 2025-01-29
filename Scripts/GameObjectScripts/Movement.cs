using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Movement", menuName = "ScriptableObjects/Movement", order = 1)]

public class Movement : ScriptableObject
{
    [SerializeField] int movementLength = 1;
    [SerializeField] bool jumps = false;
    [SerializeField] bool disjointed = false;
    [SerializeField] bool takes = false;
    [SerializeField] bool onlyTakes = false;
    public int[] directions = {2};
    [SerializeField] Vector2Int[] breakPoints;
    public bool onlyFirst = false;
    public List<Vector2Int> GetPossibleMovements(Vector2Int position, Piece[,] board)
    {
        if(movementLength == 0) {
            movementLength = Mathf.Max(board.GetLength(0), board.GetLength(1));
        }
        Piece ogPiece = board[position.x, position.y];
        Vector2Int[] directionOffsets = new Vector2Int[]
    {
            new Vector2Int(1, 0),   // 0 Right
            new Vector2Int(1, 1),   // 1 Top-right
            new Vector2Int(0, 1),   // 2 Top
            new Vector2Int(-1, 1),  // 3 Top-left
            new Vector2Int(-1, 0),  // 4 Left
            new Vector2Int(-1, -1), // 5 Bottom-left
            new Vector2Int(0, -1),  // 6 Bottom
            new Vector2Int(1, -1)   // 7 Bottom-right
    };
        if (ogPiece.isBlack)
        {
            directionOffsets = new Vector2Int[]
            {
            new Vector2Int(-1, 0),  // 4 Left
            new Vector2Int(-1, -1), // 5 Bottom-left
            new Vector2Int(0, -1),  // 6 Bottom
            new Vector2Int(1, -1),   // 7 Bottom-right
            new Vector2Int(1, 0),   // 0 Right
            new Vector2Int(1, 1),   // 1 Top-right
            new Vector2Int(0, 1),   // 2 Top
            new Vector2Int(-1, 1)  // 3 Top-left
            
            };
        }


        List<Vector2Int> movements = new List<Vector2Int>();
        foreach (int direction in directions)
        {
            

            // Ensure direction is valid
            if (direction < 0 || direction > 7)
            {
                Debug.LogError("Invalid direction. Must be between 0 and 7.");
                return null;
            }

            // Get the direction offset
            Vector2Int offset = directionOffsets[direction];

            // Calculate the displacement
            Vector2Int displacement = offset * movementLength;

            // Add the new position
            Vector2Int newPosition = position + displacement;

            Stack<Vector2Int> moveChecks = new Stack<Vector2Int>();
           

            if (breakPoints.Count() == 0)
            {
                AddPointsInLine(position, newPosition, moveChecks);
                FilterMovements(moveChecks, movements, board, ogPiece);

            }
            else
            {

                foreach (Vector2Int breakPoint in breakPoints)
                {
                    AddPointsInLine(position, newPosition, moveChecks);

                    offset = directionOffsets[(direction + breakPoint.y + 6) % 8];

                    displacement = offset * breakPoint.x;

                    Vector2Int newnewPosition = newPosition + displacement;
                    AddPointsInLine(newPosition, newnewPosition, moveChecks);
                    FilterMovements(moveChecks, movements, board, ogPiece);

                }

            }
        }

        return movements;
    }


    void FilterMovements(Stack<Vector2Int> moveChecks, List<Vector2Int> movements, Piece[,] board, Piece ogPiece)
    {
        
        bool addMove = true;
        Vector2Int moveCheck = moveChecks.Pop();
        if (moveCheck.x < 0 || moveCheck.y < 0 || moveCheck.x >= board.GetLength(0) || moveCheck.y >= board.GetLength(1))
        {
            addMove = false;
        }
        if(addMove && board[moveCheck.x, moveCheck.y] != null && ogPiece.isBlack == board[moveCheck.x, moveCheck.y].isBlack)
        {
            addMove = false;
        }
        if (addMove && onlyTakes && board[moveCheck.x, moveCheck.y] == null)
        {
            addMove = false;
        }
        if (addMove&& !takes && board[moveCheck.x, moveCheck.y] != null)
        {
            addMove = false;
        }
        if (addMove && !jumps)
        {
            foreach(Vector2Int move in moveChecks)
            {
                if (board[move.x,move.y] != null)
                {
                    addMove = false;
                    while (!moveChecks.Peek().Equals(move))
                    {
                        moveChecks.Pop();
                    }
                    break;
                }
            }
        }
        if(disjointed)
        {
            moveChecks.Clear();
        }
        if (addMove)
        {
            movements.Add(moveCheck);
        }
        if(moveChecks.Count > 0)
            FilterMovements(moveChecks, movements, board, ogPiece);
    }

    void AddPointsInLine(Vector2Int position, Vector2Int newPosition, Stack<Vector2Int> moveChecks)
    {
        int deltaX = Mathf.Abs(newPosition.x - position.x);
        int deltaY = Mathf.Abs(newPosition.y - position.y);

        // Check if they are in a straight line
        if (deltaX != 0 && deltaY != 0 && deltaX != deltaY)
        {
            Debug.Log("The points are not in a straight line.");
            return;
        }

        // Determine the step for x and y
        int stepX = deltaX == 0 ? 0 : (newPosition.x - position.x) / deltaX;
        int stepY = deltaY == 0 ? 0 : (newPosition.y - position.y) / deltaY;

        Vector2Int current = position;

        while (true)
        {
            current = new Vector2Int(current.x + stepX, current.y + stepY);

            if (current == newPosition)
            {
                moveChecks.Push(current); // Add newPosition
                break;
            }

            moveChecks.Push(current); // Add intermediate points
        }
    }
}
