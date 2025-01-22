using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int row, col;
    [SerializeField] BoardCreator creator;
    [SerializeField] Movement testMovement;
    [SerializeField] int testX, testY;
    [SerializeField] Vector2Int[] testBoard;
    [SerializeField] Piece[] pieceList;
    // Start is called before the first frame update
    void Start()
    {
        creator.GenerateBoard(row, col);
        for(int i = 0; i < pieceList.Length; i++)
        {
            creator.AddPiece(pieceList[i], testBoard[i]);
        }


    }
    void LightUpMovements(List<Vector2Int> possibleMovements)
    {
        foreach (var boardzone in creator.boardZones)
        {
            boardzone.GetComponent<Square>().mark(false);
        }
        foreach (var possibleMovement in possibleMovements)
        {
            creator.boardZones[possibleMovement.x, possibleMovement.y].GetComponent<Square>().mark(true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
