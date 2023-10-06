using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    public Camera MainCamera;
    public TMPro.TMP_Text DiceRollText;

    bool isPlayerTurn = true;
    int diceRoll = -1;
    bool rolling = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Call the matching function depending on what turn it is
        if(isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            AITurn();
        }
    }
    
    void PlayerTurn()
    {
        // If we haven't rolled yet start rolling
        if(diceRoll == -1)
        {
            // TODO: Add the start of the rolling animation here, need to figure out how the animation will work
            // If their will be a general rolling animation that can end in different numbers landing up or if
            // there's a bunch of animations that can be picked from at the start and end with different numbers up.
            diceRoll = Random.Range(0, 5);
            DiceRollText.text = "Rolling...";
            rolling = true;
            Invoke("RollFinished", 1f);
        }
        // If we have rolled and we're done with the rolling animation
        else if(!rolling)
        {

        }
    }

    void AITurn()
    {

    }

    void RollFinished()
    {
        DiceRollText.text = diceRoll.ToString();
    }
}
