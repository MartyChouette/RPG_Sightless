[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public int strength; // 0–10
    public ElementType weakness;

    // Multiply damage taken if weak to an element
    public float GetDamageMultiplier(ElementType attackElement)
    {
        return (attackElement == weakness) ? 1.5f : 1f;
    }
}