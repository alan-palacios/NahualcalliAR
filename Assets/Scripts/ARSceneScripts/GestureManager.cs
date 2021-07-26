using System.Collections;
using System.Collections.Generic;
 using UnityEngine.EventSystems;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
    int TapCount;
    public float MaxDubbleTapTime;
    float NewTime;
    public UIManager uiManager;

    void Start () {
        TapCount = 0;
    }

    void Update () {
        if (Input.touchCount == 1 && !IsPointerOverUIObject()) {
            Touch touch = Input.GetTouch (0);

            if (touch.phase == TouchPhase.Ended) {
                TapCount += 1;
            }

            if (TapCount == 1) {

                NewTime = Time.time + MaxDubbleTapTime;
            }else if(TapCount == 2 && Time.time <= NewTime){

                //Whatever you want after a dubble tap

                if (uiManager.isRecording) {
                    uiManager.SaveVideo();
                }else{
                    uiManager.ShowHideCanvas();
                }
                TapCount = 0;
            }

        }
        if (Time.time > NewTime) {
            TapCount = 0;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();

                return;
            }
        }
    }

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
