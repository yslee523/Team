using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCam : MonoBehaviour
{

    public Transform target;
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;

    //public Vector3 lastposition;

    public Animator anim;

    void Update()
    {
        
        if(!target)
        return;
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        //currentRotationAngle= Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);
        Quaternion currentRotation = Quaternion.Euler(0, 45.0f, 0);

        Vector3 tempDis = target.position;
        tempDis -= currentRotation * Vector3.forward * distance;
        tempDis.y = currentHeight;
        //if(Mathf.Abs(lastposition.x-tempDis.x)>0.01)
        //{
        //    
        //}
        if(anim.GetFloat("Forward")>0.001 | anim.GetFloat("Forward")==0)
        {
            transform.position = tempDis; 
        }
        transform.LookAt(target);

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
   
}
