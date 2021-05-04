using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelScript : MonoBehaviour
{
    public GameObject Panel;
    public Transform Menu;
    public Transform Text;
    public CanvasGroup background;

    public void AnimationStart() {

        background.alpha = 0;
        background.LeanAlpha(1, 0.5f);

        Menu.localPosition = new Vector2(0, -Screen.height);
        Menu.LeanMoveLocalY(-40, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void HidePauseMenu() {

        background.LeanAlpha(0, 0.5f);
        Menu.LeanMoveLocalY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(OnComplete);
    }

    void OnComplete(){
        Panel.SetActive(false);
    }

}
