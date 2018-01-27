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
 
        if (currentHP <= 0)
            Destroy(gameObject);
        else if ((float)currentHP / _maxHp < 0.3)
            _textMesh.color = Color.red;
        else if ((float)currentHP / _maxHp < 0.7)
            _textMesh.color = Color.yellow;
        else if ((float)currentHP / _maxHp >= 0.7)
            _textMesh.color = Color.green;
    }
}
