using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] EnemyData enemyData;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] List<GameObject> enemyMeshes = new List<GameObject>();
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip damageAudio;

    float stoppingDist = 0.3f;
    float delayDestroy = 2f;
    float moveSpeed;
    
    int level;
    int health;
    int points;
    int dropProbability;

    List<GameObject> items;

    GameObject item;

    public List<Transform> patrolPoints { get; set; }

    private void Start()
    {
        SetData();
        SetMesh();
    }

    private void OnEnable()
    {
        SetData();
        SetMesh();
    }

    private void SetData()
    {
        level = enemyData.level;
        moveSpeed = enemyData.moveSpeed;
        health = enemyData.health;
        points = enemyData.points;
        dropProbability = enemyData.dropProbability;

        if (enemyData.items.Count > 0) items = enemyData.items;
        if (items.Count > 0)
        {
            int prop = Random.Range(0, 100);
            if (prop < dropProbability)
            {
                item = Instantiate(items[Random.Range(0, items.Count)], transform.position, Quaternion.identity);
                item.SetActive(false);
            }
        }
    }

    private void SetMesh()
    {
        foreach (GameObject mesh in enemyMeshes) mesh.SetActive(false);
        if(level > enemyMeshes.Count) level = enemyMeshes.Count;
        enemyMeshes[level-1].SetActive(true);
    }

    public void Damage(int damage)
    {
        if(health > 0)
        {
            health -= damage;
            
        }
        if(health <= 0)
        {
            health = 0;
            EnemyDestroy();
        }

        audioSource.PlayOneShot(damageAudio);
    }

    public IEnumerator Move()
    {
        int p = 0;
        Vector3 targetPos;
        navMeshAgent.speed = moveSpeed;

        while (true)
        {

            targetPos = patrolPoints[p].position;
            if(gameObject.activeSelf)navMeshAgent.destination = targetPos;


            if (Vector3.Distance(transform.position, targetPos) < stoppingDist)
            {

                if (p < patrolPoints.Count - 1) p++;
                else
                {
                    p = 0;
                }
            }

            yield return null;
        }

    }

    public void SetEnemyData(EnemyData ed)
    {
        enemyData = ed;
    }

    private void EnemyDestroy()
    {
        item.transform.position = transform.position;
        item.SetActive(true);
            
        EventManager.SendEvent(points);
        EventManager.SendEvent();
        StartCoroutine(Deactivate());
    }

    IEnumerator Deactivate()
    {
        foreach(GameObject mesh in enemyMeshes) mesh.SetActive(false);
        yield return new WaitForSeconds(delayDestroy);
        gameObject.SetActive(false);
    }
}
