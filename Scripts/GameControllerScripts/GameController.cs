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
    [SerializeField] Piece[] whitepieceList, blackpieceList;
    [SerializeField] bool isBlack;
    [SerializeField] bool turnWhite = true;
    Vector2Int? selected;
    List<Vector2Int> currentPossibleMovements;
    
    // Start is called before the first frame update
    void Start()
    {
        creator.GenerateBoard(row, col);
        for(int i = 0; i < testBoard.Length; i++)
        {
            int index = i;
            if (i >= whitepieceList.Length) {
                index = i - whitepieceList.Length;
                Piece newPiece = Instantiate(blackpieceList[index]);
                newPiece.isBlack = true;
                creator.AddPiece(newPiece, testBoard[i]);
            }
            else
            {
                Piece newPiece = Instantiate(whitepieceList[index]);
                newPiece.isBlack = false;
                creator.AddPiece(newPiece, testBoard[index]);

            }
        }

    }
    void LightUpMovements(List<Vector2Int> possibleMovements)
    {
        UnlightAll();
        foreach (var possibleMovement in possibleMovements)
        {
            creator.boardZones[possibleMovement.x, possibleMovement.y].GetComponent<Square>().mark(true);
        }
    }
    void UnlightAll()
    {
        foreach (var boardzone in creator.boardZones)
        {
            boardzone.GetComponent<Square>().mark(false);
        }
    }
    private void DeselectAll()
    {
        selected = null;
        currentPossibleMovements = null;
        UnlightAll();
    }
    public void SelectSquare(Vector2Int coords)
    {
        if (selected != null)
        {
            if (currentPossibleMovements.Contains(coords))
            {
                creator.MovePiece((Vector2Int)selected, coords);
                turnWhite = !turnWhite;
                DeselectAll();
            }
            else
            {
                DeselectAll();
                SelectSquare(coords);
            }
            
        }
        else if(creator.GetPieceAt(coords) != null && selected == null && creator.GetPieceAt(coords).isBlack != turnWhite)
        {
            selected = coords;
            currentPossibleMovements = creator.GetPossibleMovements(coords);
            LightUpMovements(creator.GetPossibleMovements(coords));
        }
        else
        {
            DeselectAll();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
