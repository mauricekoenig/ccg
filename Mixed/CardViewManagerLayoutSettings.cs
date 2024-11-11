



using UnityEngine;

[CreateAssetMenu]

public class CardViewManagerLayoutSettings : ScriptableObject {

    [Header("Hand Properties")]
    public float handWidth;
    public float cardOverlap;
    public float depth;
    public float handOffset;

    [Header("Board Properties")]
    public float boardWidth;
    public float boardOverlap;

    [Header("CardView Properties")]
    public int CardViewPoolSize;
}