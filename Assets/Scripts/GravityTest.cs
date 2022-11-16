using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTest : MonoBehaviour
{
    public float rotateSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float z = Input.GetAxis("vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Gravity change");
            Physics.gravity = new Vector3(-9.81f, 0, 0);
            transform.Rotate(0, 0, -90);
        }
        
    }

    private void FixedUpdate()
    {
        
    }
}
