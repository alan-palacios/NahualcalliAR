using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxolotBehaviour : MonoBehaviour
{
     [Header("Spawn Settings")]
    public Vector2 minMaxScale;
    public Vector2 minMaxSpeed;
    public float scaleIncrement;
    public float timeBetweenScaleIncrease;
    public float raycastDistance;

    [Header("Rotation Obstacle Settings")]
    public Vector2 minMaxRotationRange;
    float rotationDirection;
    public float timeToRotate;
    public float timeBetweenRotations;

     [Header("Rotation Forward settings")]
    public Vector2 minMaxTimeToRotationOnForward;
    float timeToRotateOnForwardMove;
    float timeSinceLastRotation = 0;

    float speed;
    bool haveHit=false;

    Rigidbody rb;
    Animator anim;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();

        speed = Random.Range(minMaxSpeed.x, minMaxSpeed.y);
        timeToRotateOnForwardMove = Random.Range(minMaxTimeToRotationOnForward.x, minMaxTimeToRotationOnForward.y);

        rb = GetComponent<Rigidbody>();
        StartCoroutine(IncreaseScale());
        StartCoroutine(CountTime());
    }

    IEnumerator IncreaseScale(){
        float axolotScale = Random.Range(minMaxScale.x, minMaxScale.y);
        while(transform.localScale.x<axolotScale){
            transform.localScale+=Vector3.one*scaleIncrement;
            yield return new WaitForSeconds(timeBetweenScaleIncrease);
        }
    }


    IEnumerator CountTime(){
        while(true){
            yield return new WaitForSeconds(1);
            timeSinceLastRotation+=1;
        }
    }
    // Update is called once per frame
    void Update()
    {

        Vector3 direction =  transform.forward;
        rb.velocity = direction * speed;


        int layerMask = 1 << 8;
        RaycastHit hit;

        //frente
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance, layerMask)){

            if (!haveHit) {
                haveHit = true;
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (randomBoolean()) {
                    rotationDirection = 1;
                }else{
                    rotationDirection = -1;
                }

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit, raycastDistance+5, layerMask)){
                    rotationDirection = 1;
                }

                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit, raycastDistance+5, layerMask)){
                    rotationDirection = -1;
                }

                timeSinceLastRotation = 0;
                StartCoroutine(RotationOnObstacleDetected());
            }

        }
        else{
            if (haveHit) {
                haveHit = false;
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            }
        }

        if (timeSinceLastRotation > timeToRotateOnForwardMove) {
            timeSinceLastRotation=0;
            timeToRotateOnForwardMove = Random.Range(minMaxTimeToRotationOnForward.x, minMaxTimeToRotationOnForward.y);
            if (randomBoolean()) {
                rotationDirection = 1;
            }else{
                rotationDirection = -1;
            }
            StartCoroutine(RotationOnObstacleDetected());
        }
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag == "Axolot") {
            Debug.Log("choca");

        }
    }
    IEnumerator RotationOnObstacleDetected(){
        anim.SetBool("isRotating", true);
        float actualRotation = 0;
        float totalRotation = Random.Range(minMaxRotationRange.x, minMaxRotationRange.y);


        float rotationSpeed = (totalRotation*timeBetweenRotations)/timeToRotate;

        while(actualRotation<totalRotation){
            actualRotation+=rotationSpeed;
            transform.Rotate(0.0f, rotationDirection*rotationSpeed, 0.0f);
            yield return new WaitForSeconds(timeBetweenRotations);
        }
        haveHit = false;
        //yield return new WaitForSeconds(1);
        anim.SetBool("isRotating", false);
    }

    bool randomBoolean (){
        if (Random.value >= 0.5)
        {
            return true;
        }
        return false;
    }

}
