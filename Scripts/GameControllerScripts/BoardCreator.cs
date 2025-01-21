using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] GameObject square, board;
    [SerializeField] float xOffset, yOffset;
    public void GenerateBoard(int row, int col)
    {

        if (row < 4 || col < 4)
        {
            Debug.LogError("Board must be at least 4 x 4");
        }

        float a1rowPosition = (-(float)(row - 1) / 2.0f) + xOffset;
        float a1colPosition = (-(float)(col - 1) / 2.0f) + yOffset;
        bool squareWhite = false;
        for(int i = 0; i < col; i++)
        {
            float colPosition = a1colPosition + i;
            for(int j = 0; j < col; j++)
            {
                float rowPosition = a1rowPosition + j;
                GameObject newSquare = Instantiate(square, new Vector2(rowPosition, colPosition), Quaternion.identity);
                newSquare.transform.parent = board.transform;
                Square newSquareObject = newSquare.GetComponent<Square>();
                newSquareObject.GetComponent<Square>().row = j;
                newSquareObject.GetComponent<Square>().col = i;
                newSquareObject.GetComponent<Square>().white = squareWhite;
                newSquareObject.DetermineColor();
                squareWhite = !squareWhite;
            }
            if(row % 2 == 0)
                squareWhite = !squareWhite;
        }

    }
}
