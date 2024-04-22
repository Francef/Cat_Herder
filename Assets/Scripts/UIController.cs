using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI catValue;
    [SerializeField] private TextMeshProUGUI treatValue;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private YouWinPopup youWinPopup;
    [SerializeField] private DialogBox dialogBox;
    private int initialCats = 0;
    private int catsCollected;
    private int initialTreats = 0;
    private int treatsCollected;
    private int popupsActive = 0;
    Dictionary<String, String> dialogDict = new Dictionary<String, String>() 
    { { "intro1","Oh no! Five stray cats are wandering the hillside. Go gather them before they fall in with a bad crowd!" }, 
      { "intro2","The thing about cats is they don't trust just anybody. You might have to bribe your way into their inner circle." }, 
      { "intro3","Go find three treats and get close enough to a cat for them to catch the scent. Press 'enter' to give up those treats and they will make a purrfect companion on your adventures." }, 
      { "end","Pawsome! You collected all five cats, go back home so they can go have a well-deserved 23 hour nap." } 
    }; 
    
    void Start()
    {
        catsCollected = initialCats;
        catValue.text = catsCollected.ToString();
        UpdateTreatsCollected(initialTreats);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && popupsActive == 0)
        {
            optionsPopup.Open();
        }
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.CAT_COLLECTED, OnCatCollected);
        Messenger<int>.AddListener(GameEvent.TREAT_COLLECTED, OnTreatsChanged);
        Messenger<int>.AddListener(GameEvent.TREATS_USED, OnTreatsChanged);
        Messenger.AddListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.AddListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger<string>.AddListener(GameEvent.DIALOG_EVENT, OnDialogEvent);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CAT_COLLECTED, OnCatCollected);
        Messenger<int>.RemoveListener(GameEvent.TREAT_COLLECTED, OnTreatsChanged);
        Messenger<int>.RemoveListener(GameEvent.TREATS_USED, OnTreatsChanged);
        Messenger.RemoveListener(GameEvent.POPUP_OPENED, OnPopupOpened);
        Messenger.RemoveListener(GameEvent.POPUP_CLOSED, OnPopupClosed);
        Messenger<string>.RemoveListener(GameEvent.DIALOG_EVENT, OnDialogEvent);
    }

    private void OnCatCollected()
    {
        UpdateCatsCollected();
    }

    private void OnTreatsChanged(int newTreats)
    {
        UpdateTreatsCollected(newTreats);
    }

    // update cats collected display
    public void UpdateCatsCollected()
    {
        catsCollected++;
        catValue.text = catsCollected.ToString();
        if (catsCollected == 5)
        {
            Messenger.Broadcast(GameEvent.ALL_CATS_COLLECTED);
            Messenger<string>.Broadcast(GameEvent.DIALOG_EVENT, "end");
        }
    }

    // update treats collected display
    public void UpdateTreatsCollected(int newTreatAmount)
    {
        treatsCollected += newTreatAmount;
        treatValue.text = treatsCollected.ToString();
    }

    public void SetGameActive(bool active)
    {
        if (active)
        {
            Time.timeScale = 1;                         // unpause the game
            Cursor.lockState = CursorLockMode.Locked;   // lock cursor at center
            Cursor.visible = false;                     // hide cursor
            Messenger.Broadcast(GameEvent.GAME_ACTIVE);
        }
        else
        {
            Time.timeScale = 0;                         // pause the game
            Cursor.lockState = CursorLockMode.None;     // let cursor move freely
            Cursor.visible = true;                      // hide cursor
            Messenger.Broadcast(GameEvent.GAME_INACTIVE);
        }
    }

    private void OnPopupOpened()
    {
        if (popupsActive == 0)
        {
            SetGameActive(false);
        }
        popupsActive++;
    }

    private void OnPopupClosed()
    {
        popupsActive--;
        if (popupsActive == 0)
        {
            SetGameActive(true);
        }
    }

    private void OnDialogEvent(string dialog)
    {
        dialogBox.Open();
        UpdateDialogText(dialog);
    }

    public void UpdateDialogText(string dialog)
    {
        dialogText.text = dialogDict[dialog];
    }

    public void ShowYouWinPopup()
    {
        youWinPopup.Open();
    }
}
