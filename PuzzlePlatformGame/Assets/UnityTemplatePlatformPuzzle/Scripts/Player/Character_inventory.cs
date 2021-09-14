using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Character_inventory : MonoBehaviour
{
//public variables
//FR variables publiques
    //use to get the inventory canvas
    //FR utilisé pour récupérer le canvas de l'inventaire
    public Canvas playerInventoryCanvas;

//private variables
//FR variables privées
    //list every item that the player have sort by name and original prefab
    //FR liste de tout les noms des objets possédés par le joueur
    private Dictionary<int, Dictionary<string,Texture2D>> playerInventory;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //playerInventory initialization
        //FR initialisation de l'inventaire du joueur
        playerInventory = new Dictionary<int, Dictionary<string, Texture2D>>();
    }

    //GetInventory use by an other object to get the playerInventory
    //FR GetInventory utilisé par un autre objet pour récupérer l'inventaire du joueur
    public Dictionary<int, Dictionary<string, Texture2D>> GetInventory()
    {
        //return playerInventory
        //FR retourne l'inventaire du joueur
        return playerInventory;
    }

    //AddToInventory use by other object to add something in playerInventory
    //FR AddToInventory utilisé par un autre objet pour ajouté quelque chose dans l'inventaire du joueur
    public void AddToInventory(string objectName,Texture2D invtexture)
    {
        //Add the new object
        //FR ajout du nouvel objet
        playerInventory.Add(playerInventory.Count, new Dictionary<string, Texture2D>());
        playerInventory[playerInventory.Count - 1].Add(objectName, invtexture);
    }

    //RemoveFromInventory use by other object to remove something from playerInventory
    //FR RemoveFromInventory utilisé par un autre objet pour retiré un objet de l'inventaire du joueur
    public void RemoveFromInventory(int id)
    {
        //remove the object that have the name objectName in playerInventory
        //FR retire l'objet qui a le nom objectName dans l'inventaire du joueur
        playerInventory.Remove(id);
    }
}
