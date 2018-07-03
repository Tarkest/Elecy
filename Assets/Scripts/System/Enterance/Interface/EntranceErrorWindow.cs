using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntranceErrorWindow : MonoBehaviour
{

    public void SetMsg(string msg)
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = msg;
        EntranceController.Increase();
    }

    public void Destroy()
    {
        EntranceController.Decrease();
        Destroy(gameObject);
    }

}
