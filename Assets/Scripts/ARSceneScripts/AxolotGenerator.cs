using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxolotGenerator : MonoBehaviour
{
    public GameObject axolotModel;
    public Vector2 minMaxQuantity;
    public Vector2 minMaxRadius;
    public float initScale;

    // Start is called before the first frame update
    void Start()
    {
        GenerateAxolots();

    }

    // Update is called once per frame
    void OnEnable()
    {
        RegenerateAxolots();
    }

    public void RegenerateAxolots(){
        DeleteAxolots();
        GenerateAxolots();
    }

    void DeleteAxolots(){
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void GenerateAxolots(){
        float axolotQuantity = Random.Range(minMaxQuantity.x, minMaxQuantity.y);

        for (int i = 0; i<axolotQuantity; i++) {

                  float dst = Random.Range( minMaxRadius.x, minMaxRadius.y);
                  float rndAngle = Random.Range (0,2*Mathf.PI);

                  Vector3 position = new Vector3( dst * Mathf.Cos(rndAngle), 0, dst * Mathf.Sin(rndAngle) );
                  Vector3 angles = new Vector3( 0, Random.Range(0, 36)*10, 0);

                  GameObject axolot = GameObject.Instantiate( axolotModel, position, Quaternion.Euler(angles) ) as GameObject;
                  axolot.transform.SetParent( transform, false);
                  axolot.transform.localScale = Vector3.one*initScale;

                  /*
                  float trasVelocity = Random.Range(solarSystemConfiguration.solarSystemData.PlanetsMinTVelocity, solarSystemConfiguration.solarSystemData.PlanetsMaxTVelocity);
                  if (Random.value > 0.5f) {
                            trasVelocity*=-1;
                  }
                  planets[i].trasVelocity = trasVelocity;

                  float rotVelocity = Random.Range(solarSystemConfiguration.solarSystemData.PlanetsMinRVelocity, solarSystemConfiguration.solarSystemData.PlanetsMaxRVelocity);
                  if (Random.value > 0.5f) {
                            rotVelocity*=-1;
                  }
                  planets[i].rotVelocity = rotVelocity;

                  planets[i].angle = rndAngle;
                  */

        }

    }
}
