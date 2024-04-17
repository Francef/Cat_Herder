using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI catValue;
    [SerializeField] private TextMeshProUGUI treatValue;
    //[SerializeField] private SettingsPopup settingsPopup;
    //[SerializeField] private GameCompletedPopup gameCompletedPopup;
    private int initialCats = 0;
    private int catsCollected;
    private int initialTreats = 0;
    private int treatsCollected;
    void Start()
    {
        catsCollected = initialCats;
        catValue.text = catsCollected.ToString();
        treatsCollected = initialTreats;
        treatValue.text = treatsCollected.ToString();
    }

    void Update()
    {
        
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.CAT_COLLECTED, OnCatCollected);
        Messenger<int>.AddListener(GameEvent.TREAT_COLLECTED, OnTreatsChanged);
        Messenger<int>.AddListener(GameEvent.TREATS_USED, OnTreatsChanged);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.CAT_COLLECTED, OnCatCollected);
        Messenger<int>.RemoveListener(GameEvent.TREAT_COLLECTED, OnTreatsChanged);
        Messenger<int>.RemoveListener(GameEvent.TREATS_USED, OnTreatsChanged);
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
    }

    // update treats collected display
    public void UpdateTreatsCollected(int newTreatAmount)
    {
        treatsCollected += newTreatAmount;
        treatValue.text = treatsCollected.ToString();
    }
}
