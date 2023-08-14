using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<GameObject> menuElements = new List<GameObject>();
    [SerializeField] TextMeshProUGUI creditsText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] SaveLoadManager saveLoadManager;

    private AsyncOperation asyncOperation;

    string sceneGameName = "GameScene";
    float executionDelay = 0.3f;

    private void Start()
    {
        Time.timeScale = 1;

        //Invoke("CreditsDisplayed", executionDelay);
        Invoke("ScoreDisplayed", executionDelay);
    }

    public void MenuState(GameObject element)
    {
        foreach (GameObject e in menuElements) e.SetActive(false);
        element.SetActive(true);
    }

    public void DeactivateElementMenu(GameObject element)
    {
        element.SetActive(false);
    }

    public void NewGame()
    {
        asyncOperation = SceneManager.LoadSceneAsync(sceneGameName);
    }



    public void ExitGame()
    {
        Application.Quit();
    }

    private void CreditsDisplayed()
    {
        creditsText.text = saveLoadManager.creditString;
    }

    private void ScoreDisplayed()
    {
        string score = "";
        foreach (var d in saveLoadManager.dataScore.Keys.OrderBy(x => -x))
        {
            score +=  d + "  " + saveLoadManager.dataScore[d] + "\n";
        }
        scoreText.text = score;
    }
}
