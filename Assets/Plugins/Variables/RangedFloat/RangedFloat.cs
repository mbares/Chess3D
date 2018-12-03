using UnityEngine;

[CreateAssetMenu]
public class RangedFloat : ScriptableObject
{
    public RangedFloatData rangedFloat;

    private float value;

    public float GetRandomValue()
    {
        return Random.Range(rangedFloat.minValue, rangedFloat.maxValue);
    }
}
