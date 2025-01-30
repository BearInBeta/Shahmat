using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    [SerializeField] BoardCreator creator;
    [SerializeField] GameController controller;
    IEnumerator summonPieceCoroutine;

    public void SummonPiece(Piece piece)
    {
        if (summonPieceCoroutine != null)
        {
            StopAllCoroutines();
        }
        controller.LightUpMovements(SummonableSquares());
        summonPieceCoroutine = SummonPieceCoroutine(piece);
        StartCoroutine(summonPieceCoroutine);
    }
    List<Vector2Int> SummonableSquares()
    {
        List <Vector2Int> result = new List<Vector2Int>();
        for (int i = 0; i < controller.maxSummon; i++)
        {
            for(int j = 0; j < controller.col; j++)
            {
                if (creator.boardTracker[j,i] == null)
                result.Add(new Vector2Int(j, i));
            }
        }
        return result;
    }
    public void SummonPiece(Piece piece, Vector2Int position)
    {
        creator.AddPiece(piece, position);
    }

    private IEnumerator SummonPieceCoroutine(Piece piece)
    {
        while (controller.cardWaiting)
        {
            yield return new WaitForEndOfFrame();
        }
        controller.cardWaiting = true;
        while (controller.cardWaiting)
        {
            yield return new WaitForEndOfFrame();
        }
        if (controller.selected != null)
        {
            Vector2Int selected = (Vector2Int)controller.selected;
            if (SummonableSquares().Contains(selected))
                SummonPiece(piece, (Vector2Int)controller.selected);
        }
        
        controller.DeselectAll();
    }

}
