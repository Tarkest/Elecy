using UnityEngine;
using System.Collections;

public class Background_Stats : MonoBehaviour
{

    public int currentHP;

    private int _maxHp = 50;
    private TextMesh _textMesh;
    
    void Start()
    {
        currentHP = _maxHp;
        _textMesh = gameObject.transform.Find("TestUI").GetComponent<TextMesh>();
        _textMesh.text = _maxHp + "";

    }
    
    void Update()
    {
        _textMesh.text = currentHP + "";

        if (currentHP <= 0) {
            Destroy(gameObject);
        }
    }
}
