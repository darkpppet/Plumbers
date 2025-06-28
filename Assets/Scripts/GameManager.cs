using Monster;
using Stage;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int _stageNum;
    public StageBase Stage { get; private set; }
    public MonsterBase Monster { get; private set; }
    
    public GameObject stage1;
    public GameObject monster1;

    private TextMeshProUGUI _creditText;

    private int _credit = 0;
    public int Credit
    {
        get => Instance._credit;
        set
        {
            if (value != Instance._credit)
            {
                Instance._credit = value;
                Instance._creditText.text = $"CREDIT: {Instance._credit}";
            }
        }
    }

    private Button _resetButton;
    private Button _startButton;

    public bool IsPlaying { get; private set; }

    private Transform _winUI;
    private Transform _loseUI;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void OpenStage(int num)
    {
        Instance._stageNum = num;
        SceneManager.sceneLoaded += OnGamePlaySceneLoaded;
        SceneManager.LoadScene("GamePlay");
    }

    private void OnGamePlaySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnGamePlaySceneLoaded;
        Instance._creditText = GameObject.Find("CreditText").GetComponent<TextMeshProUGUI>();
        Instance._resetButton = GameObject.Find("ResetButton").GetComponent<Button>();
        Instance._startButton = GameObject.Find("StartButton").GetComponent<Button>();
        Instance._winUI = GameObject.Find("WinCanvas").transform;
        Instance._loseUI = GameObject.Find("LoseCanvas").transform;
        
        Instance._winUI.gameObject.SetActive(false);
        Instance._loseUI.gameObject.SetActive(false);
        
        switch (Instance._stageNum)
        {
            case 1:
                Instance.Stage = Instantiate(stage1).GetComponent<StageBase>();
                Instance.Monster = Instantiate(monster1, new Vector3(0, 0, -1), Quaternion.identity).GetComponent<MonsterBase>();
                Instance.Monster.gameObject.SetActive(false);
                break;
        }
    }

    public void StartStage()
    {
        if (Instance.Credit >= 0 && Instance.Stage.FindPath())
        {
            Instance._resetButton.enabled = false;
            Instance._startButton.enabled = false;
            Instance.IsPlaying = true;
            Instance.Monster.Activate();
        }
    }

    public void ResetStage()
    {
        Instance.Stage.ResetStage();
    }

    public void WinStage()
    {
        Instance._winUI.gameObject.SetActive(true);
    }

    public void LoseStage()
    {
        Instance._loseUI.gameObject.SetActive(true);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("StartScene");
    }
    
}
