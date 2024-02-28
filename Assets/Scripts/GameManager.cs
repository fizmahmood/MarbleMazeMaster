using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public MarbleController PlayerController;
    public float m_Delay;
    public int m_CoinCount;
    public TMP_Text m_CoinCounterText;
    public static GameManager Instance;

    public GameObject GameOverPanel;
    public GameObject GameCompletedPanel;
    int m_SceneIndex;
    public bool gameHasEnded;

    private void Awake()
    {
        Instance = this;

        if(GameOverPanel != null) GameOverPanel.SetActive(false);
        if(GameCompletedPanel != null) GameCompletedPanel.SetActive(false);

        m_SceneIndex = SceneManager.GetActiveScene().buildIndex;

        gameHasEnded = false;
    }

    void Start()
    {

        UpdateCoinDisplayText();
    }

    public void AddPoint(){

        m_CoinCount++;
        UpdateCoinDisplayText();
    }

    public void UpdateCoinDisplayText(){

        m_CoinCounterText.text = m_CoinCount.ToString();
    }

    public void GameCompleted(){

        Invoke("OnGameComplete", m_Delay);
    }

    public void GameOverRestart(){

        if(GameOverPanel != null) GameOverPanel.SetActive(true);
        Invoke(nameof(RestartLevel), m_Delay);
    }
    

    private void RestartLevel(){

        SceneManager.LoadScene(m_SceneIndex);
    }

    private void OnGameComplete(){
        
        if(GameCompletedPanel != null) GameCompletedPanel.SetActive(true);
        PlayerController.rigidbody.velocity = Vector3.zero;
        PlayerController.rigidbody.angularVelocity = Vector3.zero;
        PlayerController.enabled = false;
    }

}

