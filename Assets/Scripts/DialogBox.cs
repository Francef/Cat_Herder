using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : BasePopup
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    override public void Open()
    {
        base.Open();
    }
    override public void Close()
    {
        base.Close();
    }

    override public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    public void OnCloseButton()
    {
        Debug.Log("close dialog");
        Close();
    }
}
