using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private List<BattleUnit> players = new List<BattleUnit>();
    private List<BattleUnit> enemies = new List<BattleUnit>();
    private Queue<BattleUnit> turnQueue = new Queue<BattleUnit>();

    public GameObject battleUI;
    public TMP_Text battleLog;
    private BattleUnit currentPlayerTurn;


    public void SetupBattle(List<CharacterData> playerData, List<EnemyData> availableEnemies)
    {
        players = playerData.Select(p => new BattleUnit(p)).ToList();

        int enemyCount = Random.Range(1, 4);
        var selectedEnemies = availableEnemies.OrderBy(e => Random.value).Take(enemyCount).ToList();
        enemies = selectedEnemies.Select(e => new BattleUnit(e)).ToList();

        StartBattle();
    }

    void StartBattle()
    {
        List<BattleUnit> allUnits = new List<BattleUnit>();
        allUnits.AddRange(players);
        allUnits.AddRange(enemies);
        allUnits = allUnits.OrderBy(u => u.strength).ToList();

        turnQueue = new Queue<BattleUnit>(allUnits);

        StartCoroutine(HandleTurnLoop());
    }

    IEnumerator HandleTurnLoop()
    {
        while (players.Any(p => p.IsAlive()) && enemies.Any(e => e.IsAlive()))
        {
            var current = turnQueue.Dequeue();

            if (!current.IsAlive())
            {
                // Skip dead units
                continue;
            }

            if (current.isPlayer)
            {
                yield return StartCoroutine(HandlePlayerTurn(current));
            }
            else
            {
                yield return StartCoroutine(HandleEnemyTurn(current));
            }

            turnQueue.Enqueue(current);
            yield return new WaitForSeconds(0.5f);
        }

        if (players.Any(p => p.IsAlive()))
            GameManager.Instance.OnBattleEnd(true);
        else
            GameManager.Instance.OnBattleEnd(false);
    }

    IEnumerator HandlePlayerTurn(BattleUnit player)
    {
        currentPlayerTurn = player;
        battleUI.SetActive(true);
        battleLog.text = $"{player.name}'s turn. Choose an action.";

        // Wait until player picks action
        while (currentPlayerTurn != null)
            yield return null;

        yield return new WaitForSeconds(1f);
    }

    IEnumerator HandleEnemyTurn(BattleUnit enemy)
    {
        BattleUnit target = players.Where(p => p.IsAlive()).OrderBy(x => Random.value).FirstOrDefault();
        if (target != null)
        {
            float dmg = target.TakeDamage(2, ElementType.None);
            Debug.Log($"{enemy.name} attacks {target.name} for {dmg} damage!");
        }

        yield return new WaitForSeconds(1f);
    }

    public void PlayerChooseAttack()
    {
        var target = enemies.FirstOrDefault(e => e.IsAlive());
        if (target != null)
        {
            float dmg = target.TakeDamage(2, currentPlayerTurn.element);
            battleLog.text = $"{currentPlayerTurn.name} attacked {target.name} for {dmg}!";
        }

        EndPlayerTurn();
    }

    public void PlayerChooseDefend()
    {
        currentPlayerTurn.isDefending = true;
        battleLog.text = $"{currentPlayerTurn.name} is defending!";
        EndPlayerTurn();
    }

    void EndPlayerTurn()
    {
        currentPlayerTurn = null;
        battleUI.SetActive(false);
    }

}
