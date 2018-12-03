using UnityEngine;

[CreateAssetMenu]
public class RangedInt : ScriptableObject
{
    public RangedIntData rangedInt;

    private int value;

    public int GetRandomValue()
    {
        return Random.Range(rangedInt.minValue, rangedInt.maxValue + 1);
    }
}
