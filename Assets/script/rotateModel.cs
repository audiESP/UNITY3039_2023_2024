using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateModel : MonoBehaviour
{   
    bool isdown= false;
    float rotSpeed=10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){

            isdown=true;

        }
         if(Input.GetMouseButtonUp(0)){

            isdown=false;

        }
        if(isdown){
            float mx= Input.GetAxis("Mouse X");
            float my= Input.GetAxis("Mouse Y");
            Quaternion qx=Quaternion.AngleAxis(-mx*rotSpeed,Vector3.up);
            Quaternion qy=Quaternion.AngleAxis(my*rotSpeed,Vector3.right);
            transform.rotation=transform.rotation*qx*qy;

        }
    }
}
