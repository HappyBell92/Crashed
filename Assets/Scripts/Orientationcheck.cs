using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orientationcheck : MonoBehaviour
{
    public float sensX;
    public float sensY;
    float xRotation = 400;
    float yRotation = 400;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, -transform.up, Color.red, Time.deltaTime);

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
