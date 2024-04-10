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
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // update cats collected display
    public void UpdateCatsCollected(int newCatAmount)
    {
        catValue.text = newCatAmount.ToString();
    }

    // update treats collected display
    public void UpdateTreatsCollected(int newTreatAmount)
    {
        treatValue.text = newTreatAmount.ToString();
    }
}
