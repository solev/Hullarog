using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int MaxBlood = 500;

    public static GameManager Instance;

    public float TimeBetweenWaves = 30f;

    [HideInInspector]
    public int blood,
    enemyCount,
    enemiesToSummon,
    level,
    maxLevelTime = 300,
    initialWave = 20;

    

    public GameObject skeleton;
    public Transform[] spawnPoints;

    [HideInInspector]
    public bool waveActive = false;
    [HideInInspector]
    public bool breakStarted = false;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        blood = 0;
        enemyCount = 0;
        enemiesToSummon = 0;
        level = 0;
    }

    void Update()
    {
        if (!waveActive)
        {
            level++;
            LaunchWave(level);
            waveActive = true;
        }

        if (waveActive && (enemyCount == 0) && !breakStarted)
        {
            breakStarted = true;
            StartCoroutine("LevelOver");
        }
    }

    void LaunchWave(int level)
    {
        if (level <= 5)
        {
            enemiesToSummon += level * initialWave;
        }
        else {
            skeleton.GetComponent<EnemyMovement>().IncreaseStats();
        }

        StartCoroutine(SummonSkeletons(enemiesToSummon));
    }

    public void AddBlood(int bloodPoints)
    {
        blood = Mathf.Clamp(blood + bloodPoints, 0, MaxBlood);
    }

    IEnumerator LevelOver()
    {
        yield return new WaitForSeconds(TimeBetweenWaves);
        waveActive = false;
        breakStarted = false;
    }

    IEnumerator SummonSkeletons(int number)
    {
        for (int i = 0; i < number; i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject newSkeleton = Instantiate(skeleton, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation) as GameObject;
            newSkeleton.SetActive(true);
            enemyCount++;

            yield return new WaitForSeconds(0.2f);
        }
    }
}
