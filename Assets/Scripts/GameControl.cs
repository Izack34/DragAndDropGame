using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameControl: MonoBehaviour
{
    public static GameControl instance;

    private int NumberOfPicks = 3;

    public int[] TableofNumbers = new int[3];

    public delegate void OnStartPicking();
    public OnStartPicking onStartPicking;

    public delegate void OnPrepere();
    public OnPrepere onPrepere;

    public delegate void OnEndTime();
    public OnEndTime onEndTime;

    public delegate void OnNextPicking();
    public OnNextPicking onNextPicking;

    [SerializeField]
    private GameObject PausePanel;

    [SerializeField]
    Droplet Dropletcontrol;

    [SerializeField]
    private TMP_Text ButtonText;

    [SerializeField]
    private TMP_Text EndroundText;

    [SerializeField]
    GameObject[] Slots;

    private PausePanelScript PauseScript;


    void Awake(){
        if(instance != null){
            Debug.LogError("Error Controler");
        }else{
            instance = this;
        }
    }
    private void Start() {
        GameControl.instance.onEndTime += retry;

        PauseScript = gameObject.GetComponent<PausePanelScript>();
    }

    public GameObject returnSlot(int number){
        //Debug.Log(TableofNumbers[0]);
        return Slots[TableofNumbers[number]];
    }

    public int returnNumberofDrops(){
        return NumberOfPicks;
    }

    public void StartGame(){
        PickNumbers();
        PauseScript.HidePauseMenu();
    }

    private void PickNumbers(){
        TableofNumbers = new int[NumberOfPicks];
        TableofNumbers[0] = Random.Range(0, 9);

        for ( int i = 1 ; NumberOfPicks > i; i++){
            
            TableofNumbers[i] = Random.Range(0, 9);
            while (TableofNumbers[i-1] == TableofNumbers[i])
            {
                TableofNumbers[i] = Random.Range(0, 9);
            }
            //Debug.Log(TableofNumbers[i]);
        }

        onPrepere();
        StartCoroutine(showQue(TableofNumbers));
    }

    private IEnumerator showQue(int[] _TableofNumbers){
        yield return new WaitForSeconds(0.5f);
        
        for ( int i = 0 ; NumberOfPicks > i; i++){

            Dropletcontrol.MoveDropletTo(Slots[_TableofNumbers[i]].GetComponent<RectTransform>().anchoredPosition.x ,
                Slots[_TableofNumbers[i]].GetComponent<RectTransform>().anchoredPosition.y);
            yield return new WaitForSeconds(0.5f);

            Slots[_TableofNumbers[i]].GetComponent<Dropslot>().ShowNumber(i);
            yield return new WaitForSeconds(1.0f);
        }

        Dropletcontrol.MoveDropletTo(0,0);
        yield return new WaitForSeconds(0.5f);
        onStartPicking();
    }

    public void incremantSetps(){
        NumberOfPicks += 1;
        EndroundText.text = "Good";
        ButtonText.text = "Next";

        PausePanel.SetActive(true);
        PauseScript.AnimationStart();
    }

    public void retry(){
        NumberOfPicks = 3;
        EndroundText.text = "Time out";
        ButtonText.text = "Retry";

        PausePanel.SetActive(true);
        PauseScript.AnimationStart();
    }
}
