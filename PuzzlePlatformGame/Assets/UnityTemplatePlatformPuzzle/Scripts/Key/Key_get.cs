using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Key_get : MonoBehaviour
{

//public variable
//FR variable publique
    //myCamObj is the camera prefab use to make the temporary render
    //FR myCamObj est la préfab de la caméra utilisé pour faire le rendu temporaire
    public GameObject myCamObj;
//private variables
//variables privées
    //temporary render of the object
    //FR rendu temporaire de l'objet
    private RenderTexture testTexture;
    //texture that will be send to the inventory
    //FR texture qui sera envoyé à l'inventaire
    private Texture2D textureToSend;

    //OnTriggerEnter use when something enter the object's trigger
    //FR OnTriggerEnter utilisé quand quelque chose entre dans la trigger de l'objet
    private void OnTriggerEnter(Collider other)
    {
        //if the other object is the player
        //FR si l'objet est le joueur
        if (other.tag == "Player")
        {

            //create a temporary RenderTexture
            //FR création d'un render de texture temporaire
            testTexture = new RenderTexture(256, 256, 24);
            testTexture.Create();

            //create a temporary camera to set the texture
            //FR création d'une caméra temporaire pour setter la texture
            GameObject myCam=Instantiate(myCamObj, transform.localPosition + new Vector3(0f, 0f, -1f), transform.rotation,transform);

            //set the texture
            //FR set de la texture
            myCam.GetComponent<Camera>().targetTexture = testTexture;
            myCam.GetComponent<Camera>().Render();

            //start the render
            //FR lancement du rendu
            RenderTexture.active = testTexture;

            //set the textureToSend with the temporary render
            //FR set de textureToSend avec le render temporaire
            textureToSend = new Texture2D(256, 256, TextureFormat.RGB24, false);
            textureToSend.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
            textureToSend.Apply();

            //end of the render
            //FR fin du rendu
            RenderTexture.active = null;

            //add the key to the player inventory
            //FR ajout de la clé à l'inventaire du joueur
            other.GetComponent<Character_inventory>().AddToInventory(this.name,textureToSend);

            //destroy the gameObject
            //FR destruction du gameObject
            Destroy(this.gameObject);
        }
    }
}
