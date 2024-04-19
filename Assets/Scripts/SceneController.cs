using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject catPrefab;
    private GameObject[] cats;
    private int numCats = 5;
    private Vector3 catSpawnPt;
    // bounds for where cats should spawn
    private int xMin = -100;
    private int xMax = 100;
    private int zMin = -40;
    private int zMax = 100;

    [SerializeField] private GameObject treatPrefab;
    [SerializeField] private UIController ui;

    void Start()
    {
        cats = new GameObject[numCats];

        // spawn multiple cat instances
        for (int i = 0; i < cats.Length; i++)
        {
            if (cats[i] == null)
            {
                
                // generate a random position
                catSpawnPt = new Vector3(Random.Range(xMin, xMax), 1, Random.Range(zMin, zMax));
                // generate a random rotation
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                cats[i] = Instantiate(catPrefab, catSpawnPt, randomRotation) as GameObject;
            }
        }
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnWinGame()
    {
        ui.ShowGameOverPopup();
    }

}
