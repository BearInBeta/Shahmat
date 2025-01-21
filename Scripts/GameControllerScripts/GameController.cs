using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] int row, col;
    [SerializeField] BoardCreator creator;
    // Start is called before the first frame update
    void Start()
    {
        creator.GenerateBoard(row, col);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
