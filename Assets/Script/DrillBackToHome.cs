using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillBackToHome : MonoBehaviour
{
    // the drill that has to go back to its home
    public GameObject drill = null;

    // the game object with the home position
    public GameObject home = null;

    // Start is called before the first frame update
    void Start()
    {
        drill.transform.position = home.transform.position;
        drill.transform.rotation = Quaternion.Euler(0,-90,90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}




