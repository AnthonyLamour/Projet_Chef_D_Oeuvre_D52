using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Resize : MonoBehaviour
{

//Public variables
//FR variables publiques

    //Global platform size settings
    //FR paramètre globaux de la taille de plateforme
    [Header("Global platform size settings")]
    //use to know if user want to resize the platform is the game or in the editor
    //FR utilisé pour savoir si l'utilisateur veut changer la taille de la plateforme dans le jeu ou dans l'éditeur
    public bool platformResizeInGame;

    //Settings to change platform size in game
    //FR paramètre pour changer la taille de la platform dans le jeu
    [Header("Settings to change platform size in game")]
    //size of the platform
    //FR taille de la plateforme
    public Vector3 platformSize;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //if the user want to resize the platform in the game
        //FR si l'utilisateur veut changer la taille de ça plateforme en jeux
        if (platformResizeInGame)
        {
            //change platform size
            //FR changement de la taille de la plateforme
            transform.localScale = platformSize;
        }

    }
}
