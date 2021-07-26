using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recorder
{
    [RequireComponent(typeof(ScreenRecorder))]
    public class RecordManager : MonoBehaviour
    {
        ScreenRecorder recorder;
        public UIManager uiManager;

		private void Start()
		{
            recorder = GetComponent<ScreenRecorder>();
		}

        public void StartRecord(string filepath)
        {
            recorder.PrepareRecorder(filepath);
            StartCoroutine(DelayCallRecord());
        }
        private IEnumerator DelayCallRecord()
        {
            yield return new WaitForSeconds(1f);
            uiManager.canvas.SetActive(false);
            //uiManager.SetRecordingUI();
            recorder.StartRecording();
        }


        public void StopRecord()
        {
            recorder.StopRecording();
            //uiManager.SetIdleUI();
            StartCoroutine(DelaySaveVideo());
        }
        private IEnumerator DelaySaveVideo()
        {
            yield return new WaitForSeconds(0.3f);
            recorder.SaveVideoToGallery();
            yield return new WaitForSeconds(0.4f);
            uiManager.ShowShareMediaDialog(GameObject.Find("Localization").GetComponent<Localization>().SearchWord("SHARE_VIDEO"));
            //new NativeShare().AddFile(filepath).SetSubject("share").SetText("Screenshot of NahualcalliAR").Share();
            /*ScreenRecorder.ShareAndroid("I challenge you to beat my high score in Fire Block", "I challenge you to beat my high score in Fire Block. " +
    		      ". Get the Fire Block app from the link below. \nCheers\n" +
    		            "\nhttp://onelink.to/fireblock", null, filepath, "video/mp4", true, "share image"		);*/
        }

    }
}
