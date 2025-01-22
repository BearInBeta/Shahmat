using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] GameObject square, board, piece;
    [SerializeField] float xOffset, yOffset;
    public GameObject[,] boardZones;
    public Piece[,] boardTracker;
    public void GenerateBoard(int row, int col)
    {
        boardTracker = new Piece[row, col];
        boardZones = new GameObject[row, col];
        if (row < 4 || col < 4)
        {
            Debug.LogError("Board must be at least 4 x 4");
        }

        float a1rowPosition = (-(float)(col - 1) / 2.0f) + xOffset;
        float a1colPosition = (-(float)(row - 1) / 2.0f) + yOffset;
        bool squareWhite = false;
        for(int i = 0; i < row; i++)
        {
            float colPosition = a1colPosition + i;
            for(int j = 0; j < col; j++)
            {
                float rowPosition = a1rowPosition + j;
                GameObject newSquare = Instantiate(square, new Vector2(colPosition, rowPosition), Quaternion.identity);
                newSquare.transform.parent = board.transform;
                Square newSquareObject = newSquare.GetComponent<Square>();
                newSquareObject.GetComponent<Square>().white = squareWhite;
                newSquareObject.DetermineColor();
                squareWhite = !squareWhite;
                boardZones[i, j] = newSquare;
            }
            if(row % 2 == 0)
                squareWhite = !squareWhite;
        }

    }

    public void AddPiece(Piece piece, Vector2Int position)
    {
        boardTracker[position.x, position.y] = piece;
        GameObject newPiece = Instantiate(this.piece, boardZones[position.x, position.y].transform);
        newPiece.GetComponent<SpriteRenderer>().sprite = piece.isBlack ? piece.spriteBlack : piece.spriteWhite;
        newPiece.transform.localPosition = new Vector3(0, 0.15f, -1);

    }
}
