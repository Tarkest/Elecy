using UnityEngine;
using UnityEngine.UI;

public class EntranceProcessWindow : MonoBehaviour
{

    private bool _destroy;

    public void Update()
    {
        if(_destroy)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text)
    {
        gameObject.transform.Find("Text").GetComponent<Text>().text = text;
        EntranceController.Increase();
    }

    public void Destroy()
    {
        EntranceController.Decrease();
        _destroy = true;
    }

}
