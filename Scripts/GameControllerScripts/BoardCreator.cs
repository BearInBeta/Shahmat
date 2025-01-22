using System;
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
        for(int i = 0; i < col; i++)
        {
            float colPosition = a1colPosition + i;
            for(int j = 0; j < row; j++)
            {
                float rowPosition = a1rowPosition + j;
                GameObject newSquare = Instantiate(square, new Vector2(colPosition, rowPosition), Quaternion.identity);
                newSquare.transform.parent = board.transform;
                Square newSquareObject = newSquare.GetComponent<Square>();
                newSquareObject.coordinates = new Vector2Int(i, j);
                newSquareObject.GetComponent<Square>().white = squareWhite;
                newSquareObject.DetermineColor();
                squareWhite = !squareWhite;
                boardZones[i, j] = newSquare;
            }
            if(row % 2 == 0)
                squareWhite = !squareWhite;
        }

    }
    public void PrintPieceColors()
    {
        for (int i = 0; i < boardTracker.GetLength(0); i++) // Iterate over rows
        {
            for (int j = 0; j < boardTracker.GetLength(1); j++) // Iterate over columns
            {
                Piece piece = boardTracker[i, j];
                if (piece != null)
                {
                    Debug.Log($"Piece at [{i}, {j}] isBlack: {piece.isBlack}");
                }
            }
        }
    }
    public Piece GetPieceAt(Vector2Int index)
    {
       return boardTracker[index.x, index.y];
    }

    public List<Vector2Int> GetPossibleMovements(Vector2Int coords)
    {
        return GetPieceAt(coords).GetPossibleMovements(coords, boardTracker);
    }
    public void AddPiece(Piece piece, Vector2Int position)
    {
        boardTracker[position.x, position.y] = piece;
        GameObject newPiece = Instantiate(this.piece, boardZones[position.x, position.y].transform);
        newPiece.GetComponent<SpriteRenderer>().sprite = piece.isBlack ? piece.spriteBlack : piece.spriteWhite;
        newPiece.transform.localPosition = new Vector3(0, 0.25f, -1);
    }

    public void MovePiece(Vector2Int from, Vector2Int to)
    {
        boardTracker[from.x, from.y].movements.RemoveAll(movement => movement.onlyFirst);
        boardTracker[to.x, to.y] = boardTracker[from.x, from.y];
        foreach(Transform child in boardZones[to.x, to.y].transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in boardZones[from.x, from.y].transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        GameObject newPiece = Instantiate(this.piece, boardZones[to.x, to.y].transform);
        newPiece.GetComponent<SpriteRenderer>().sprite = boardTracker[from.x, from.y].isBlack ? boardTracker[to.x, to.y].spriteBlack : boardTracker[to.x, to.y].spriteWhite;
        newPiece.transform.localPosition = new Vector3(0, 0.25f, -1);
        boardTracker[from.x, from.y] = null;

    }
}
