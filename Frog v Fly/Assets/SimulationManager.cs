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
    // time sim has been running
    private float time = 0;
    public float frogMoves;
    private float _numOfFliesCaught = 0;
    private float _frogEfficiencyValue;

    // how long lilypads stay underwater
    public int lilypadWaitTime = 2;
    
    // is there a fly active on the scene?
    public bool flyActive = true;
    public GameObject flyPrefab; 
    
    // flag to switch to sim or game
    public bool isSim = false;

    // the current fly in the scene
    public GameObject fly;

    #region UserInterfaceObjects
        public GameObject simInterface;

        private Button _restartButton;
        private Text _timer;
        private Text _frogMoves;
        private Text _flyDistance;
        private Text _fliesCaught;
        private Text _frogEfficiency;

    #endregion


    private void Start()
    {
        // get a random position on screen and create a fly there
        Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
        fly = (GameObject) Instantiate(flyPrefab, position, Quaternion.identity);
        

        #region GetUserInterfaceObjects
        
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

    // Update is called once per frame
    void Update()
    {
        // if there's no fly on the scene, make a new one
        if (flyActive == false)
        {
            _numOfFliesCaught += 1;
            _fliesCaught.text = "Flies Caught: " + _numOfFliesCaught;
            // get a random position on screen and create a fly there
            Vector3 position = new Vector3(Random.Range(-9, 9), Random.Range(-5, 5), 0);
            fly = (GameObject) Instantiate(flyPrefab, position, Quaternion.identity);
            flyActive = true;
        }
        else
        {
            _frogEfficiencyValue = _numOfFliesCaught / frogMoves;
            _frogEfficiency.text = "Efficiency: " + _frogEfficiencyValue;
        }

        UpdateTimerUI();
        _frogMoves.text = "Moves: " + frogMoves;

    }
    
    void UpdateTimerUI(){
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
}
