using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFinalTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<UnityEngine.UI.Text>().text = "Time: " + PassingVarThroughScene.finalTime;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
