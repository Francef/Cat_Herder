using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinPopup : BasePopup
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnExitGameButton()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    public void OnStartButton()
    {
        Close();
        Messenger.Broadcast(GameEvent.RESTART_GAME);
    }

}
