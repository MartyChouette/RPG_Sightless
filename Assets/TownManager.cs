using System;
using TMPro;
using UnityEngine;

//public class TownManager : MonoBehaviour
//{
//    private string[] townNames =
//    {
//        "Karada", "Sofida", "Frazeail", "Tobito", "Prala", "Gornot", "Qaytail", "Lesnod", "Rezaal", "Soput",
//        "Srahal", "Wayhol", "Connel", "Rawkal", "Veranad", "Hadda", "Popata", "Brickle", "Martayle", "Wrostlane"
//    };

//    private string selectedTown;

//    public Canvas townNameCanvas;

//void Start()
//{

//    //name test
//    for (int x = 0; x < 20; x++)
//    {
//        selectedTown = townNames[x];
//            Debug.Log(townNames[x]);
//    }
    
//}

//    // Update is called once per frame
//    void Update()
//    {
//        int townNameIndex = 0;


//        townNameCanvas.GetComponentInChildren<TMP_Text>().SetText(townNames[townNameIndex]) ;
//    }
//}
public class TownManager : MonoBehaviour
{
    public TMP_Text townNameText;
    public GameObject townUI;
    private string[] townNames =
    {
        "Karada", "Sofida", "Frazeail", "Tobito", "Prala", "Gornot", "Qaytail", "Lesnod", "Rezaal", "Soput",
        "Srahal", "Wayhol", "Connel", "Rawkal", "Veranad", "Hadda", "Popata", "Brickle", "Martayle", "Wrostlane"
    };

    private int currentTownIndex = 0;

    public void ShowTownUI(int townIndex)
    {
        currentTownIndex = townIndex;
        townUI.SetActive(true);
        townNameText.text = "Welcome to " + townNames[townIndex];
    }

    public void OnRest()
    {
        AudioManager.Instance.PlayOneShot("event:/UI/Rest");
        GameManager.Instance.RestParty(); // Refill stats
    }

    public void OnTalk()
    {
        AudioManager.Instance.PlayOneShot("event:/UI/Talk");
        GameManager.Instance.ShowDialogue("The world is changing... shadows gather.");
    }

    public void OnJourney()
    {
        AudioManager.Instance.PlayOneShot("event:/UI/Journey");
        townUI.SetActive(false);
        GameManager.Instance.EnterWilds();
    }

    public string GetTownName(int index) => townNames[index];

}
