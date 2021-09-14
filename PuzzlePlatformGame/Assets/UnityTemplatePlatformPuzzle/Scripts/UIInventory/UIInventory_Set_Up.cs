using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory_Set_Up : MonoBehaviour
{

//public variables 
//FR variables publiques
    [Header("Inventory Page Settings")]
    [Header("Margin in %")]
    //margin of the inventory page
    //FR marges de la page d'inventaire
    public float marginLeftRight;
    public float marginTopBottom;
    [Header("Item Image Settings")]
    //size of the items' images in the inventory page
    //FR taille des images des items dans la page de l'inventaire
    public float itemImageWidth;
    public float itemImageHeight;
    [Header("Item Text Settings")]
    //height of the items' name in the inventory page
    //FR hauteur du nom des items dans la page de l'inventaire
    public float itemTextHeight;

    //GetMargins use to get the margins of the inventory page
    //FR GetMargins utilisé pour récupérer les marges de l'inventaire
    public Vector2 GetMargins()
    {
        //return a Vector2 that contains the margins' values
        //FR retourne un Vecteur 2D contenant la valeur des marges
        return new Vector2(GetMarginLeftRight(), GetMarginTopBottom());
    }

    //GetMarginLeftRight use to get the margin of the inventory page on the x axis
    //FR GetMarginLeftRight utilisé pour récupérer la marge de la page de l'inventaire sur l'axe x
    public float GetMarginLeftRight()
    {
        //return marginLeftRight
        //FR retourne marginLeftRight
        return marginLeftRight;
    }

    //GetMarginTopBottom use to get the margin of the inventory page on the y axis
    //FR GetMarginTopBottom utilisé pour récupérer la marge de la page de l'inventaire sur l'axe y
    public float GetMarginTopBottom()
    {
        //return marginTopBottom
        //FR retourne marginTopBottom
        return marginTopBottom;
    }

    //GetItemImageSize is use to get the width and the height of the item's image in the inventory
    //FR GetItemImageSize utilisé pour récupérer la largeur et la hauteur des images des items dans l'inventaire
    public Vector2 GetItemImageSize()
    {
        //return a Vector2 composed of ItemImageWidth and ItemImageHeight
        //FR retourne un Vector2 composé de la largeur de l'image d'un item et de la hauteur de l'image d'un item
        return new Vector2(GetItemImageWidth(), GetItemImageHeight());
    }

    //GetItemImageWidth is use to get the width of item's image in the inventory
    //FR GetItemImageWidth est utilisé pour récupérer la largeur d'une image d'un item dans l'inventaire
    public float GetItemImageWidth()
    {
        //return itemImageWidth
        //FR retourne itemImageWidth
        return itemImageWidth;
    }

    //GetItemImageWidth is use to get the height of item's image in the inventory
    //FR GetItemImageWidth est utilisé pour récupérer la hauteur d'une image d'un item dans l'inventaire
    public float GetItemImageHeight()
    {
        //return itemImageHeight
        //FR retourne itemImageHeight
        return itemImageHeight;
    }

    //GetItemImageWidth is use to get the height of item's text in the inventory
    //FR GetItemImageWidth est utilisé pour récupérer la hauteur du texte d'un item dans l'inventaire
    public float GetItemTextHeight()
    {
        //return itemTextHeight
        //FR retourne itemTextHeight
        return itemTextHeight;
    }

}
