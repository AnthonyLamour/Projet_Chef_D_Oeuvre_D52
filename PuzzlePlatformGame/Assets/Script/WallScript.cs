using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    public Material[] matList;
    private float min;
    private float max;
    private float rndSize;
    private Transform childTransform;

    // Start is called before the first frame update
    void Start()
    {
        min = 0.3f;
        max = 1f;
        for(int i=1; i < transform.childCount; i++)
        {
            rndSize = Random.Range(min, max);
            childTransform = transform.GetChild(i).GetComponent<Transform>();
            childTransform.localScale = new Vector3(rndSize, childTransform.localScale.y,childTransform.localScale.z);
            int rndMat = Random.Range(0, 3);
            transform.GetChild(i).gameObject.GetComponent<Renderer>().material = matList[rndMat];
        }
        Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
