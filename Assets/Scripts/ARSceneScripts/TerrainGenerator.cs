using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject [] rockModels;
    public float radius;
    public int numOfRocks;
    public Vector2 minMaxScaleRock;

    // Start is called before the first frame update
    void Start()
    {
        GenerateRocks();

    }

    void OnEnable(){
        GenerateRocks();
    }
    
    public void GenerateRocks(){
        DeleteRocks();
        float angle = 0;
        float angleIncremment = 2*Mathf.PI/numOfRocks;
        for (int i = 0; i<numOfRocks; i++) {
                    GameObject randomModel = rockModels[Random.Range (0,rockModels.Length)];
                  float rndScale = Random.Range (minMaxScaleRock.x, minMaxScaleRock.y);
                  Vector3 position = new Vector3( radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle) );

                  Vector3 angles = new Vector3( 0, Random.Range(0, 36)*10, 0);
                  GameObject rock = GameObject.Instantiate( randomModel, position, Quaternion.Euler(angles) ) as GameObject;
                  rock.transform.SetParent( transform, false);
                  rock.transform.localScale = Vector3.one*rndScale;
                  angle+=angleIncremment;

        }

    }

    void DeleteRocks(){
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
