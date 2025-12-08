using UnityEngine;

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public int count = 5;
    public float delayBetween = 0.5f;
}

[System.Serializable]
public class Wave
{
    public EnemySpawnInfo[] enemies;
    public float timeBeforeNextWave = 10f;
}