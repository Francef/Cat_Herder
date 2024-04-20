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
    // list of vectors where cats can spawn
    private List<Vector3> spawnPtList = new List<Vector3>() { new Vector3 (85,0,43), new Vector3(48,0,-34), new Vector3(-21,0,30), new Vector3(-93,0,44), new Vector3(-96,0,-34), new Vector3(-55,0,5), new Vector3(51,1,12), new Vector3(-10,1,-34), new Vector3(-47,1,-50), new Vector3(8,1,43) };

    [SerializeField] private GameObject treatPrefab;
    [SerializeField] private UIController ui;

    private Coroutine introDialog;
    void Start()
    {
        introDialog = StartCoroutine(Intro());
        cats = new GameObject[numCats];

        // spawn multiple cat instances
        for (int i = 0; i < cats.Length; i++)
        {
            if (cats[i] == null)
            {
                
                // generate a random position from spawnPtList
                catSpawnPt = spawnPtList[Random.Range(0, spawnPtList.Count)];
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
        Messenger.AddListener(GameEvent.WIN_GAME, OnWinGame);
        Messenger.AddListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.WIN_GAME, OnWinGame);
        Messenger.RemoveListener(GameEvent.RESTART_GAME, OnRestartGame);
    }

    public void OnRestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnWinGame()
    {
        ui.ShowYouWinPopup();
    }

    IEnumerator Intro()
    {
        Messenger<string>.Broadcast(GameEvent.DIALOG_EVENT, "intro1");
        yield return new WaitForSeconds(0.5f);
        Messenger<string>.Broadcast(GameEvent.DIALOG_EVENT, "intro2");
        yield return new WaitForSeconds(0.5f);
        Messenger<string>.Broadcast(GameEvent.DIALOG_EVENT, "intro3");
    }

}
