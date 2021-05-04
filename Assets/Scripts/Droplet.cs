using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Droplet: MonoBehaviour
{
    [SerializeField]
    private RectTransform StartrectTransform;

    private RectTransform rectTransform;
    public GameObject StartSlot;
    private int NumberofSteps;
    public GameObject next;

    private DragandDrop ScriptDragandDrop;
    public int stepNumber = 0;

    public bool isStarted = true;
    
    private void Awake() {
       ScriptDragandDrop = GetComponent<DragandDrop>();
       rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        GameControl.instance.onStartPicking += StartDragAndDroping;
        GameControl.instance.onEndTime += StopDragandDrop;
    }

    public void MoveDropletTo(float x, float y){
        //Debug.Log(x+" "+y);
        rectTransform.LeanMoveLocalY(y, 0.5f).setEaseOutExpo().delay = 0.1f;
        rectTransform.LeanMoveLocalX(x, 0.5f).setEaseOutExpo().delay = 0.1f;
        //rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition,StartrectTransform.anchoredPosition, 1f);
        ScriptDragandDrop.Slot = StartSlot;
    }


    public void StartDragAndDroping(){
        isStarted = false;
        stepNumber = 0;
        next = GameControl.instance.returnSlot(stepNumber);
        stepNumber +=1;
        NumberofSteps = GameControl.instance.returnNumberofDrops();
    }

    public void Step(GameObject GoodSlot){
        //Debug.Log("good slot");
        GoodSlot.GetComponent<Dropslot>().GoodAnimation();

        if(stepNumber >= NumberofSteps){
            GameControl.instance.onNextPicking.Invoke();
            //Debug.Log("Ez win");
            GameControl.instance.incremantSetps();
            isStarted = true;
            return;
        }

        next = GameControl.instance.returnSlot(stepNumber);
        stepNumber +=1;
    }

    public void StopDragandDrop(){
        isStarted = true;
    }

    public void StepWrong(GameObject wrongSlot){
        wrongSlot.GetComponent<Dropslot>().WrongAnimation();
    }


}
