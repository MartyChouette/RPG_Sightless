using UnityEngine;

public class BattleUnit
{
    public string name;
    public int strength;
    public ElementType element;
    public int maxHP = 10;
    public int currentHP;
    public bool isPlayer;
    public bool isDefending = false;
    public ElementType weakness;

    public BattleUnit(CharacterData c)
    {
        name = c.characterName;
        strength = c.strength;
        element = c.element;
        maxHP = currentHP = 10;
        isPlayer = true;
    }

    public BattleUnit(EnemyData e)
    {
        name = e.enemyName;
        strength = e.strength;
        weakness = e.weakness;
        maxHP = currentHP = 10;
        isPlayer = false;
    }

    public float TakeDamage(int baseDamage, ElementType attackerElement)
    {
        float multiplier = (attackerElement == weakness) ? 1.5f : 1f;
        if (isDefending) multiplier *= 0.5f;

        int damage = Mathf.RoundToInt(baseDamage * multiplier);
        currentHP = Mathf.Max(0, currentHP - damage);
        isDefending = false;

        return damage;
    }

    public bool IsAlive() => currentHP > 0;
}