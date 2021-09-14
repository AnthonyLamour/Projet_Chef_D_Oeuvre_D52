using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage_Switch_Page : MonoBehaviour
{
    // Start is called before the first frame update
    // FR Start appellé à la première frame
    void Start()
    {
        //add the onClick function to the button
        //FR ajout de la fonction onClick sur le bouton
        this.gameObject.GetComponent<Button>().onClick.AddListener(CallSwitchPage);
    }

    // SwitchPage is use to call the switch inventory pages function
    // FR SwitchPage est utilisé pour appeller la fontction pour changer les pages de l'inventaire
    private void CallSwitchPage()
    {
        // if we switch on the next inventory page
        // FR si on passe à la page d'inventaire suivante
        if (this.gameObject.tag == "NextInventoryPage")
        {
            //call the switch function with the parameter true
            //FR appelle de la fonction de switch avec le paramètre vrai
            this.gameObject.transform.parent.gameObject.GetComponent<InventoryPage_SetUp>().SwitchPage(true);
        }
        // if we swithc on the previous inventory page
        // FR si on passe à la page d'inventaire précédente
        else if (this.gameObject.tag == "PreviousInventoryPage")
        {
            //call the switch function with the parameter false
            //FR appelle de la fonction de switch avec le paramètre faux
            this.gameObject.transform.parent.gameObject.GetComponent<InventoryPage_SetUp>().SwitchPage(false);
        }
    }
}
