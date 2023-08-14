using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs = new List<GameObject>();
    [SerializeField] List<EnemyData> enemyDataList = new List<EnemyData>();
    [SerializeField] List<Transform> enemySpawnPoints = new List<Transform>();
    [SerializeField] List<Transform> patrolPoints = new List<Transform>();
    [SerializeField] int enemyHeadCount;
    [SerializeField] float spawnDelay;
    [SerializeField] float levelUpTime;

    public bool isSpawn{ get; set; }
    public int activeEnemy { get; private set; }
    public int enemyCount { get { return enemyHeadCount; } }

    int enemyLevel = 0;

    List<GameObject> enemysPool = new List<GameObject>();


    private void Start()
    {
        isSpawn = true;

        FillPool();
        StartCoroutine(Spawn());
        StartCoroutine(LevelUp());
    }

    public List<GameObject> GetEnemysPool()
    {
        return enemysPool;
    }

    private void FillPool()
    {
        for (int i = 0; i < enemyHeadCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Count)], transform.position, transform.rotation);
            enemy.GetComponent<EnemyBehavior>().patrolPoints = patrolPoints;
            enemy.SetActive(false);
            enemysPool.Add(enemy);
        }
    }

    private void GetFromPool(Transform spawnPoint)
    {
        foreach (var enemy in enemysPool)
        {
            if (!enemy.activeSelf)
            {
                enemy.transform.position = spawnPoint.position;
                enemy.transform.rotation = spawnPoint.rotation;
                enemy.SetActive(true);
                EnemyBehavior enemyBehavior = enemy.GetComponent<EnemyBehavior>();
                enemyBehavior.SetEnemyData(enemyDataList[enemyLevel]);
                StartCoroutine(enemyBehavior.Move());
                break;
            }
        }
    }

    private int ActiveEnemyCounter()
    {
        int count = 0;
        foreach (var enemy in enemysPool)
        {
            if(enemy.activeSelf)count++;
        }

        return count;
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            if (isSpawn)
            {
                
                yield return new WaitForSeconds(spawnDelay);
                GetFromPool(enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)]);
                activeEnemy = ActiveEnemyCounter();
                EventManager.SendEvent();
            }
            yield return null;
        }
    }

    IEnumerator LevelUp()
    {
        for(int i = 0; i < enemyDataList.Count; i++)
        {
            yield return new WaitForSeconds(levelUpTime);
            enemyLevel = i;
        }
    }
}
