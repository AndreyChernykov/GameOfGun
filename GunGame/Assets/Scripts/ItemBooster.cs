using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBooster : MonoBehaviour
{
    [SerializeField] TypeBooster typeBooster;
    [SerializeField] GameObject mesh;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float freezeSpawnTime;

    SpawnSystem spawnSystem;

    enum TypeBooster
    {
        exterminator,
        freezeSpawn,
    }

    float destroyDelay = 1.5f;

    private void Start()
    {
        spawnSystem = FindObjectOfType<SpawnSystem>();
    }

    public void ToDestroy()
    {
        SwitchEffect();
        mesh.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        audioSource.Play();
    }


    private void SwitchEffect()
    {
        switch (typeBooster)
        {
            case TypeBooster.exterminator:
                StartCoroutine(Exterminate());
                break;
            case TypeBooster.freezeSpawn:
                StartCoroutine(FreezeSpawn());
                break;
        }
    }

    IEnumerator FreezeSpawn()
    {
        spawnSystem.isSpawn = false;
        yield return new WaitForSeconds(freezeSpawnTime);
        spawnSystem.isSpawn = true;
        Destroy(gameObject);
    }

    IEnumerator Exterminate()
    {
        foreach(GameObject enemy in spawnSystem.GetEnemysPool())
        {
            enemy.SetActive(false);
        }
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
