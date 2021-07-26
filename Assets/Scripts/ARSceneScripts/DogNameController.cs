using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogNameController : MonoBehaviour
{
    public InputField inputDogName;
    public GameObject objDogName;
    TextMesh dogname;
    // Start is called before the first frame update
    void Start()
    {
        dogname = objDogName.GetComponent<TextMesh>();
        if (PlayerPrefs.HasKey("dogname")) {
            dogname.text = PlayerPrefs.GetString("dogname");
        }else{
            dogname.text = "Nombrame!";
        }
    }

    void Update () {

        if (Input.GetMouseButtonDown(0)) {
                RaycastHit  hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                 if (Physics.Raycast(ray, out hit)) {
                     if (hit.transform.gameObject.tag == "DogName" ){
                         Debug.Log( "My object is clicked by mouse");
                         inputDogName.ActivateInputField();
                     }
                 }
         }
    }

    public void UpdateDogName(InputField inputField){
        dogname.text = inputField.text;
    }

    public void SaveDogName(InputField inputField){
        dogname.text = inputField.text;
        PlayerPrefs.SetString("dogname", inputField.text);
    }
}
