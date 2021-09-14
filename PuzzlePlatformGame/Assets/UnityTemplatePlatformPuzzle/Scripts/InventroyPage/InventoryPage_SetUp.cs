using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPage_SetUp : MonoBehaviour
{
//public variable
//FR variable publique
    //itemPrefab is the prefab use to show an item in the inventory
    //FR itemPrefab est une prefab utilisée pour montrer un item dans l'inventaire
    public GameObject itemPrefab;
//private variables
//FR variables privés
    //margins is use to set the gameobject's margins with his parent
    //FR margins est utilisé pour setter les marges du gameobject avec son parent
    private Vector2 margins;
    //itemImageSize is the size of the items' image in inventory
    //FR itemImageSize est la taille de l'image des items dans l'inventaire
    private Vector2 itemImageSize;
    //screenSize is the size of the screen
    //FR screenSize correspond à la taille de l'écran
    private Vector2 screenSize;
    //maxRows is the maximum number of rows in the inventory page
    //FR maxRows est le nombre de colonnes maximale dans la page l'inventaire
    private int maxRows;
    //maxLines is the maximum number of lines in the inventory page
    //FR maxLines est le nombre de lignes maximale dans la page l'inventaire
    private int maxLines;
    //maxItem is the maximum number of item in the inventory page
    //FR maxItem est le nombre maximum d'item dans la page de l'inventaire
    private int maxItem;
    //itemSpace is the space between 2 items
    //FR itemSpace correspond à l'espace entre 2 items
    private Vector2 itemSpace;
    //itemTextHeight is the height of the item name in the inventory page
    //FR itemTextHeight est la hauteur du nom de l'item dans la page de l'inventaire
    private float itemTextHeight;
    //inventoryZone is the panel where the items will appear in the inventory page
    //FR inventoryZone est le panel où vont apparaître les objets dans la page d'inventaire
    private GameObject inventoryZone;
    //inventoryTitle is the TextMeshPro that contain the title of the inventory
    //FR inventoryTitle est le TextMeshPro qui contient le titre de l'inventaire
    private GameObject inventoryTitle;
    //inventoryPage is the TextMeshPro that contain the number of the page of the iventory
    //FR inventoryPage est le TextMeshPro qui contient le numéro de la page de l'inventaire
    private GameObject inventoryPage;
    //itemCuror is use to know where is the last item on screen in the Dictionary
    //FR itemCuror est utilisé pour connaître l'emplacement du dernière item à l'écran dans le dictionaire
    private int itemCursor;
    //playerInv is use to get the player inventory
    //FR playerInv est utilisé pour récupérer l'inventaire du joueur
    private Dictionary<int, Dictionary<string, Texture2D>> playerInv;


    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //initialize itemSpace at 0,0
        //FR initialisation de itemSpace at 0,0
        itemSpace = new Vector2(0, 0);

        //get margins in inventory page parent
        //FR récupération de la marge dans le parent de la page d'inventaire
        margins = transform.parent.gameObject.GetComponent<UIInventory_Set_Up>().GetMargins();

        //get itemImageSize in inventory page parent
        //FR récupération de la taille des images des items dans le parent de la page d'inventaire
        itemImageSize = transform.parent.gameObject.GetComponent<UIInventory_Set_Up>().GetItemImageSize();

        //get screen size
        //FR récupération de la taille de l'écran
        screenSize = new Vector2(Screen.width,Screen.height);

        //get itemTextHeight in inventory page parent
        //FR récupération de la hauteur des noms d'items dans le parent de la page d'inventaire
        itemTextHeight = transform.parent.gameObject.GetComponent<UIInventory_Set_Up>().GetItemTextHeight();

        //set inventory page anchors
        //FR set des ancres de la page d'inventaire
        this.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(margins.x / 100, margins.y / 100);
        this.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(1-margins.x / 100, 1-margins.y / 100);

        //set inventory page size
        //FR set de la taille de la page d'inventaire
        this.gameObject.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        this.gameObject.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);

        //initialization of the inventoryTitle
        //FR initialisation du titre de la page d'inventaire
        inventoryTitle = null;
        for(var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "InventoryTitle")
            {
                inventoryTitle = transform.GetChild(i).gameObject;
            }
        }

        //initialization of the inventoryZone
        //FR initialisation de la zone d'affichage de la page d'inventaire
        inventoryZone = null;
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "InventoryZone")
            {
                inventoryZone = transform.GetChild(i).gameObject;
            }
        }

        //initialization of the inventoryPage
        //FR initialisation du texte de la page de l'inventaire
        inventoryPage = null;
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "InventoryPage")
            {
                inventoryPage = transform.GetChild(i).gameObject;
            }
        }

        //set of maxRows
        //FR set de du nombre maximale de colonnes
        maxRows =SetMaxRows();

        //set of maxLines
        //FR set de du nombre maximale de lignes
        maxLines = SetMaxLines();

        //set of maxItem
        //FR set de du nombre maximale d'item
        maxItem = SetMaxItem();

        //initialization of the page
        //FR initialisation de la page
        PageInitialization();

    }

    //SetMaxRows is use to define the maximum number of rows in the inventory page
    //FR SetMaxRows est utilisé pour définir le nombre maximum de colonnes dans la page d'inventaire
    private int SetMaxRows()
    {
        //define the item space on x axis
        //FR définition de l'espace entre les items sur l'axe x
        itemSpace.x = ((inventoryZone.GetComponent<RectTransform>().rect.width % (itemImageSize.x))+ itemImageSize.x) / (int)(inventoryZone.GetComponent<RectTransform>().rect.width / (itemImageSize.x));
        Debug.Log("Taille de l'inventaire : " + inventoryZone.GetComponent<RectTransform>().rect.width);
        Debug.Log("Taille de l'image : " + itemImageSize.x);
        Debug.Log("Taille espace total : " + ((inventoryZone.GetComponent<RectTransform>().rect.width % (itemImageSize.x)) + itemImageSize.x));
        Debug.Log("nombre de colonne max : " + (int)(inventoryZone.GetComponent<RectTransform>().rect.width / (itemImageSize.x)));
        Debug.Log("Espace entre les items : " + itemSpace.x);

        //return the maximum number of rows in the inventory page
        //FR retourne le nombre maximum de colonnes dans la page de l'inventaire
        return (int)((inventoryZone.GetComponent<RectTransform>().rect.width / (itemImageSize.x))-1);
    }

    //SetMaxLines is use to define the maximum number of lines in the inventory page
    //FR SetMaxLines est utilisé pour définir le nombre maximum de lignes dans la page d'inventaire
    private int SetMaxLines()
    {
        //define the item space on y axis
        //FR définition de l'espace entre les items sur l'axe y
        itemSpace.y = ((inventoryZone.GetComponent<RectTransform>().rect.height / (itemImageSize.y + itemTextHeight))+ itemImageSize.y)/ (int)(inventoryZone.GetComponent<RectTransform>().rect.height / (itemImageSize.y + itemTextHeight));
        //return the maximum number of lines in the inventory page
        //FR retourne le nombre maximum de lignes dans la page de l'inventaire
        return (int)((inventoryZone.GetComponent<RectTransform>().rect.height / (itemImageSize.y+ itemTextHeight))-1);
    }

    //SetMaxItem is use to define the maximum number of items in the inventory page
    //FR SetMaxItem est utilisé pour définir le nombre maximum d'items dans la page d'inventaire
    private int SetMaxItem()
    {
        //returm the maximum number of rows * the maximum number of lines
        //FR retourne le nombre maximum de colonnes * le nombre maximum de lignes
        return SetMaxRows() * SetMaxLines();
    }

    //InstantiateItemInInventory is use to instantiate an object in the inventory page with his name, his texture, his ID
    //FR InstantiateItemInInventory est utilisé pour instantier un objet dans la page de l'inventaire avec son nom, sa texture et son ID
    public void InstantiateItemInInventory(string itemName,Texture2D itemTexture,int itemID)
    {
        //intantiate the new item in the inventory page
        //FR instantiation du nouvel item dans la page de l'inventaire
        GameObject newItem = Instantiate(itemPrefab, inventoryZone.transform);

        //set the position of the new item in the inventory page
        //FR set de la position du nouvel item dans la page de l'inventaire
        newItem.GetComponent<RectTransform>().anchoredPosition = new Vector2(itemImageSize.x * (itemID%maxRows) + itemSpace.x * ((itemID%maxRows) + 1) + itemImageSize.x / 2, -(itemImageSize.y + itemTextHeight) * (int)(itemID / maxRows) - itemSpace.y * ((int)(itemID / maxRows)+1) - (itemImageSize.y + itemTextHeight) / 2);

        //set of the name if the new item in the inventory page
        //FR set du nom du nouvel item dans la page de l'inventaire
        newItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;

        newItem.transform.GetChild(1).GetComponent<RawImage>().texture = itemTexture;
    }

    // SwitchPage is use to switch inventory pages
    // FR SwitchPage est utilisé pour changer les pages de l'inventaire
    public void SwitchPage(bool next)
    {
        //initialization of the current page
        //FR initialisation de la page actuelle
        int currentPage=0;
        //initialization of i
        //FR initialisation de i
        int i=itemCursor;

        //get the current page
        //FR récupération de la page actuelle
        currentPage = GetNumberInString(inventoryPage.GetComponent<TextMeshProUGUI>().text);

        //if the player switch to the next page and there is a next page
        //FR si le joueur switch sur la page suivante et qu'il y a encore une page
        if (next && playerInv.Count > maxItem * currentPage)
        {
            //destroy all item in inventory page
            //FR destruiction de tout les items de la page de l'inventaire
            foreach (Transform child in inventoryZone.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            //set of the currentPage
            //FR set de la page actuelle
            currentPage++;

            //if this is the last page
            //FR si c'est la dernière page
            if (playerInv.Count < maxItem*currentPage)
            {
                //instantiation of the player's items in the inventory
                //FR instantiation des items du joueur dans l'inventaire
                for (i=itemCursor; i < playerInv.Count; i++)
                {
                    foreach (KeyValuePair<string, Texture2D> element in playerInv[i])
                    {
                        InstantiateItemInInventory(element.Key, element.Value, i - maxItem * (currentPage - 1));
                    }
                }
            }
            //if this is not the last page
            //FR si ce n'est pas la dernière page
            else
            {
                //instantiation of the player's items in the inventory
                //FR instantiation des items du joueur dans l'inventaire
                for (i = itemCursor; i < itemCursor+maxItem; i++)
                {
                    foreach (KeyValuePair<string, Texture2D> element in playerInv[i])
                    {
                        InstantiateItemInInventory(element.Key, element.Value, i - maxItem * (currentPage - 1));
                    }
                }
            }

        }
        //else if the currentPage is not the fisrt and the player switch to the previous page
        //FR sinon si la page actuelle n'est pas la première et que la joueur switch à la page précédente
        else if (currentPage > 1 && !next)
        {
            //destroy all item in inventory page
            //FR destruiction de tout les items de la page de l'inventaire
            foreach (Transform child in inventoryZone.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            //set of the currentPage
            //FR set de la page actuelle
            currentPage--;

            //instantiation of the player's items in the inventory
            //FR instantiation des items du joueur dans l'inventaire
            for (i = maxItem*currentPage; i > maxItem*(currentPage-1); i--)
            {
                foreach (KeyValuePair<string, Texture2D> element in playerInv[i-1])
                {
                    InstantiateItemInInventory(element.Key, element.Value, i - maxItem*(currentPage-1)-1);
                }
            }

            //set of i for the itemCursor
            //FR set de i pour le cusor
            i = i + maxItem;
        }

        //set of itemCursor
        //FR set de l'itemCursor
        itemCursor = i;

        //update of the inventory page text
        //FR mise à jour du texte de la page de l'inventaire
        inventoryPage.GetComponent<TextMeshProUGUI>().text = "Page " + currentPage.ToString();
    }

    //GetNumberInString is use to get the final number in a string
    //FR GetNumberInString est utilisé pour récupérer le dernier nombre dans une chaine
    private int GetNumberInString(string str)
    {

        //initialization of i
        //FR initialisation de i
        int i = str.Length - 1;

        //string that contains all the numbers
        //FR chaine de caractère contenant tous les nombres
        string numbers = "0123456789";

        //string that will contains all the numbers of the string
        //FR chaine contenant tous les nombres de la chaine
        string tmpres = "";

        //res is the number that will be return
        //FR res est le nomhre retourné
        int res = 0;

        //get the number in the string
        //FR récupération du nombre dans la chaine
        while(numbers.IndexOf(str[i])!=-1 && i > -1)
        {
            tmpres = str[i] + tmpres;
            i--;
        }

        //get the result in res
        //FR récupération du résultat dans res
        res = int.Parse(tmpres);

        //return res
        //FR renvoie de res
        return res;
    }

    //SetPlayerInv is use to set playerInv
    //FR SetPlayerInv est utilisé pour setter playerInv
    public void SetPlayerInv(Dictionary<int,Dictionary<string,Texture2D>> newPlayerInv)
    {

        //set playerInv
        //FR set de playerInv
        playerInv = newPlayerInv;
        
    }

    //PageIniatization is use to initialize the page
    //FR PageInitialisatoin est utilisé pour initialiser la page
    private void PageInitialization()
    {
        //if this is the last page
        //FR si c'est la dernière page
        if (playerInv.Count < maxItem)
        {
            //instantiation of the player's items in the inventory
            //FR instantiation des items du joueur dans l'inventaire
            for (itemCursor = 0; itemCursor < playerInv.Count; itemCursor++)
            {
                foreach(KeyValuePair<string,Texture2D> element in playerInv[itemCursor])
                {
                    InstantiateItemInInventory(element.Key, element.Value, itemCursor);
                }
            }
        }
        //else if this is not the last page
        //FR sinon si ce n'est pas la dernière page
        else
        {
            //instantiation of the player's items in the inventory
            //FR instantiation des items du joueur dans l'inventaire
            for (itemCursor = 0; itemCursor < maxItem; itemCursor++)
            {
                foreach (KeyValuePair<string, Texture2D> element in playerInv[itemCursor])
                {
                    InstantiateItemInInventory(element.Key, element.Value, itemCursor);
                }
            }
        }
        
    }
}
