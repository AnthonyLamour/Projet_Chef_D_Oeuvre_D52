using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
 
//private variable
//FR variables privée
    //gameState is use to know the current gameSate of the game
    //FR gameState est utilisé pour connaître le gameState actuel du jeu
    private string gameState;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //initialization of the gameState
        //FR initialisation du gameState
        gameState = "Runing";
    }

    //SetGameState is use to set the gameState of the game
    //FR SetGameState est utilisé pour set le gameState du jeu
    public void SetGameState(string newGameState)
    {
        //set the gameState
        //FR set du gameState
        gameState = newGameState;

        //switch case of the gameState
        //FR switch case du gameState
        switch (gameState)
        {
            //if the gameState is pause
            //FR si le gameState est pause
            case "Pause":
                //stop the time of the scene
                //FR stop le temps de la scene
                StopTime();
                break;
            //if the gameState is Runing
            //FR si le gameState est Runing
            case "Runing":
                //make the time continu in the scene
                //FR relance le temps dans la scene
                TimeContinue();
                break;
        }
    }

    //GetGameState is use to get the current gameState
    //FR GetGameState est utilisé pour récupérer le gameState actuel
    public string GetGameState()
    {
        //return the gameState
        //FR renvoi du gameState
        return gameState;
    }

    //StopTime is use to stop the time in the scene
    //FR StopTime est utilisé pour stopper le temps dans la scène
    private void StopTime()
    {
        //stop the time
        //FR arrêt du temps
        Time.timeScale = 0;
    }

    //StopTime is use to make the time continu in the scene
    //FR StopTime est utilisé pour relancer le temps dans la scène
    private void TimeContinue()
    {
        //make the time continu
        //FR relance le temps
        Time.timeScale = 1;
    }
}
