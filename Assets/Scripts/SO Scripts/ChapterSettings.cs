using UnityEngine;

[System.Serializable]
public struct EnemyWaves
{
    public Enemy Enemy;
    public float[] NumberPerSeconds;
}

[CreateAssetMenu]
public class ChapterSettings : ScriptableObject
{
    public EnemyWaves[] EnemyWavesArray;
}