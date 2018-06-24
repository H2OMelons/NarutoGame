using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager manager;

    public GameObject[] backgrounds;
    public GameObject[] playerPrefabs;      // Reference to the player prefabs that the player will control
    public GameObject[] player1Names;
    public GameObject[] player2Names;
    public PlayerManager[] players;         // Collection of managers to enable/disable the players
    public float startDelay = 3f;           // Amount of time to wait at the beginning of the round
    public float endDelay = 3f;             // Amount of time to wait at the end of the round

    [HideInInspector] public int sceneSelection;
    [HideInInspector] public static int player1Choice;
    [HideInInspector] public static int player2Choice;

    private WaitForSeconds startWait;       // Delay for when the game starts
    private WaitForSeconds endWait;         // Delay for when the game ends
    private PlayerManager winner;           // Reference to the winner of the fight

    private static bool transitioningToPlay = false;

    private TimerManager timerManager;

    private void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);
            manager = this;
        }
        else if (manager != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        // Create the wait times
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);
        timerManager = GetComponent<TimerManager>();
	}

    private void SpawnPlayers()
    {
        players[0].instance = Instantiate(playerPrefabs[player1Choice], players[0].spawnPoint.position, players[0].spawnPoint.rotation);
        players[0].playerNumber = 1;
        players[0].Setup();
        Instantiate(player1Names[player1Choice]);
        players[1].instance = Instantiate(playerPrefabs[player2Choice], players[1].spawnPoint.position, players[1].spawnPoint.rotation);
        players[1].playerNumber = players.Length;
        players[1].Setup();
        Instantiate(player2Names[player2Choice]);
        timerManager.StartCountDown();
    }

    public void TransitionToCharacterSelection()
    {
        SceneManager.LoadScene("PlayerChooser");
    }

    public void TransitionToSceneSelection()
    {
        SceneManager.LoadScene("BackgroundChooser");
        
    }

    public void TransitionToPlay()
    {
        transitioningToPlay = true;
        SceneManager.LoadScene(backgrounds[sceneSelection].name);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (transitioningToPlay)
        {
            transitioningToPlay = false;
            SpawnPlayers();
        }
        
    }

    public void SetPlayerSelections(int p1, int p2)
    {
        player1Choice = p1;
        player2Choice = p2;
    }

    public void EndGame()
    {
        //players[0].DisableControl();
        //players[1].DisableControl();
        players[0].Reset();
        players[1].Reset();
    }

    public void EnableControls()
    {
        players[0].EnableControl();
        players[1].EnableControl();
    }
}
