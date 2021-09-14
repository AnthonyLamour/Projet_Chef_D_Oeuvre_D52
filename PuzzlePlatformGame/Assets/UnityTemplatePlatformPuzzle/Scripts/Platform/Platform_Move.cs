using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Move : MonoBehaviour
{

//public variables
//FR variables publiques
    //use to know if the user want to move the platfomr
    //FR utilisé pour savoir si l'utilisateur veut déplacer la platform
    public bool movePlatform;
    //use to know if the user want to automaticly set the first checkpoint
    //FR utilisé pour savoir si l'utilisateur veut seter le premier checkpoint automatiquement
    public bool autoInitializeFirstCheckpoint;
    //list of checkpoint were the platform will move
    //FR liste de point de passages où la plateforme va se déplacer
    public List<Vector3> checkpoints = new List<Vector3>() { new Vector3(0f,0f,0f) };
    //Platform movement speed
    //FR vitesse de déplacement de la plateforme
    public float speed;

//privates variables
//FR variables privées
    //use to get the next platform postion in the list
    //FR utilisé pour récupérer la position suivqnte de la platefomre dans la list
    private int nextPlatformPosition;

    private bool forward;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        forward = true;
        //if the user want to move the platform
        //FR si l'utilisateur veut bouger la plateforme
        if (movePlatform)
        {
            //if the user want to autoinitialize the first checkpoint
            //FR si l'utilisateur veut initialisé automatiquement le premier checkpoint
            if (autoInitializeFirstCheckpoint)
            {
                //if the checkpoints size if more than 0
                //FR si la taille de checkpoints est supérieur à 0
                if (checkpoints.Count > 0)
                {
                    //initialize the first checkpoint at the platform position
                    //FR initialisation du premier checkpoint à la position de la plateforme
                    checkpoints[0] = transform.position;
                }
                //if the checkpoints size if less than 0
                //FR si la taille de checkpoints est inférieur à 0 
                else
                {
                    //add the platform position to checkpoints
                    //FR ajout de la position de la plateforme à checkpoints
                    checkpoints.Add(transform.position);
                }
            }

            //initialization of the nextPlatformPosition to 1 in the list
            //FR initialisation de nextPlatform à 1 dans la liste
            nextPlatformPosition = 1;
        }
        
    }

    // Update is called once per frame
    // FR appellé toutes à la fin de toutes les frames
    void FixedUpdate()
    {
        //if the user want to move the platform
        //FR si l'utilisateur veut bouger la plateforme
        if (movePlatform)
        {
            //move the platform
            //FR déplacement de la plateforme
            transform.position = Vector3.MoveTowards(transform.position, checkpoints[nextPlatformPosition], speed * Time.deltaTime);
            //if the platform reach her next position
            //FR si la plateforme a atteint sa prochaine position
            if (transform.position == checkpoints[nextPlatformPosition])
            {
                if (forward)
                {
                    //if nextPlatformPosition isn't the end of the list
                    //FR si nextPlatformPosition n'est pas la fin de la liste
                    if (nextPlatformPosition != checkpoints.Count - 1)
                    {
                        //change nextPlatformPosition to the next one
                        //FR changement de nextPlatformPosition au prochain
                        nextPlatformPosition = nextPlatformPosition + 1;
                    }
                    //if nextPlatformPosition is the end of the list
                    //FR si nextPlatformPosition est la fin de la liste
                    else
                    {
                        //reitilaze nextPlatformPosition
                        //FR reinitialisation de nextPlatformPosition
                        nextPlatformPosition = nextPlatformPosition-1;
                        forward = false;
                    }
                }
                else
                {
                    //if nextPlatformPosition isn't the end of the list
                    //FR si nextPlatformPosition n'est pas la fin de la liste
                    if (nextPlatformPosition != 0)
                    {
                        //change nextPlatformPosition to the next one
                        //FR changement de nextPlatformPosition au prochain
                        nextPlatformPosition = nextPlatformPosition - 1;
                    }
                    //if nextPlatformPosition is the end of the list
                    //FR si nextPlatformPosition est la fin de la liste
                    else
                    {
                        //reitilaze nextPlatformPosition
                        //FR reinitialisation de nextPlatformPosition
                        nextPlatformPosition = nextPlatformPosition + 1;
                        forward = true;
                    }
                }
                
            }
        }
        
    }

    public Vector3 GetPositionOfPlatform()
    {
        return transform.position;
    }

    public Vector3 GetSizeOfPlatform()
    {
        return this.gameObject.GetComponent<Collider>().bounds.size;
    }

}
