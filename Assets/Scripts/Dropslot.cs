using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Dropslot : MonoBehaviour, IDropHandler
{
    public GameObject NumberText;
    private Image Imagecolor;
    
    [SerializeField]
    private TMP_Text textmesh;

    private void Start() {  
        NumberText.SetActive(false);
        Imagecolor = GetComponent<Image>();
    }

    public void ShowNumber(int QueNumber){
        //QueNumber += 1;
        //textmesh.text = QueNumber.ToString();
        StartCoroutine(ShowNumberforSometime());
    }


    public IEnumerator ShowNumberforSometime(){
        //NumberText.SetActive(true);
        StartCoroutine("FadeGoodColor");
        yield return new WaitForSeconds(1.0f);
        //NumberText.SetActive(false);
    }


    public void OnDrop(PointerEventData eventData){

        //Debug.Log("dropped");
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        eventData.pointerDrag.GetComponent<DragandDrop>().Slot = this.gameObject;
        
    }

    public void WrongAnimation(){
        //anim.Play("Base Layer.Bounce", 0, 0.25f);
        //Debug.Log("start wrong animation");
        Imagecolor.color = Color.red;
        StartCoroutine("FadeWrongColor");
        
    }

    IEnumerator FadeWrongColor(){
        float animationTime = 0;
        while (animationTime < 0.5f) {
            animationTime += Time.deltaTime;
            Imagecolor.color = Color.Lerp(Color.red, new Color(0.8f, 0.5f, 0.1f, 1f), animationTime/0.5f);
            yield return null;
        }
        
    }

    public void GoodAnimation(){
        //anim.Play("Base Layer.Bounce", 0, 0.25f);
        //Debug.Log("start wrong animation");
        Imagecolor.color = Color.green;
        StartCoroutine("FadeGoodColor");
        
    }

    IEnumerator FadeGoodColor(){
        float animationTime = 0;
        while (animationTime < 0.5f) {
            animationTime += Time.deltaTime;
            Imagecolor.color = Color.Lerp(Color.green, new Color(0.8f, 0.5f, 0.1f, 1f), animationTime/0.5f);
            yield return null;
        }
        
    }
}
