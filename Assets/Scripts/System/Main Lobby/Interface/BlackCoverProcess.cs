using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackCoverProcess : MonoBehaviour {

    private Image _circle;
    private float _process = 0f;
    private bool _side = true;
    public float speed = 0.05f;

	void Start () {
        _circle = GetComponent<Image>();
	}

    void Update()
    {
        StartCoroutine("Rotate");
    }

    IEnumerator Rotate()
    {
        if(_side)
        {
            _process += speed;
            _circle.fillClockwise = _side;
            _circle.fillAmount = _process;
            if (_process >= 1f)
                _side = false;
        }
        if (!_side)
        {
            _process -= speed;
            _circle.fillClockwise = _side;
            _circle.fillAmount = _process;
            if (_process <= 0f)
                _side = true;
        }
        yield return null;
    }
}
