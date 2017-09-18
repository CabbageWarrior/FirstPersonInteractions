using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
        //transform.rotation = new Quaternion(
        //    cameraTransform.rotation.x,
        //    cameraTransform.rotation.y,
        //    cameraTransform.rotation.z,
        //    cameraTransform.rotation.w
        //    );
        transform.eulerAngles = cameraTransform.eulerAngles; //ToDo: FIX!!!
    }
}
