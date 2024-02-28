using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityStandardAssets.Cameras;

public class GameManager : MonoBehaviour
{
    public MarbleController playerController;
    public FreeLookCam freeLookCam;
    public float m_Delay;
    public int m_CoinCount = 0;
    public TMP_Text m_CoinCounterText;
    public TMP_Text m_TimeCounterText;
    public static GameManager Instance;

    public GameObject GameOverPanel;
    public GameObject GameCompletedPanel;
    public GameObject FailedObjectivePanel;
    int m_SceneIndex;
    public bool gameHasEnded;
    public bool gameHasStarted;
    public int CoinCountObjective;
    public float TimeCountObjective;

    public AudioSource GameCompletedAudio;
    public AudioSource FailedObjectiveAudio;

    private void Awake()
    {
        Instance = this;

        if(GameOverPanel != null) GameOverPanel.SetActive(false);
        if(GameCompletedPanel != null) GameCompletedPanel.SetActive(false);
        if(FailedObjectivePanel != null) FailedObjectivePanel.SetActive(false);

        m_SceneIndex = SceneManager.GetActiveScene().buildIndex;

        gameHasEnded = false;
        gameHasStarted = false;

        freeLookCam.enabled = false;
        freeLookCam.m_LockCursor = false;
        playerController.enabled = false;
    }

    void Start()
    {

        UpdateCoinDisplayText();
    }

    void Update()
    {
        
        if(gameHasStarted & TimeCountObjective != 9999f){ // using 9999 as a flag to disable this tiem feature in earlier levels
            
            TimeCountObjective -= Time.deltaTime;

            // Ensure the timer doesn't go below 0
            TimeCountObjective = Mathf.Max(0f, TimeCountObjective);

            int minutes = Mathf.FloorToInt(TimeCountObjective / 60f);   // 120 / 60 = 2
            int seconds = Mathf.FloorToInt(TimeCountObjective % 60f); // 120 mod 60 = 0 // 119 mod 60 = 59

            m_TimeCounterText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if(TimeCountObjective <= 0  && !gameHasEnded){

                gameHasEnded = true;
                CheckIfGameIsCompleted();
            }
        }
        
    }

    public void StartGame(){

        gameHasStarted = true;
        freeLookCam.enabled = true;
        freeLookCam.m_LockCursor = true;
        playerController.enabled = true;
    }

    public void AddPoint(){

        m_CoinCount++;
        UpdateCoinDisplayText();
    }

    public void UpdateCoinDisplayText(){

        m_CoinCounterText.text = m_CoinCount.ToString();
    }

    public void CheckIfGameIsCompleted(){

        CheckIfPlayerMetAllObjectives();
        playerController.rigidbody.velocity = Vector3.zero;
        playerController.rigidbody.angularVelocity = Vector3.zero;
        playerController.enabled = false;
        freeLookCam.enabled = false;
    }

    private void CheckIfPlayerMetAllObjectives(){

        if(gameHasEnded){

            if(m_CoinCount == CoinCountObjective){

                GameCompletedAudio.Play();
                
                if(GameCompletedPanel != null){

                    GameCompletedPanel.SetActive(true);
                
                } 
            }

            else{

                FailedObjectiveAudio.Play();

                if(FailedObjectivePanel != null){

                    FailedObjectivePanel.SetActive(true);
                } 
            }
        }
        
    }

    public void GameOverRestart(){

        if(GameOverPanel != null) GameOverPanel.SetActive(true);
        Invoke(nameof(RestartLevel), m_Delay);
    }
    
    public void NextLevel()
    {  
        int nextScene = m_SceneIndex + 1;

        if (nextScene < SceneManager.sceneCountInBuildSettings) //check if next scene index exists
        {
            SceneManager.LoadScene(nextScene);
        }
        else
        {
            SceneManager.LoadScene(0); // Back to the menu
        }
    }

    public void RestartLevel(){

        SceneManager.LoadScene(m_SceneIndex);
    }


}

