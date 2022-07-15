using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using TMPro;
using TigerForge;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public GameObject[] Enemy;
    [SerializeField] public GameObject GameMenu;
    [SerializeField] public GameObject GameOverMenu;
    [SerializeField] public TMP_Text coinText;
    [SerializeField] GameObject Tween;
    [HideInInspector] public Gem_Collect gem;
    [HideInInspector] public Camera cam;
    private bool isPause;
    public int coin, level, coin_lv, up;
    // Mode Game
    public enum modes { GameOver, Victory, Ready, Play }
    public modes mode;

    void Awake()
    {
        Instance = this;
        cam = Camera.main;
        mode = modes.Ready;
    }

    void Start()
    {
        //PlayerPrefs.SetInt("Level", 0);
        up = -1;
        gem = GameManager.Instance.GetComponent<Gem_Collect>();
        coin = PlayerPrefs.GetInt("Coin");
        level = PlayerPrefs.GetInt("Level");
        coin_lv = 0;
        Update_Coin();
        Time.timeScale = 1;
        EventManager.StartListening("GamePlay", GamePlay);
    }

    void GamePlay()
    {
        mode = modes.Play;
    }

    public void Play_Sound_At(AudioClip clip, Vector3 vector)
    {
        AudioSource.PlayClipAtPoint(clip, vector, 1.0f);
    }

    public void UpPlus()
    {
        up++;
    }

    // Coin
    public void Add_Coin(int number)
    {
        coin += number;
        coin_lv++;
        PlayerPrefs.SetInt("Coin", coin);
        Update_Coin();
    }

    void Update_Coin()
    {
        coinText.text = coin.ToString();
    }

    public void GameOver()
    {
        GameOverMenu.SetActive(true);
        FindObjectOfType<Movement_Input>().enabled = false;
        var player = FindObjectOfType<Player_Index>();
        player.SetState(Player_Index.aniState.Die);
        ShowTween(modes.GameOver);
    }

    public void GameVictory()
    {
        if(up == 0)
        {
            GameOver();
            return;
        }
        GameOverMenu.SetActive(true);
        level++;
        PlayerPrefs.SetInt("Level", (level > 2 ? 0 : level));
        PlayerPrefs.SetInt("Coin", (coin_lv * (up < 1 ? 1 : up)) + coin);
        FindObjectOfType<Movement_Input>().enabled = false;
        var player = FindObjectOfType<Player_Index>();
        player.SetState(Player_Index.aniState.Victory);
        coin = PlayerPrefs.GetInt("Coin");
        Update_Coin();
        ShowTween(modes.Victory);
    }

    void ShowTween(modes m)
    {
        switch (m.ToString())
        { 
            case "Victory":
                Tween.GetComponent<LevelTween>().i = 1;
                break;
            default:
                Tween.GetComponent<LevelTween>().i = 0;
                break;
        }
        
        Tween.GetComponent<LevelTween>().enabled = true;
    }


    public void Reset_load()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void StopGame()
    {
        Time.timeScale = 0f;
    }

    public void Menu()
    {
        StartCoroutine("MainMenu");
    }

    private IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(0);

        SceneManager.LoadScene("MainMenu");

        //AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
    }

    public void NextLevel()
    {
        StartCoroutine("LoadNextLevel");
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0);

        SceneManager.LoadScene($"Level_{PlayerPrefs.GetInt("Level")}");

    }
}
