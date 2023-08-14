using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] PlayerManager playerManager;
    [SerializeField] TextMeshProUGUI textPoint;
    [SerializeField] TextMeshProUGUI textPlayerLevel;
    [SerializeField] GameObject panelPause;
    [SerializeField] GameObject panelGameOver;

    string pointsText = "Points ";
    string levelPlayerText = "Player level ";
    string sceneGameName = "MainMenu";

    private AsyncOperation asyncOperation;

    private void Awake()
    {
        EventManager.eventsGame.AddListener(DisplayPoints);
        EventManager.eventsGame.AddListener(DisplayLevelPlayer);
    }
    public void DisplayPoints()
    {
        textPoint.text = pointsText + playerManager.points;
    }

    public void DisplayLevelPlayer()
    {
        textPlayerLevel.text = levelPlayerText + playerManager.playerLevel;
    }

    public void Pause(bool p)
    {
        panelPause.SetActive(p);
    }

    public void ExitGame()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneGameName);
    }

    public void GameOver()
    {       
        panelGameOver.SetActive(true);
    }
}
