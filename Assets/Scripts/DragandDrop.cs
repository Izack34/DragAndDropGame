using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour ,IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    public Droplet Controler;
    public GameObject Slotfrom;
    public GameObject Slot;

    private void Awake() {
       rectTransform = GetComponent<RectTransform>();
       canvasGroup = GetComponent<CanvasGroup>();
       Controler = GetComponent<Droplet>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        if(Controler.isStarted){
            eventData.pointerDrag = null;
            return;
        }
        canvasGroup.blocksRaycasts = false;
        Slotfrom = Slot;
    }

    public void OnDrag(PointerEventData eventData){

        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData){
        //Debug.Log("onEND");
        canvasGroup.blocksRaycasts = true;
        if(eventData.pointerCurrentRaycast.gameObject.tag != "Slot"){
            rectTransform.anchoredPosition = Slot.GetComponent<RectTransform>().anchoredPosition;
        }else{
            if(Controler.next == Slot){
                Controler.Step(eventData.pointerCurrentRaycast.gameObject);
            }else{
                Controler.StepWrong(eventData.pointerCurrentRaycast.gameObject);
            }

        }

    }
}
