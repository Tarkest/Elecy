using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeveloperScreenController : MonoBehaviour {

    GameObject text;
    GameObject content;
    private static string _debugText = "Elecy Client V 0.00.1";
    private static float _messageCount;

    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    void Start()
    {
        text = gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").Find("ConsoleText").gameObject;
    }

    void Update ()
    {
        text.GetComponent<Text>().text = _debugText;
        gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").GetComponent<RectTransform>().sizeDelta = new Vector2(0, (float)_messageCount * 16);
    }

    public static void AddInfo(string info)
    {
        _messageCount++;
        _debugText += "/n" + info;
    }
}
