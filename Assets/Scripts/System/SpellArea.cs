using UnityEngine;

public class SpellArea : MonoBehaviour {

    private float _spellArea;
    private float _spellRange;

    private string _targetType;

    private Vector3 _mousePosition;
    private Vector3 _playerPosition;

    private SpellContainer _spellContainer;

    void Start()
    {
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _playerPosition = GameObject.Find("Test Player").GetComponent<Transform>().position;
        _spellContainer = GameObject.Find("Test Player").GetComponent<SpellInvoker>().spellConteiner.GetComponent<SpellContainer>();
        _targetType = _spellContainer.targetType;
        _spellArea = _spellContainer.castArea;
        _spellRange = _spellContainer.distance;
    }

    void Update()
    {
        _spellContainer = GameObject.Find("Test Player").GetComponent<SpellInvoker>().spellConteiner.GetComponent<SpellContainer>();
        _mousePosition = GameObject.Find("MouseController").GetComponent<MouseController>().mousePosition;
        _playerPosition = GameObject.Find("Test Player").GetComponent<Transform>().position;
        _targetType = _spellContainer.targetType;
        _spellArea = _spellContainer.castArea;
        _spellRange = _spellContainer.distance;
    }

	void FixedUpdate ()
    {
        
    }
}
