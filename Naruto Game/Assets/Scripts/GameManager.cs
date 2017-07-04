using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager manager;

    public GameObject[] backgrounds;
    public GameObject[] playerPrefabs;      // Reference to the player prefabs that the player will control
    public PlayerManager[] players;         // Collection of managers to enable/disable the players
    public float startDelay = 3f;           // Amount of time to wait at the beginning of the round
    public float endDelay = 3f;             // Amount of time to wait at the end of the round

    [HideInInspector] public int sceneSelection;
    [HideInInspector] public int player1Choice;
    [HideInInspector] public int player2Choice;

    private WaitForSeconds startWait;       // Delay for when the game starts
    private WaitForSeconds endWait;         // Delay for when the game ends
    private PlayerManager winner;           // Reference to the winner of the fight

    private bool transitioningToPlay;

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
	}

    private void SpawnPlayers()
    {
        players[0].instance = Instantiate(playerPrefabs[player1Choice], players[0].spawnPoint.position, players[0].spawnPoint.rotation);
        players[0].playerNumber = 1;
        players[0].Setup();
        players[1].instance = Instantiate(playerPrefabs[player2Choice], players[1].spawnPoint.position, players[1].spawnPoint.rotation);
        players[1].playerNumber = players.Length;
        players[1].Setup();
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
}
