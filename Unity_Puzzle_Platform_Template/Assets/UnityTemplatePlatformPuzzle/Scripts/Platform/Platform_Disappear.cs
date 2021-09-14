using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Disappear : MonoBehaviour
{

//Public viriables
//FR variables publiques
    //use to know if the user want to make the platform disappear
    //FR utilisé pour savoir si l'utilisateur veut faire dispparaitre la plateforme
    public bool activeDisappear;
    //time before the platform will disappear
    //FR temps avant dispparition de la plateforme
    public float timeBeforeDisappear;
    //time before the platform reappear
    //temps avant réapparition de la plateforme
    public float timeBeforeReappear;

    public Material[] matList;

//Private variables
//FR variables privées
    //use to save the platform original scale
    //FR utilisé pour garder le scale d'origine de la plateforme
    private Vector3 basePlatformScale;
    //use to seve the scale of the platform when she will disappear
    //FR utilisé pour garder la scale de la platform lorsqu'elle dispparait
    private Vector3 disappearScale;
    //use to know the time when the platform will disappear
    //FR utilisé pour savoir quand la plateforme va dispparaitre
    private float disappearTimer;
    //use to know the time when the platform will reappear
    //FR utilisé pour savoir quand la plateforme va réapparaitre
    private float reappearTimer;
    //use to know if the platform have to disappear
    //FR utilisé pour savoir si la plateforme doit dispparaitre
    private bool startDisappear;
    //use to know if the platform have to reappear
    //FR utilisé pour savoir si la plateforme doit réapparaitre
    private bool startReappear;

    // Start is called before the first frame update
    // FR appelé avant la première frame
    void Start()
    {
        //if the user want to make the platform disappear
        //FR si l'utilisateur veut faire dispparaitre la plateforme
        if (activeDisappear)
        {
            //initialization of basePlatformScale
            //FR initialisation de basePlatformScale
            basePlatformScale = transform.localScale;
            //initialization of disappearScale
            //FR initialisaion de disappearScale
            disappearScale = new Vector3(0f, 0f, 0f);
            //initialization of startDisappear
            //FR initialisation de startDisappear
            startDisappear = false;
            //initialization of startReappear
            //FR initialisation de startReappear
            startReappear = false;
        }
    }

    // Update is called once per frame
    // FR appellé toutes à la fin de toutes les frames
    void Update()
    {
        
        //if the user want to make the platform disappear
        //FR si l'utilisateur veut faire dispparaitre la plateforme
        if (activeDisappear)
        {
            //if the time before the platform disappear is over and the platform have to disappear
            //FR si le temps avant la dispparitaion de la plateforme est écoulé et qu'elle doit dispparaitre
            if (Time.time > disappearTimer && startDisappear)
            {
                //change platform's scale
                //FR changement du scale de la plateforme
                transform.localScale = disappearScale;

                gameObject.GetComponent<Collider>().enabled = false;
                //set startdisappear at false
                //FR set de startDisappear à faux
                startDisappear = false;
                //set startReappear  at true
                //FR set startReappear à vrai
                startReappear = true;
                //initialize reapperTimer
                //FR initialisation de reappetTimer
                reappearTimer = Time.time + timeBeforeReappear;

                /*if (this.gameObject.GetComponent<MeshRenderer>().material == matList[1])
                {
                    this.gameObject.GetComponent<MeshRenderer>().material = matList[0];
                }
                else
                {
                    this.gameObject.GetComponent<MeshRenderer>().material = matList[1];
                }*/
                
            }

            //if the time before the platform reappear is over and the platform have to reappear
            //FR si le temps avant la réapparition de la plateforme est écoulé et qu'elle doit dispparaitre
            if (Time.time > reappearTimer && startReappear)
            {
                //change platform's scale
                //FR changement du scale de la plateforme
                transform.localScale = basePlatformScale;
                gameObject.GetComponent<Collider>().enabled = true;
                Color platformColor = this.gameObject.GetComponent<Renderer>().material.color;
                platformColor.a = 1f;
                this.gameObject.GetComponent<Renderer>().material.color = platformColor;
                //set startReappear at false
                //FR set startReappear à faux
                startReappear = false;
            }
        }
        
            
    }

    //function use to activate the platform's disappearance
    //FR fonction utilisé pour activé la disparition de la plateforme
    public void ActivePlatformDisappear()
    {
        if (!startDisappear)
        {
            //set startDisappear at true
            //FR set startDisappear à vrai
            startDisappear = true;
            //initialization of disappearTimer
            //FR initialisation de disappearTimer
            disappearTimer = Time.time + timeBeforeDisappear;

            StartCoroutine(clip());
        }
        
    }

    IEnumerator clip()
    {
        float alphaUpdater = 1f;
        bool fadeAway = true;
        Color platformColor = this.gameObject.GetComponent<Renderer>().material.color;
        while (Time.time < disappearTimer && startDisappear)
        {
            if (fadeAway)
            {
                alphaUpdater -= 0.1f;
                if (alphaUpdater <= 0f)
                {
                    fadeAway = false;
                }
            }
            else
            {
                alphaUpdater += 0.1f;
                if (alphaUpdater >= 1f)
                {
                    fadeAway = true;
                }
            }
            platformColor.a = alphaUpdater;
            this.gameObject.GetComponent<Renderer>().material.color = platformColor;
            yield return new WaitForSeconds(.1f);
        }
    }

}
