using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceExitWindow : MonoBehaviour
{
    public void No()
    {
        EntranceController.Decrease();
        Destroy(gameObject);
    }

    public void Yes()
    {
        EntranceController.Decrease();
        Application.Quit();
    }

}
