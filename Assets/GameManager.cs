using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<CharacterData> playerParty;
    public List<EnemyData> enemyPool;

    public TownManager townManager;
    public BattleManager battleManager;
    public TMP_Text locationDisplay;


    private int currentTownIndex = 0;
    private int battlesRemaining = 0;


    public List<EnemyData> GetMatchingEnemies()
    {
        int avgStrength = playerParty.Count > 0 ?
            Mathf.RoundToInt((float)playerParty.Average(c => c.strength)) : 0;

        return enemyPool
            .Where(e => Mathf.Abs(e.strength - avgStrength) <= 2)
            .ToList();

    }


    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        SetLocationText("In " + townManager.GetTownName(currentTownIndex));
        townManager.ShowTownUI(currentTownIndex);
    }

    public void EnterWilds()
    {
        SetLocationText("In the Wilds");
        battlesRemaining = Random.Range(1, 4);
        StartNextBattle();
    }

    public void SetLocationText(string text)
    {
        if (locationDisplay != null)
            locationDisplay.text = text;
    }


    public void RestParty()
    {
        // Implement: restore HP, etc.
    }

    public void ShowDialogue(string text)
    {
        // Plug into a text box later
        Debug.Log(text);
    }



    public void StartNextBattle()
    {
        if (battlesRemaining > 0)
        {
            battlesRemaining--;

            var enemies = GetMatchingEnemies();
            battleManager.SetupBattle(playerParty, enemies);
        }
        else
        {
            GoToNextTown();
        }
    }


    public void OnBattleEnd(bool playerWon)
    {
        if (playerWon)
        {
            StartNextBattle();
        }
        else
        {
            // Reset to previous town
            townManager.ShowTownUI(currentTownIndex);
        }
    }

    public void GoToNextTown()
    {
        currentTownIndex++;
        if (currentTownIndex >= 7)
        {
            Debug.Log("Game Complete!");
            // Optionally trigger end screen
        }
        else
        {
            townManager.ShowTownUI(currentTownIndex);
        }
    }




}