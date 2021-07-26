using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Recorder;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIManager : MonoBehaviour
{
    public RecordManager recordManager;
    public GameObject stopBtn;
    public GameObject startVideoBtn;
    public GameObject takeScreenshotBtn;
    public GameObject returnBtn;
    public GameObject switchBtn;
    public Sprite CamToVid;
    public Sprite VidToCam;
    public GameObject canvas;
    public GameObject doubleTapMessagge;
    public GameObject stopRecordMessagge;
    public GameObject shareMediaMessagge;
    public Text typeOfMedia;
    public bool isRecording = false;
    string filepath;
    private const string GALLERY_PATH = "/../../../../DCIM/NahualcalliAR";
    public SoundManager soundManager;

    void Start(){
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //PlayerPrefs.SetInt("showShareMessagge",1);
        if (!PlayerPrefs.HasKey("showDT")){
             PlayerPrefs.SetInt("showDT",0);
            doubleTapMessagge.SetActive(true);
         }

         if (!PlayerPrefs.HasKey("showShareMessagge")){
              PlayerPrefs.SetInt("showShareMessagge",1);
          }
    }

#region shareMethods
    public void ShowShareMediaDialog(string txt){
        if (PlayerPrefs.GetInt("showShareMessagge")==1) {
            typeOfMedia.text = txt;
            shareMediaMessagge.SetActive(true);
        }
    }

    public void ShareMedia(){
        soundManager.PlaySound("buttonClick");
        shareMediaMessagge.SetActive(false);
        new NativeShare().AddFile(filepath).SetSubject("share").SetText(GameObject.Find("Localization").GetComponent<Localization>().SearchWord("SHARE_MESSAGE")).Share();
    }

    public void DontShowAgain(Toggle toggle){
        if (toggle.isOn) {
            PlayerPrefs.SetInt("showShareMessagge",0);
        }else{
            PlayerPrefs.SetInt("showShareMessagge",1);
        }
    }

    public void CloseShareMedia(){
        soundManager.PlaySound("buttonClick");
        shareMediaMessagge.SetActive(false);
    }

#endregion

#region generalUI
    public void HideDoubleTapMessagge(){
        doubleTapMessagge.SetActive(false);
    }


    public void ReturnToMenu(){
        //PlayerPrefs.DeleteKey("showDT");
        //PlayerPrefs.DeleteKey("showSR");
        SceneManager.LoadScene("MainScene");
    }

    public void ShowHideCanvas(){
        canvas.SetActive(!canvas.activeSelf);
    }

#endregion

#region ScreenShot
    public void TakeScreenshot(){
        canvas.SetActive(false);
        StartCoroutine(TakeAndSaveScreenshot());
    }

    IEnumerator TakeAndSaveScreenshot(){

        yield return new WaitForEndOfFrame();
        Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
        //Get Image from screen
        screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenImage.Apply();
        //Convert to png
        byte[] imageBytes = screenImage.EncodeToPNG();
        //Save image to gallery
        string fileName = "NahualcalliARScreenshot_"+System.DateTime.Now.ToString("hh_mm_ss")+".png";
        filepath = Path.GetFullPath(Application.persistentDataPath + GALLERY_PATH + "/" + fileName);

        NativeGallery.SaveImageToGallery(imageBytes, "NahualcalliAR", fileName, null);


        yield return new WaitForEndOfFrame();
        canvas.SetActive(true);
        ScreenRecorder.ShowToast(GameObject.Find("Localization").GetComponent<Localization>().SearchWord("SS_TOAST"));
        ShowShareMediaDialog(GameObject.Find("Localization").GetComponent<Localization>().SearchWord("SHARE_SS"));
        //nativeShare.AddFile(filepath).SetSubject("share").SetText("Screenshot of NahualcalliAR").Share();
    }
#endregion

#region RecordVideo

    public void HideStopRecordMessagge(){
        stopRecordMessagge.SetActive(false);
        canvas.SetActive(false);

        System.DateTime now = System.DateTime.Now;
        string fileName = "NahualcalliARVideo_" + System.DateTime.Now.ToString("yy_hh_mm") + ".mp4";
        filepath = Path.GetFullPath(Application.persistentDataPath + GALLERY_PATH + "/" + fileName);

        recordManager.StartRecord(Application.persistentDataPath + GALLERY_PATH + "/" + fileName);
        isRecording = true;
    }

    public void StartVideo(){
        //canvas.SetActive(true);
        if (PlayerPrefs.HasKey("showSR")){
            canvas.SetActive(false);

            System.DateTime now = System.DateTime.Now;
            string fileName = "Video_" + now.Year + "_" + now.Month + "_" + now.Day + "_" + now.Hour + "_" + now.Minute + "_" + now.Second + ".mp4";
            filepath = Path.GetFullPath(Application.persistentDataPath + GALLERY_PATH + "/" + fileName);
            recordManager.StartRecord(filepath);
            isRecording = true;
         }else{
             PlayerPrefs.SetInt("showSR",0);
             stopRecordMessagge.SetActive(true);
         }

    }

    public void SaveVideo(){
        soundManager.PlaySound("recording");
        canvas.SetActive(true);
        isRecording = false;
        recordManager.StopRecord();
    }
#endregion

#region switch

    public void SwitchMode(){
        if (startVideoBtn.activeSelf) {
            switchBtn.GetComponent<Button>().GetComponent<Image>().sprite = CamToVid;
        }else{
            switchBtn.GetComponent<Button>().GetComponent<Image>().sprite = VidToCam;
        }
        takeScreenshotBtn.SetActive(!takeScreenshotBtn.activeSelf);
        startVideoBtn.SetActive(!startVideoBtn.activeSelf);
    }
    public void SetRecordingUI(){
        returnBtn.SetActive(false);
        startVideoBtn.SetActive(false);
        stopBtn.SetActive(true);
        switchBtn.SetActive(false);
    }
    public void SetIdleUI(){
        returnBtn.SetActive(true);
        startVideoBtn.SetActive(true);
        stopBtn.SetActive(false);
        switchBtn.SetActive(true);
    }
#endregion




}
