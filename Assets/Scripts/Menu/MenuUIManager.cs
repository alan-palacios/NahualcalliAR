using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public GameObject tutorialParent;
    public GameObject nextBtn;
    public GameObject previousBtn;
    public GameObject [] tutorialSteps;
    public SoundManager soundManager;

    int index = 0;

    public void Start(){
        previousBtn.SetActive(false);
    }

    void Update(){
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();

                return;
            }
        }
    }

    public void StartAR()
    {
        soundManager.PlaySound("buttonClick");
        SceneManager.LoadScene("ARScene");
    }

    public void OpenURL(){
        Application.OpenURL("https://nahualcalli.com/");
    }

    public void ShowTutorial(){
        soundManager.PlaySound("buttonClick");
        tutorialParent.SetActive(true);
    }

    public void CloseTutorial(){
        soundManager.PlaySound("buttonClick");
        tutorialParent.SetActive(false);
    }

    public void NextStep(){
        soundManager.PlaySound("buttonClick");
        tutorialSteps[index].SetActive(false);
        index+=1;
        UpdateTutorialStep();
    }
    public void PreviousStep(){
        soundManager.PlaySound("buttonClick");
        tutorialSteps[index].SetActive(false);
        index-=1;
        UpdateTutorialStep();
    }
    public void UpdateTutorialStep(){

        if (index<=0) {
            previousBtn.SetActive(false);
            index = 0;
        }else{
            previousBtn.SetActive(true);
        }

        if (index>=2) {
            nextBtn.SetActive(false);
            index = 2;
        }else{
            nextBtn.SetActive(true);
        }
        tutorialSteps[index].SetActive(true);
    }




}
