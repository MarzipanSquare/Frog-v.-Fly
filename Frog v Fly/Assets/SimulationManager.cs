using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class SimulationManager : MonoBehaviour
{
    [Header("General")]
    // flag to switch to sim or game
    public bool isSim = false;
    
    // time sim has been running
    private float time = 0;
    public float frogMoves;
    
    // is there a fly active on the scene?
    public bool flyActive = true;
    public GameObject flyPrefab;

    // the current fly in the scene
    public GameObject fly;
    
    [Header("Simulation")]
    private float _numOfFliesCaught = 0;
    private float _frogEfficiencyValue;
    
    #region SimulationUserInterfaceObjects
    public GameObject simInterface;

    private Button _restartButton;
    private Text _timer;
    private Text _frogMoves;
    private Text _flyDistance;
    private Text _fliesCaught;
    private Text _frogEfficiency;

    #endregion

    [Header("Game")] 
    public bool gameOver = false;
    
    // values for scoring
    public int pointsGainedPerFly = 500;
    public int pointsLostPerMove = 100;

    [Tooltip("Number of Lives the frog starts with")]
    public int totalLives = 3;
    
    [Tooltip("Number of Lives the frog current has")]
    private int _livesRemaining = 3;

    [Tooltip("Life Meter Icon")] 
    public GameObject lifeMeterIconPrefab;

    #region GameUserInterfaceObjects

    public GameObject gameInterface;

    private Text _score;
    private GameObject _lifeMeter;
    private List<Image> _meterFrogs;
    private GameObject _gameOverScreen;

    private Button _playAgainButton;
    private Text _finalScoreText;
    
    #endregion


    private void Start()
    {
        // get a random position on screen and create a fly there
        Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
        fly = (GameObject) Instantiate(flyPrefab, position, Quaternion.identity);


        if (isSim)
        {

            simInterface.SetActive(true);
            gameInterface.SetActive(false);
            
            #region GetSimInterfaceObjects
        
            GameObject infoPanel = simInterface.transform.GetChild(0).gameObject;

            _restartButton = simInterface.transform.GetChild(1).gameObject.GetComponent<Button>();

            _restartButton.onClick.AddListener(RestartGame);

            _timer = infoPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
            _frogMoves = infoPanel.transform.GetChild(1).gameObject.GetComponent<Text>();
            _flyDistance = infoPanel.transform.GetChild(2).gameObject.GetComponent<Text>();
            _fliesCaught = infoPanel.transform.GetChild(3).gameObject.GetComponent<Text>();
            _frogEfficiency = infoPanel.transform.GetChild(4).gameObject.GetComponent<Text>();

            #endregion
            
            _timer.text = "Time: 0 s";
            _frogMoves.text = "Moves: 0";
            _flyDistance.text = "Fly Distance: 0";
            _fliesCaught.text = "Flies Caught: 0";
            _frogEfficiency.text = "Efficiency: 0 flies\\move";
            
        }
        else
        {
            gameInterface.SetActive(true);
            simInterface.SetActive(false);

            #region GetGameInterfaceObjects

            _lifeMeter = gameInterface.transform.Find("LifeMeter").gameObject;
            _score = gameInterface.transform.Find("Score").gameObject.GetComponent<Text>();
            _gameOverScreen = gameInterface.transform.Find("GameOverScreen").gameObject;
            _playAgainButton = _gameOverScreen.transform.Find("PlayAgainButton").gameObject.GetComponent<Button>();
            _finalScoreText = _gameOverScreen.transform.Find("Score").gameObject.GetComponent<Text>();

            for (int i = 0; i < totalLives; i++)
            {
                GameObject frogIndicator = Instantiate(lifeMeterIconPrefab, new Vector3(0,0,0), Quaternion.identity);
                frogIndicator.transform.parent = _lifeMeter.transform;
            }
            
            _meterFrogs = new List<Image>(_lifeMeter.GetComponentsInChildren<Image>());
            
            _playAgainButton.onClick.AddListener(RestartGame);

            #endregion

            _gameOverScreen.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // if there's no fly on the scene, make a new one
        if (flyActive == false)
        {
            _numOfFliesCaught += 1;
            
            if (isSim)
            {
                _fliesCaught.text = "Flies Caught: " + _numOfFliesCaught;
            }

            // get a random position on screen and create a fly there
            Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
            fly = (GameObject) Instantiate(flyPrefab, position, Quaternion.identity);
            flyActive = true;
        }
        else if (flyActive == false && isSim)
        {
            _frogEfficiencyValue = _numOfFliesCaught / frogMoves;
            _frogEfficiency.text = "Efficiency: " + _frogEfficiencyValue;
        }

        if (isSim)
        {
            UpdateTimerUI();
            _frogMoves.text = "Moves: " + frogMoves;
        }
        else
        {
            UpdateScore();
        }

    }
    
    private void UpdateTimerUI(){
        //set timer UI
        time += Time.deltaTime;
        _timer.text = "Time: " + time + "s";
    }

    public void UpdateDistance(float distance)
    {
        _flyDistance.text = "Fly Distance: " + distance;
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void RemoveLife()
    {
        
    }

    private void UpdateScore()
    {
        int score = 0;

        score = Mathf.RoundToInt((_numOfFliesCaught * pointsGainedPerFly) - (frogMoves * pointsLostPerMove));

        _score.text = "Score: " + score;

    }

    public void TakeFrogLife()
    {
        if (_meterFrogs.Count > 0)
        {
            _livesRemaining -= 1;
            Destroy(_meterFrogs[0]);
            _meterFrogs.RemoveAt(0);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOver = true;
        _gameOverScreen.SetActive(true);
        _finalScoreText.text = _score.text;
    }
}
