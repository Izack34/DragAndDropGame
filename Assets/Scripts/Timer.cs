using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer: MonoBehaviour
{
    [SerializeField]
    public Image leftimage;
    [SerializeField]
    public Image rightimage;

    void Start()
    {
        GameControl.instance.onStartPicking += StarCount;
        GameControl.instance.onNextPicking  += StopCount;
        GameControl.instance.onPrepere += ReFillTimer;
    }

    public void ReFillTimer(){
        leftimage.color = new Color(0.9f, 0.5f, 0.1f, 1f) ;
        rightimage.color = new Color(0.9f, 0.5f, 0.1f, 1f);
        StartCoroutine("reStartAnimation",1.0f);
    }

    public void StarCount(){
        StartCoroutine("CountDownAnimation",10.0f);
    }

    public void StopCount(){
        StopCoroutine("CountDownAnimation");
    }

    IEnumerator reStartAnimation(float time){

        float StartfillAmount = leftimage.fillAmount;
        float animationTime = 0;
        float SubFromStartFill = 1 - StartfillAmount;

        while (animationTime < time) {
            animationTime += Time.deltaTime;

            leftimage.fillAmount = StartfillAmount + SubFromStartFill*(animationTime/time);
            rightimage.fillAmount = StartfillAmount + SubFromStartFill*(animationTime/time);

            yield return null;
        }
        
    }

    IEnumerator CountDownAnimation(float time){
        float animationTime = time;
        while (animationTime > 0) {
            animationTime -= Time.deltaTime;
            leftimage.fillAmount = animationTime/time;
            rightimage.fillAmount = animationTime/time;
            leftimage.color = Color.Lerp(Color.red, new Color(0.9f, 0.5f, 0.1f, 1f) , (animationTime/time)+0.2f);
            rightimage.color = Color.Lerp( Color.red, new Color(0.9f, 0.5f, 0.1f, 1f), (animationTime/time)+0.2f);
            yield return null;
        }
        GameControl.instance.onEndTime.Invoke();
    }

}
