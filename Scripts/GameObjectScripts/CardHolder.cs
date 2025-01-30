using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardHolder : MonoBehaviour
{
    [SerializeField] Card card;
    [SerializeField] TMP_Text title, effect, movement;
    [SerializeField] GameObject r, tr, t, tl, l, bl, b, br;
    [SerializeField] Image art;
    [SerializeField] Transform cardContent;
    [SerializeField] private float cardUp;
    bool hovering = false;

    public void ActivateEffect()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ActivateCardEffect(card);
    }
    private void OnGUI()
    {
        if (cardContent != null)
        {
            if (hovering)
            {
                cardContent.localPosition = new Vector3(0, cardUp, 0);

            }
            else
            {
                cardContent.localPosition = new Vector3(0, 0, 0);

            }
        }
    }
    public void setHover(bool hovering)
    {
        if (GetComponent<Button>().interactable)
            this.hovering = hovering;

    }
    private void Start()
    {
        title.text = card.title; effect.text = card.effect; movement.text = card.movement;
        art.sprite = card.art;
        if(card is PieceCard)
        {
            PieceCard pieceCard = (PieceCard)card;

            r.SetActive(false);
            tr.SetActive(false);
            t.SetActive(false);
            tl.SetActive(false);
            l.SetActive(false);
            bl.SetActive(false);
            b.SetActive(false);
            br.SetActive(false);
            foreach (Movement move in pieceCard.piece.movements)
            {
                foreach (int direction in move.directions)
                {
                    switch (direction)
                    {
                        case 0: r.SetActive(true); 
                            break;
                        case 1: tr.SetActive(true); 
                            break;
                        case 2: t.SetActive(true);
                            break;
                        case 3: tl.SetActive(true);
                            break;
                        case 4: l.SetActive(true);
                            break;
                        case 5: bl.SetActive(true);
                            break;
                        case 6: b.SetActive(true);
                            break;
                        case 7: br.SetActive(true);
                            break;

                    }
                }
            }
        }
        
    }
}
