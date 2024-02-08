using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedController : MonoBehaviour

 
{

    public float xLoc = 0;
    public float BedSpeed = .1f;
   
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)&& xLoc > -9f){
            xLoc -= .07f;
             //transform.rotation = new Quaternion()
        }
        

         if (Input.GetKey(KeyCode.X)&& xLoc < 9f){
            xLoc += .07f;

         }

         this.transform.position = new Vector3(xLoc, transform.position.y, transform.position.z);
    }
}
