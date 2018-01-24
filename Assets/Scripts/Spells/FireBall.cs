using UnityEngine;

public class FireBall : MonoBehaviour {

    [SerializeField]
    private float _spellRange = 10f;
    [SerializeField]
    private float _spellSpeed = 10f;
    private Vector3 _casterPosition;
    private Vector3 _mousePosition;
    private Vector3 _target;
    private Rigidbody _spellRigidbody;
    private float _startTime;
    private float _spellJourneyLenght;
    private float _spellDistCovered;
    private float _spellJourney = 0f;

    void Start () {
        _spellRigidbody = GetComponent<Rigidbody>();
        _casterPosition = GameObject.Find("Test player").GetComponent<Transform>().position;
        Debug.Log(_casterPosition);
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _target = (_mousePosition - _casterPosition) * _spellRange;
        Debug.Log(_target);
        _startTime = Time.time;
        _spellJourneyLenght = Vector3.Distance(_casterPosition, _target);
        _spellJourney = 0f;
    }
	
    void FixedUpdate () {
        if (_spellJourney<1)
        {
            _spellDistCovered = (Time.time - _startTime) * _spellSpeed;
            _spellJourney = _spellDistCovered / _spellJourneyLenght;
            Debug.Log(_spellJourney);
            _spellRigidbody.MovePosition(Vector3.Lerp(_casterPosition, _target, _spellJourney));
        }
        else
        {
            
        }
    }

	void Update () {
		
	}
}
