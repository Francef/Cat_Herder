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

    [SerializeField] private UIController ui;

    // amount of cats the player has collected in the game
    private int catsCollected = 0;

    // amount of treats the player has collected
    private int treatsCollected = 0;

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
        Messenger.AddListener(GameEvent.CAT_COLLECTED, OnCatCollected);
        Messenger.AddListener(GameEvent.TREAT_COLLECTED, OnTreatCollected);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CAT_COLLECTED, OnCatCollected);
        Messenger.RemoveListener(GameEvent.TREAT_COLLECTED, OnTreatCollected);
    }

    private void OnCatCollected()
    {
        catsCollected++;
        ui.UpdateCatsCollected(catsCollected);
    }    
    private void OnTreatCollected()
    {
        treatsCollected++;
        ui.UpdateTreatsCollected(treatsCollected);
    }
}
