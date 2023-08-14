using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] UIManager uIManager;
    [SerializeField] SpawnSystem spawnSystem;
    [SerializeField] SaveLoadManager saveLoadManager;
    [SerializeField] PlayerManager playerManager;

    bool isPause = false;

    private void Awake()
    {
        EventManager.eventsGame.AddListener(GameOver);
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void Pause()
    {
        float time = isPause ? 1 : 0;
        isPause = !isPause;
        uIManager.Pause(isPause);
        Time.timeScale = time;
    }

    private void GameOver()
    {
        if(spawnSystem.enemyCount == spawnSystem.activeEnemy)
        {
            Time.timeScale = 0;
            uIManager.GameOver();
        }
    }

    public void ExitGame()
    {
        
        saveLoadManager.score = playerManager.points;
        saveLoadManager.SaveProgress();
        uIManager.ExitGame();
    }
}
