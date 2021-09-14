using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{

//public variables
//FR variables publiques
    //Transform of the camera's target
    //FR Transform de l'objet ciblé par la caméra
    public Transform target;
    //bool that is use to know if the user want to auto generate the offset
    //FR bool utilisé pour savoir si l'utilisateur veut générer l'offset automatiquement
    public bool autoSetOffset;
    //How far from the camera will be from the target
    //FR distance à laquelle va se trouver la caméra par rapport à l'objet ciblé
    public Vector3 offset;
    //Speed of the camera rotation
    //FR vitesse de rotation de la caméra
    public float rotateSpeed;
    //Pivot point of the camera
    //FR point de pivot de la caméra
    public Transform pivot;
    //Max view angle of the camera
    //FR angle de vue maximale de la caméra
    public float maxViewAngle;
    //Min view angle of the camera
    //FR angle de vue minimal de la caméra
    public float minViewAngle;
    //Invert the Y Axis
    //FR inverse l'axe Y
    public bool invertCameraY;

//private variables
 //FR variables privés
    //use for rotate object on horizontal axis
    //FR utilisé pour tourner les objets sur l'axe horizontal
    private float horizontalRotation;
    //use for rotate object on vertical axis
    //FR utilisé pour tourner les objets sur l'axe vertical
    private float verticalRotation;
    //Y angle that we want
    //FR angle Y voulu
    private float desiredYAngle;
    //X angle that we want
    //FR angle X voulu
    private float desiredXAngle;
    //rotation of the camera
    //FR rotation de la caméra
    private Quaternion rotation;
    //gameController is the gameController of the scene
    //FR gameController est le gameController de la scène
    private GameObject gameController;


    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {

        //make the cursor disaper and lock in the center of the window
        //FR disparition du curseur et locker au centre de la fenêtre
        Cursor.lockState = CursorLockMode.Locked;

        //if the user want to auto set the offset
        //FR si l'utilisateur veut générer l'offset automatiquement
        if (autoSetOffset)
        {
            //initialise the offset
            //FR initialisation de l'offset
            offset = target.position - transform.position;
        }

        //set the position of the pivot on the target
        //FR set de la postion du pivot sur l'objet ciblé
        pivot.position = target.position;

        //attach the pivot to the target
        //FR attache le pivot sur l'objet ciblé
        pivot.parent = target;

        //initialization of gameController
        //FR initialisation du gameController
        gameController = FindGameController();

    }

    // Update is called once at the end of frame
    // FR appellé toutes à la fin de toutes les frames
    void LateUpdate()
    {

        //if the game isn't paused
        //FR si le jeu n'est pas en pause
        if (gameController.GetComponent<GameController>().GetGameState() != "Pause")
        {

            if (!target.GetComponent<Character_Controller>().GetIsRespawning())
            {
                //Get the x position of the mouse
                //FR récupération de la position en x de la souris
                horizontalRotation = Input.GetAxis("Mouse X") * rotateSpeed;

                //Get the y position of the mouse
                //FR récupération de la position en y de la souris
                verticalRotation = Input.GetAxis("Mouse Y") * rotateSpeed;
            }
            else
            {
                horizontalRotation = 0;
                verticalRotation = 0;
            }
            

            //Rotate the camera target
            //FR rotation de l'objet ciblé par la caméra
            target.Rotate(0, horizontalRotation, 0);

            //Rotate the pivot
            //FR rotation du pivot
            if (invertCameraY)
            {
                pivot.Rotate(-verticalRotation, 0, 0);
            }
            else
            {
                pivot.Rotate(verticalRotation, 0, 0);
            }


            //Limit the camera view angle
            //FR limitation de l'angle de vue de la caméra
            if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
            {
                pivot.rotation = Quaternion.Euler(maxViewAngle, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
            }
            if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f - minViewAngle)
            {
                pivot.rotation = Quaternion.Euler(360f - minViewAngle, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
            }

            //set of the desire Y angle
            //FR set de l'angle Y voulu
            desiredYAngle = target.eulerAngles.y;

            //set of the desire X angle
            //FR set de l'angle X voulu
            desiredXAngle = pivot.eulerAngles.x;

            //set of rotation
            //FR set de la rotation
            rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

            //apply the offset and the rotation
            //FR application de l'offset et de la rotaion
            transform.position = target.position - (rotation * offset);

            //make sure that camera not going under the ground
            //FR vérifier que la caméra ne passe pas sous le sol
            if (transform.position.y < target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
            }

            //make the camera look at the target
            //FR fait en sorte que la caméra regarde l'objet ciblé
            transform.LookAt(target);
        }

    }

    //FindGameController use to get the gameController of the scene
    //FR FindGameController utilisé pour récupérer le gameController de la scène
    private GameObject FindGameController()
    {
        //find the gameController and return
        //FR retrouvé le gameController et le renvoyer
        return GameObject.FindGameObjectWithTag("GameController");
    }
}
