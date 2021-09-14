using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open : MonoBehaviour
{

//public variables
//FR variables publiques
    [Header("Open on the")]
    //use to where the door will open
    //FR utilisé pour savoir où s'ouvre la porte
    public bool right;
    public bool left;
    public bool top;
    public bool bottom;
    public bool withRotation;
    [Header("If open with rotation : ")]
    public Vector3 openRotation;
    [Header("Open settings")]
    //use to know if the door start open in the game
    //FR utilisé pour savoir si la porte commence ouverte dans le jeu
    public bool startOpen;
    //openning speed of the door
    //FR vitesse d'ouverture de la porte
    public float openSpeed;
    //closing speed of the door
    //FR vistesse de fermuture de la porte
    public float closeSpeed;
    [Header("Button openning")]
    //code to use with buttons
    //FR code a utilisé avec les boutons
    public string doorButtonCode;
    [Header("Key openning")]
    //list on needed key to open the door
    //FR liste des clés nécessaire pour ouvrir la porte
    public string[] neededKeyName;
    [Header("Auto close settings")]
    //use to know if the user want to autoclose the door after open it
    //FR utilisé pour savoir si l'utilisateur veut fermer automatiquement la porte après ouverture
    public bool autoCloseTheDoor;
    //time before the door close
    //FR temps avant que la porte ce ferme
    public float timeBeforeClosing;

    public GameObject doorPivot;

//private variables
//FR variables privées
    //position of the door when she is open
    //FR position de la porte lorsqu'elle est ouverte
    private Vector3 openPosition;
    //position of the door when she is close
    //FR position de la porte lorsqu'elle est fermée
    private Vector3 closePosition;
    //use to know if the door is openning
    //FR utilisé pour savoir si la porte s'ouvre
    private bool isOpenning;
    //use to know if the door is closing
    //FR utilisé pour savoir si la porte se ferme
    private bool isClosing;
    //timer use to know when the door have to close
    //FR timer utilisé pour savoir quand la porte doit se fermée
    private float timerClosing;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //closePosition initialaze with the current door position
        //FR closePosition initialisée avec la position actuelle de la porte
        closePosition = transform.position;

        //openPosition initialaze depending on the open direction
        //FR openPosition initialisée en fonction de la direction d'ouverture de la porte
        if (right)
        {
            openPosition = closePosition + new Vector3(transform.localScale.x+0.1f,0f,0f);
        }
        else if (left)
        {
            openPosition = closePosition + new Vector3(-transform.localScale.x-0.1f, 0f, 0f);
        }
        else if (top)
        {
            openPosition = closePosition + new Vector3(0f, transform.localScale.y+0.1f, 0f);
        }
        else if(bottom)
        {
            openPosition = closePosition + new Vector3(0f, -transform.localScale.y+-0.1f, 0f);
        }
        else if (withRotation)
        {
            closePosition = transform.eulerAngles;
        }

        //if the door start open in the game
        //FR si la porte commence ouverte dans le jeu
        if (startOpen)
        {
            //set position to openPosition
            //FR set de la position à openPosition
            transform.position = openPosition;
        }

        //initialization of isOpenning and isClosing at false
        //FR initialisation de isOpenning et isClosing à faux
        isOpenning = false;
        isClosing = false;


        //if the button code isn't valide
        //FR si le code des boutons n'est pas valide
        if (!CheckCode())
        {
            //reset code
            //FR reset code
            ResCode();
        }

    }

    // Update is called once per frame
    // FR appellé toutes les frames
    void Update()
    {
        //if the door is openning
        //FR si la porte s'ouvre
        if (isOpenning)
        {
            if (withRotation)
            {
                //move the door
                //FR déplacement de la porte
                // The step size is equal to speed times frame time.
                var step = openSpeed * Time.deltaTime;

                if (doorPivot == null)
                {
                    // Rotate our transform a step closer to the target's.
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, openRotation, step);

                    //if the door is fully open
                    //FR si la porte est complètement ouverte
                    if (Vector3.Distance(transform.eulerAngles,openRotation)<=0.1)
                    {
                        //reset isOpenning
                        //FR reset isOpenning
                        isOpenning = false;
                    }
                }
                else
                {
                    doorPivot.transform.eulerAngles = Vector3.Lerp(doorPivot.transform.eulerAngles, openRotation, step);

                    //if the door is fully open
                    //FR si la porte est complètement ouverte
                    if (Vector3.Distance(doorPivot.transform.eulerAngles, openRotation) <= 0.1)
                    {
                        //reset isOpenning
                        //FR reset isOpenning
                        isOpenning = false;
                    }
                }

                
            }
            else
            {
                //move the door
                //FR déplacement de la porte
                transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
                //if the door is fully open
                //FR si la porte est complètement ouverte
                if (transform.position == openPosition)
                {
                    //reset isOpenning
                    //FR reset isOpenning
                    isOpenning = false;
                }
            }
            
        }
        //else if the door is closing
        //FR sinon si la porte se ferme
        else if (isClosing)
        {
            if (withRotation)
            {

                //move the door
                //FR déplacement de la porte
                // The step size is equal to speed times frame time.
                var step = openSpeed * Time.deltaTime;

                if (doorPivot == null)
                {
                    // Rotate our transform a step closer to the target's.
                    transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, closePosition, step);

                    //if the door is fully open
                    //FR si la porte est complètement ouverte
                    if (Vector3.Distance(transform.eulerAngles, closePosition) <= 0.1)
                    {
                        //reset isOpenning
                        //FR reset isOpenning
                        isClosing = false;
                    }
                }
                else
                {
                    doorPivot.transform.eulerAngles = Vector3.Lerp(doorPivot.transform.eulerAngles, closePosition, step);

                    //if the door is fully open
                    //FR si la porte est complètement ouverte
                    if (Vector3.Distance(doorPivot.transform.eulerAngles, closePosition) <= 0.1)
                    {
                        //reset isOpenning
                        //FR reset isOpenning
                        isClosing = false;
                    }
                }
            }
            else
            {

                //move the door
                //FR déplacement de la porte
                transform.position = Vector3.MoveTowards(transform.position, closePosition, closeSpeed * Time.deltaTime);
                if (transform.position == closePosition)
                {
                    //reset isClosing
                    //FR reset isClosing
                    isClosing = false;
                }

            }
            
        }
        //else
        //FR sinon
        else
        {
            //if this is the end of the timer and the user want autoclose
            //FR si le timer est fini et que l'utilisateur veut fermer la porte automatiquement
            if (Time.time > timerClosing && autoCloseTheDoor)
            {
                //close the door
                //FR fermeture de la porte
                CloseDoor();
            }
        }
        
            
    }

    //function OpenDoor use to initialize the openning of the door
    //FR fonction OpenDoor utilisé pour initialiser l'ouverture de la porte 
    public void OpenDoor()
    {
        //if the door isn't already openning or closing
        //FR si la porte n'est pas déjà en train de s'ouvrir ou de se fermer
        if (!isClosing && !isOpenning)
        {
            //initialization of the openning
            //FR initialisation de l'ouverture
            isOpenning = true;
        }

        //if the user want to autoclose the door
        //FR si l'utilisateur veut fermer automatiquement la porte
        if (autoCloseTheDoor)
        {
            //set the timer
            //FR set du timer
            timerClosing = Time.time + timeBeforeClosing;
        }
    }

    //function CloseDoor use to initialize the closing of the door
    //FR fonction CloseDoor utilisé pour initialiser la fermeture de la porte 
    public void CloseDoor()
    {
        //if the door isn't already openning or closing
        //FR si la porte n'est pas déjà en train de s'ouvrir ou de se fermer
        if (!isClosing && !isOpenning)
        {
            //initialization of the closing
            //FR initialisation de la fermeture
            isClosing = true;
        }
    }

    //GetButtonCode use to return the code of the door to an other object
    //FR GetButtonCode utilisé pour retourné le code de la porte à un autre objet
    public string GetButtonCode()
    {
        //return the value of doorButtonCode
        //FR retourne la valeur de doorButtonCode
        return doorButtonCode;
    }

    //CheckCode use to check if the code of the door is valide
    //FR CheckCode utilis& pour vérifié si le code de la porte est valide
    private bool CheckCode()
    {
        //foreach letter of the code
        //FR pour chaque caractère du code
        for(var i = 0; i < doorButtonCode.Length; i++)
        {
            //if the letter is not 0 or 1
            //FR si le caractère n'est pas 0 ou 1
            if(doorButtonCode[i]!='0' && doorButtonCode[i] != '1')
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

    //ResCode use to reset the code of the door
    //FR ResCode utilisé pour reset le code de la porte
    private void ResCode()
    {
        //use to create the new code
        //FR utilisé pour créer le nouveau code
        string tmpString="";
        //foreach letter of the code
        //FR pour chaque caractère du code
        for (var i = 0; i < doorButtonCode.Length; i++)
        {
            //add a 0 to tmpString
            //FR ajout d'un 0 à tmpString
            tmpString = tmpString+'0';
        }
        //reset the code of the door
        //FR reset du code de la porte
        doorButtonCode = tmpString;
    }

    //GetIsOpenning use to get the value of isOpenning for other object
    //FR GetIsOpenning utilisé pour retourné la valeur de isOpenning à un autre objet
    public bool GetIsOpenning()
    {
        //return isOpenning
        //FR retourne isOpenning
        return isOpenning;
    }

    //GetIsOpenning use to get the value of isClosing for other object
    //FR GetIsOpenning utilisé pour retourné la valeur de isClosing à un autre objet
    public bool GetIsClosing()
    {
        //return isClosing
        //FR retourne isClosing
        return isClosing;
    }

    //OpenWithKey use to open the door with the correct keys
    //FR OpenWithKey utilisé pour ouvrir la porte avec les bonnes clés
    public void OpenWithKey(Dictionary<int,Dictionary<string,Texture2D>> inventoryList)
    {

        bool testKey=false;

        //if the door need keys
        //FR si la porte a besoins de clés
        if (neededKeyName.Length != 0)
        {
            //for every needed keys
            //FR pour chaque clés nécessaires 
            for(var i = 0; i < neededKeyName.Length; i++)
            {
                //testKey is use to check if the player have the key
                //FR testKey est utilisé pour vérifier si le joueur possède la clé
                testKey = false;

                //for every item in the player's inventory
                //FR pour chaque item dans l'inventaire du joueur
                for(var j=0;j<inventoryList.Count;j++)
                {
                    //if the key have been found
                    //FR si la clé est trouvé
                    if (inventoryList[j].ContainsKey(neededKeyName[i]))
                    {
                        //set testKey to true
                        //FR set de testKey à vrai
                        testKey = true;
                    }
                }

                //if the key wasn't found
                //FR si la clé n'a pas été trouvé
                if (!testKey)
                {
                    //quit the function
                    //FR sorti de la fonction
                    return;
                }
                
            }

            //Open the door
            //FR ouverture de la porte
            OpenDoor();
        }
        
    }

}
