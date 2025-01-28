using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    [SerializeField] Card card;

    public void ActivateEffect()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ActivateCardEffect(card);
    }
}
