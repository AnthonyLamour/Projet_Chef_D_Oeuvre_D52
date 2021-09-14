using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnPanelScript : MonoBehaviour
{

    private GameObject player;


    public void startRespawn(GameObject newPlayer)
    {
        player = newPlayer;
        Color tmpCol= this.GetComponent<Image>().color;
        tmpCol.a=1f;
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        for (float ft = 1f; ft >= 0; ft -= 0.1f)
        {
            Color c = this.GetComponent<Image>().color;
            c.a = ft;
            this.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0.1f);
        }
        player.GetComponent<Character_Controller>().EndRespawn();
    }
}
