using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Switch_Lamp : MonoBehaviour
{

//public variables
//FR variables publique
    //use to get the 2 different materials of the lamp
    //FR utilisé pour récupérer les 2 matériaux différents de la lampe
    public Material[] lampColor;

//privat variables
//FR variables privé
    //use to know if the light is active
    //FR utilisé pour savoir si la lumière est actif
    private bool activate;
    //use to get the material array of the button
    //FR utilisé pour  récupérer le tableau de matériel du bouton
    private Material[] matArray;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //set active at false
        //FR set active à faux
        activate = false;
        //initialize matArray
        //FR initialisation de matArray
        matArray = this.gameObject.GetComponent<MeshRenderer>().materials;
        //set matArray first index to the first lamp color
        //FR set du premier index de matArray à la première couleur de la lampe
        matArray[0] = lampColor[0];
        //set the lamp color
        //FR set de la couleur de la lampe
        this.gameObject.GetComponent<MeshRenderer>().materials = matArray;
    }

    //SwitchLampColor use to change the color of the lamp
    //SwitchLampColor utilisé pour changer la couleur de la lampe
    public void SwitchLampColor()
    {
        //if the lamp is active
        //FR si la lampe est activé
        if (activate)
        {
            //set matArray first index to the first lamp color
            //FR set du premier index de matArray à la première couleur de la lampe
            matArray[0] = lampColor[0];
            //set the lamp color
            //FR set de la couleur de la lampe
            this.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            //set active at false
            //FR set active à faux
            activate = false;
        }
        //else if the lamp isn't active
        //FR sinon si la lampe n'est pas active
        else
        {
            //set matArray first index to the second lamp color
            //FR set du premier index de matArray à la deuxième couleur de la lampe
            matArray[0] = lampColor[1];
            //set the lamp color
            //FR set de la couleur de la lampe
            this.gameObject.GetComponent<MeshRenderer>().materials = matArray;
            //set active at true
            //FR set active à vrai
            activate = true;
        }
    }
}
