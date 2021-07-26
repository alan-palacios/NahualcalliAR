using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float yAngleVelocity;
    void Start(){
        GetComponent<Animation>().Play("Armature|Idle");
    }
    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, yAngleVelocity, 0, Space.Self);
    }
}
