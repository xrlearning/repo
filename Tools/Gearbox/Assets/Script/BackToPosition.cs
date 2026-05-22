using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public Transform homePosition;

    private void HandleRelease()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = homePosition.position;
        transform.rotation = homePosition.rotation;
    }
}



