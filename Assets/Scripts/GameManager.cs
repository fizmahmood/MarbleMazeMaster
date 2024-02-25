using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float m_GameOverDelay;
    public int m_CoinCount;
    public TMP_Text m_CoinCounterText;
    public static GameManager Instance;

    public GameObject GameOverPanel;

    private void Awake()
    {
        Instance = this;

        GameOverPanel.SetActive(false);
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

    public void EndGame(){

        GameOverPanel.SetActive(true);
        Invoke(nameof(RestartLevel), m_GameOverDelay);
    }

    private void RestartLevel(){

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

