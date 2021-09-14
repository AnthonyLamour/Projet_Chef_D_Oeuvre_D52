using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{

//public variables
//FR variables publiques
    //movement speed of the Player
    //FR vitesse de mouvement du joueur
    public float moveSpeed;
    //force of the Player's jump
    //FR intensité du saut du joueur
    public float jumpForce;
    //force of the gravity on the Player
    //FR intensité de la gravitée sur le joueur
    public float gravityForce;
    //reference to the inventory page
    //FR référence à la page d'inventaire
    public GameObject inventoryPage;

    public string invTouch;

    public Vector3 lastCheckpoint;

    public GameObject respawnPanel;

//private variables
//FR variables privées
    //use to save the y of the movedirection
    //FR utilisé pour garder la valeur y de movedireciton
    private float ySave;
    //reference to the CharacterController of the Player
    //FR référence au CharacterController du joueur
    private CharacterController controller;
    //movement direction of the Player
    //FR direction du mouvement du joueur
    private Vector3 moveDirection;
    //gameController is the gameController of the scene
    //FR gameController est le gameController de la scène
    private GameObject gameController;
    //myPage is use to register the inventory page object
    //FR myPage est utilisé pour enregistrer l'objet de la page de l'inventaire
    private GameObject myPage;

    private bool isOnPlatform;

    private bool isRespawning;
    
    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {

        //Get the CharacterController of the Player
        //FR récupération du CharacterController du joueur
        controller = this.GetComponent<CharacterController>();
        //initialization of gameController
        //FR initialisation du gameController
        gameController = FindGameController();
        //initialization of myPage
        //FR initialisation de myPage
        myPage = null;

        isOnPlatform = false;

        lastCheckpoint = transform.position;

        isRespawning = true;

        respawnPanel.GetComponent<RespawnPanelScript>().startRespawn(this.gameObject);
    }

    // Update is called once per frame
    // FR appellé toutes les frames
    void Update()
    {

        if (!isRespawning)
        {
            //save the y of moveDirection
            //FR garder la valeur y de moveDirection
            ySave = moveDirection.y;

            //movement of the Player depending of the input
            //FR mouvement du joueur selon les inputs
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));

            //apply speed to movement
            //FR application de la vitesse aux mouvements
            moveDirection = moveDirection.normalized * moveSpeed;

            //restore the y of moveDirection
            //FR restoration de la valeur y de moveDirection
            moveDirection.y = ySave;

            //if the player use the inventory input and the game is paused
            //FR si le joueur utilise l'input de l'inventaire et que le jeu est en pause
            if (Input.GetKeyDown(invTouch) && gameController.GetComponent<GameController>().GetGameState() == "Pause")
            {
                //destruction of the inventory page
                //FR destruction de la page de l'inventaire
                Destroy(myPage.gameObject);
                //set the gameState to Runing
                //FR set du gameState à Runing
                gameController.GetComponent<GameController>().SetGameState("Runing");
                //unlock the mouse cursor
                //FR débloque le curseur de la souris
                Cursor.lockState = CursorLockMode.Locked;
                //make the cursor visible
                //FR rend visible le curseur
                Cursor.visible = false;
            }

            //if the Player is on the ground
            //FR si le joueur touche le sol
            if (controller.isGrounded)
            {

                //reset the gravity apply on moveDirection
                //FR reset de la gravité appliqué sur moveDirection
                moveDirection.y = 0f;

                //if the Player jump
                //FR si le joueur saute
                if (Input.GetButtonDown("Jump"))
                {

                    transform.parent = null;
                    isOnPlatform = false;
                    //add jumpForce to the movement direction of the Player
                    //FR ajout de jumpForce à la direction du joueur
                    moveDirection.y = jumpForce;

                }

                //if the player use the inventory input
                //FR si le joueur utilise l'input de l'inventaire
                if (Input.GetKeyDown(invTouch))
                {



                    //if the game isn't paused
                    //FR si le jeu n'est pas en pause
                    if (gameController.GetComponent<GameController>().GetGameState() != "Pause")
                    {
                        //instantiation of the inventoryPage
                        //FR instantiation de la page d'inventaire
                        myPage = Instantiate(inventoryPage, this.gameObject.GetComponent<Character_inventory>().playerInventoryCanvas.transform);
                        //send the player inventory to the inventoryPage
                        //FR envoie de l'inventaire du joueur vers la page de l'inventaire
                        myPage.GetComponent<InventoryPage_SetUp>().SetPlayerInv(transform.gameObject.GetComponent<Character_inventory>().GetInventory());
                        //set the gameState to pause
                        //FR set du gameState à pause
                        gameController.GetComponent<GameController>().SetGameState("Pause");
                        //unlock the mouse cursor
                        //FR débloque le curseur de la souris
                        Cursor.lockState = CursorLockMode.None;
                        //make the cursor visible
                        //FR rend visible le curseur
                        Cursor.visible = true;
                    }

                }

            }

            //apply gravity on the Player
            //FR application de la gravité sur le joueur
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityForce * Time.deltaTime);

            //move the Player
            //FR déplacement du joueur
            controller.Move(moveDirection * Time.deltaTime);

            //if mouse left click
            //FR si click guauche de la souris
            if (Input.GetMouseButtonDown(0))
            {
                //creation of a ray starting at the player position and taking the foward direction of the player
                //FR création d'un ray commençant à la position du joueur et prennant la direciton de là où regarde le joueur
                Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

                //declaration of the hit object
                //FR déclaration de l'objet touché
                RaycastHit hit;

                //trace the raycast and check if it hit something
                //FR taçage du raycat et vérificaiton de si il touche quelque chose
                if (Physics.Raycast(ray, out hit, 10))
                {
                    //if raycast hit a button
                    //FR si le raycast touche un bouton
                    if (hit.transform.gameObject.tag == "Button")
                    {
                        //switch the button
                        //FR changement du bouton
                        hit.transform.gameObject.GetComponent<Button_Use>().SwitchButton();
                    }
                    //if the raycast hit a Door
                    //FR si le raycast touche une porte
                    else if (hit.transform.gameObject.tag == "Door")
                    {
                        //Try to open the door with the player's keys
                        //FR tentative d'ouverture de la porte avec les clés du joueur
                        hit.transform.gameObject.GetComponent<Door_Open>().OpenWithKey(transform.gameObject.GetComponent<Character_inventory>().GetInventory());
                    }
                }
            }
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

    //when the player touch something
    //FR quand le joueur touche quelque chose
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //if this is a platform
        //FR si c'est une plateforme
        if (hit.gameObject.tag == "Platform")
        {
            //try to know if the platform is moving and if this is true change player's parent
            //FR essaie de savoir si la plateforme bouge et si c'est vrai change le parent du player
            try
            {
                if (hit.gameObject.GetComponent<Platform_Move>() != null && !isOnPlatform)
                {
                    if (hit.gameObject.GetComponent<Platform_Move>().movePlatform)
                    {
                        Vector3 tmpPlatformPosition = hit.gameObject.GetComponent<Platform_Move>().GetPositionOfPlatform();
                        Vector3 tmpPlatfomSize = hit.gameObject.GetComponent<Platform_Move>().GetSizeOfPlatform();
                        float xMin= tmpPlatformPosition.x - tmpPlatfomSize.x/2;
                        float xMax= tmpPlatformPosition.x + tmpPlatfomSize.x/2;
                        float zMin= tmpPlatformPosition.z - tmpPlatfomSize.z/2;
                        float zMax= tmpPlatformPosition.z + tmpPlatfomSize.z/2;
                        if(transform.position.y>tmpPlatformPosition.y && (transform.position.z>zMin && transform.position.z<zMax) && (transform.position.x>xMin && transform.position.x < xMax))
                        {
                            transform.parent = hit.transform;
                            isOnPlatform = true;
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                Debug.Log("error"+e);
            }

            //try to know if the platform have to disappear and if this is true active the disappearance
            //FR essaie de savoir si la plateforme doit dispparaitre et si c'est vrai active la dispparition
            try
            {
                if (hit.gameObject.GetComponent<Platform_Disappear>() != null)
                {
                    if (hit.gameObject.GetComponent<Platform_Disappear>().activeDisappear)
                    {
                        hit.gameObject.GetComponent<Platform_Disappear>().ActivePlatformDisappear();
                    }
                }
                
            }
            catch (Exception e)
            {
                Debug.Log("error" + e);
            }

        }
        else
        {
            transform.parent = null;
        }
    }


    public Vector3 GetLastCheckpoint()
    {
        return lastCheckpoint;
    }

    public void SetLastChecpoint(Vector3 newCheckpoint)
    {
        lastCheckpoint = newCheckpoint;
    }

    public void TPToLastCheckpoint()
    {

        this.transform.position = lastCheckpoint;
        isRespawning = true;
        respawnPanel.SetActive(true);
        respawnPanel.GetComponent<RespawnPanelScript>().startRespawn(this.gameObject);
        moveDirection = new Vector3(0, 0, 0);

    }

    public void EndRespawn()
    {
        respawnPanel.SetActive(false);
        isRespawning = false;
    }

    public bool GetIsRespawning()
    {
        return isRespawning;
    }

}
