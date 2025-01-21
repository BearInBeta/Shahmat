using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] int[] directions = {2};
    [SerializeField] int breakLength = 0;
    [SerializeField] int breakDirection = 0;
    [SerializeField] bool onlyFirst = false;
    public List<Vector2Int> GetPossibleMovements(Vector2Int position, Piece[,] board)
    {
        Vector2Int[] directionOffsets = new Vector2Int[]
            {
            new Vector2Int(1, 0),   // Right
            new Vector2Int(1, 1),   // Top-right
            new Vector2Int(0, 1),   // Top
            new Vector2Int(-1, 1),  // Top-left
            new Vector2Int(-1, 0),  // Left
            new Vector2Int(-1, -1), // Bottom-left
            new Vector2Int(0, -1),  // Bottom
            new Vector2Int(1, -1)   // Bottom-right
            };

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

            if (jumps)
            {
                if(takes && board[newPosition.x, newPosition.y])
                {
                    movements.Add(newPosition);
                }
            }
        }
    }
}
