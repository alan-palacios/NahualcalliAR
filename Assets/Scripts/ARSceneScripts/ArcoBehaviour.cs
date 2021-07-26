using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcoBehaviour : MonoBehaviour
{
    Animation anim;
    public AnimationClip animClipStart;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    void OnEnable(){
        anim = GetComponent<Animation>();
        anim.Play("Scene");
        StartCoroutine(PlayLoopAnimation());
        StartCoroutine(SetVisibleText());
    }

    IEnumerator PlayLoopAnimation(){
        yield return new WaitForSeconds(animClipStart.length);
        anim.Play("Loop");
    }
    IEnumerator SetVisibleText(){
        text.SetActive(false);
        yield return new WaitForSeconds(animClipStart.length-2f);
        text.SetActive(true);
    }
}
