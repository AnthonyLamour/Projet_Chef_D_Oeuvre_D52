using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Use : MonoBehaviour
{
    //use to know the door affected by the button
    //FR utilsié pour connaître la porte affecté par le bouton
    public GameObject targetDoor;
    //use to get all other buttons of the group
    //FR utilisé pour récupérer tous les autres boutons du groupe
    public GameObject[] otherButtonOfGroup;
    //use to get all other button influenced by this button
    //FR utilisé pour récupérer tous les autres boutons influencé par celui-ci
    public GameObject[] otherButtonInfluenced;
    //number of the button
    //FR numéro du bouton
    public int buttonNumber;

    //use to know if the button is actived
    //FR utilisé pour savoir si le bouton est actif
    private bool activate;
    //use to check if there is a valid door
    //FR utilisé pour vérifié si il y a une porte valide
    private bool noDoor;
    //code of the target door
    //FR code de la porte cible
    private string doorCode;
    //use to know if the button is in a correct position for the code
    //FR utilisé pour savoir si le bouton est dans la bonne position par rapport au code
    private bool isInCorrectPosition;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //initialize active to false
        //FR initialisation de active à faux
        activate = false;
        //if the targetDoor is not a door
        //FR si la porte cible n'est pas une porte
        if (targetDoor.tag != "Door")
        {
            //set noDoor at true
            //FR set noDoor à vrai
            noDoor = true;
        }
        //else if the targetDoor is a door
        //FR sinon si la porte cible est une porte
        else
        {
            //set noDoor at false
            //FR set noDoor à faux
            noDoor = false;
            //get the code of the door
            //FR récupération du code de la porte
            doorCode = targetDoor.GetComponent<Door_Open>().GetButtonCode();
        }
        //Check the position of the button
        //FR vérification de la postion du bouton
        isInCorrectPosition = CheckPosition();
    }

    //use by the player to switch the button
    //FR utilisé par le joueur pour changer le bouton
    public void SwitchButton()
    {
        //if there is a valid target door
        //FR si il y a une porte cible valide
        if (!noDoor)
        {
            //if the door isn't openning or closing
            //FR si la porte n'est pas en train de s'ouvrir ou de se fermer
            if(!targetDoor.GetComponent<Door_Open>().GetIsOpenning() && !targetDoor.GetComponent<Door_Open>().GetIsClosing())
            {
                //if the button is active
                //FR si le bouton est actif
                if (activate)
                {
                    //reset button position
                    //FR reset de la position du bouton
                    transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).transform.localPosition.x * 2, transform.GetChild(0).transform.localPosition.y, transform.GetChild(0).transform.localPosition.z);
                    //reset active
                    //FR reset de active
                    activate = false;
                    SwitchTheLamp();
                }
                //if the button isn't active
                //FR si le bouton n'est pas actif
                else
                {
                    //change the position of the button
                    //FR changement de la position du bouton
                    transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).transform.localPosition.x / 2, transform.GetChild(0).transform.localPosition.y, transform.GetChild(0).transform.localPosition.z);
                    //set active at true
                    //FR set active à vrai
                    activate = true;
                    SwitchTheLamp();
                }
                //check the position of the button
                //FR vérification de la position du bouton
                isInCorrectPosition = CheckPosition();
                //if there is other button inflence by this one
                //FR si il y a d'autres bouton influencé par celui-ci
                if (otherButtonInfluenced.Length > 0)
                {
                    //foreach other button influence by this one
                    //FR pour chaque autre bouton influencé par celui-ci
                    for (var i = 0; i < otherButtonInfluenced.Length; i++)
                    {
                        //switch the button
                        //FR changement de l'état du bouton
                        otherButtonInfluenced[i].GetComponent<Button_Use>().Influenced();
                    }
                }
                //check if the code is correct
                //FR vérifie si le code est correct
                if (CodeValidation())
                {
                    //open the door
                    //FR ouverture de la porte
                    targetDoor.GetComponent<Door_Open>().OpenDoor();
                }
                //else
                //FR sinon
                else
                {
                    //close the door
                    //FR fermuture de la porte
                    targetDoor.GetComponent<Door_Open>().CloseDoor();
                }
            }
            
        }
        
    }


    //Influenced switch the button
    //FR Influenced changement de l'état du bouton
    public void Influenced()
    {
        //if the door isn't openning or closing
        //FR si la porte n'est pas en train de s'ouvrir ou de se fermer
        if (!targetDoor.GetComponent<Door_Open>().GetIsOpenning() && !targetDoor.GetComponent<Door_Open>().GetIsClosing())
        {
            //if the button is active
            //FR si le bouton est actif
            if (activate)
            {
                //reset button position
                //FR reset de la position du bouton
                transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).transform.localPosition.x * 2, transform.GetChild(0).transform.localPosition.y, transform.GetChild(0).transform.localPosition.z);
                //reset active
                //FR reset de active
                activate = false;
                SwitchTheLamp();
            }
            //if the button isn't active
            //FR si le bouton n'est pas actif
            else
            {
                //change the position of the button
                //FR changement de la position du bouton
                transform.GetChild(0).transform.localPosition = new Vector3(transform.GetChild(0).transform.localPosition.x / 2, transform.GetChild(0).transform.localPosition.y, transform.GetChild(0).transform.localPosition.z);
                //set active at true
                //FR set active à vrai
                activate = true;
                SwitchTheLamp();
            }
            //Check the position of the button
            //FR vérification de la postion du bouton
            isInCorrectPosition = CheckPosition();
        }
    }

    //GetActivate return the value of active to an other object
    //FR GetActivate retourne la valeur de active à un autre objet
    public bool GetActivate()
    {
        //return activate
        //FR retourne activate
        return activate;
    }

    //CodeValidation return if the code is correct
    //FR CodeValidation retourne si le code est correct
    private bool CodeValidation()
    {
        //if the door is valid
        //FR si la porte est valide 
        if (!noDoor)
        {
            //check if the button is in the correct position
            //FR vérifi si le bouton à une position correct
            if (isInCorrectPosition)
            {
                //foreach other button of the group
                //FR pour chaque autre bouton du groupe
                for(var i = 0; i < otherButtonOfGroup.Length; i++)
                {
                    //if the button isn't in the correct position
                    //FR si le bouton n'est pas dans ça bonne position
                    if (!otherButtonOfGroup[i].GetComponent<Button_Use>().isInCorrectPosition)
                    {
                        //return false
                        //FR retourne faux
                        return false;
                    }
                }
            }
            //else if the button isn't in the correct position
            //FR sinon si le bouton n'est pas dans une bonne position
            else
            {
                //return false
                //FR retourne faux
                return false;
            }
        }

        //return true
        //FR retourne vrai
        return true;
    }

    //CheckPosition use to Check if the button is in the correct position
    //FR CheckPosition utilisé pour vérifier si le bouton est dans la bonne position
    private bool CheckPosition()
    {
        //if there is a valid door
        //FR si il y a une porte valide
        if (!noDoor)
        {
            //if the button is active and the number is 1 or if the button isn't active and the number is 0
            //FR si le bouton est actif et que le nombre est 1 ou que le bouton est inactif et que le nombre est 0
            if ((activate && doorCode[buttonNumber] == '1') || (!activate && doorCode[buttonNumber] == '0'))
            {
                //return true
                //FR retourne vrai
                return true;
            }
            //else
            //FR sinon
            else
            {
                //return false
                //FR retourne faux
                return false;
            }
        }
        //else if the door isn't valid
        //FR sinon si la porte n'est pas valide
        else
        {
            //return false
            //FR retourne faux
            return false;
        }
        
    }

    //SwitchTheLamp use to switch the color of the corresponding lamp
    //FR SwitchTheLamp utilisé pour changer la couleur de la lampe correspondante
    private void SwitchTheLamp() {
        //foreach child of the button
        //FR pour chaque enfant du button
        for(var i = 0; i < transform.childCount; i++)
        {
            //if this is a lamp
            //FR si c'est une lampe
            if (transform.GetChild(i).gameObject.tag == "Button_Lamp")
            {
                //switch her color
                //FR changement de sa couleur
                transform.GetChild(i).GetComponent<Button_Switch_Lamp>().SwitchLampColor();
            }
        }
    }

}
