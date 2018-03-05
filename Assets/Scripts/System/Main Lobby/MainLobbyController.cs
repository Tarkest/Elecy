using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyController : MonoBehaviour {

    private GameObject _findGameButton;
    private GameObject _armoryButton;
    private GameObject _shopButton;
    private GameObject _machTypeDropdown;
    private GameObject _timeCounter;

    private int matchType = 0;
    public static bool isSearching = false;
    private float _searchTimeCounter = 0f;

    void Awake()
    {
        _findGameButton = GameObject.Find("FindGameButton");
        _armoryButton = GameObject.Find("ArmoryButton");
        _shopButton = GameObject.Find("ShopButton");
        _machTypeDropdown = GameObject.Find("MatchTypeDropdown");
        _timeCounter = GameObject.Find("TimeCounter");
    }

    void Update()
    {
        if(isSearching)
        {
            _searchTimeCounter += Time.deltaTime;
            NetPlayerSendData.SendSearching(_searchTimeCounter);
            //_timeCounter.GetComponent<Text>().text = TimeInText(_searchTimeCounter);
            _machTypeDropdown.GetComponent<Dropdown>().interactable = false;
            _findGameButton.transform.Find("Text").GetComponent<Text>().text = "Searching...";
        }
        else
        {
            _machTypeDropdown.GetComponent<Dropdown>().interactable = true;
            _findGameButton.transform.Find("Text").GetComponent<Text>().text = "Find Game";
        }
    }

    //private string TimeInText(float Time)
    //{
        
    //}

    public void ChangeMatchType()
    {
        switch(matchType)
        {
            case 0:
                matchType = 1;
            break;

            case 1:
                matchType = 0;
            break;
        }
    }

    public void StartSearchingForMatch()
    {
        if(!isSearching)
        { 
            _searchTimeCounter = 0;
            Debug.Log(matchType);
            NetPlayerSendData.SendQueueStart(matchType);
        }
        else if(isSearching)
        {
            isSearching = false;
            _timeCounter.GetComponent<Text>().text = "";
            NetPlayerSendData.SendQueueStop();
        }

    }

}
