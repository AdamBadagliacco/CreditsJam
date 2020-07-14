using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWords : MonoBehaviour
{ 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         transform.Translate(Vector3.up * Time.deltaTime); // Move the object up along its y axis 1 unit/second.

         if(transform.position.y >= 6 && transform.position.x != -20){ //Don't want to kill the default latter everything is spawining of off so it's left safe at -20
             Destroy(gameObject);
         }
    }
}
