using UnityEngine;
using System.Collections;
using System;

public class SpellInvoker999 : MonoBehaviour
{
    private string _combination;

    private readonly string[] _possibleCombinations = new string[] { "", "Q", "E", "QQ", "EE", "QE", "EQ", "QQQ", "EEE", "QEQ", "QEQE" };

    private GameObject[] _spells;

    public GameObject spell1;
    public GameObject spell2;
    public GameObject spell3;

    public int spellType;

    private TextMesh _textMesh;

    void Start()
    {
        _combination = "";

        _textMesh = gameObject.transform.Find("TestUi").GetComponent<TextMesh>();

        SpellsContainer();
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            UpdateCombination('Q');
        }

        if (Input.GetKeyDown("e"))
        {
            UpdateCombination('E');
        }

        if (Input.GetKeyDown("c"))
        {
            UpdateCombination();
        }

        if (Input.GetMouseButtonDown(0))
            InvokeScript(0);

        if (Input.GetMouseButtonDown(1))
            InvokeScript(1);
    }

    private void UpdateCombination()
    {
        spellType = 0;
        _combination = "";
        _textMesh.text = _combination;
    }

    private void UpdateCombination(char val)
    {
        if (_combination.Length <= 4)
            _combination += val;

        _textMesh.text = _combination;
    }

    private void InvokeScript(int type)
    {
        try
        {
            if (type == 0)
            {
                Instantiate(_spells[Array.IndexOf(_possibleCombinations, _combination)]);
                spellType = 1;

            }
            else
            {
                Instantiate(_spells[Array.IndexOf(_possibleCombinations, _combination)]);
                spellType = 2;
            }
        }
        catch
        {
            Debug.Log("Combination not found");
        }
        UpdateCombination();
    }

    // Test
    private void SpellsContainer()
    {
        _spells = new GameObject[6];

        _spells[0] = spell1;
        _spells[1] = spell2;
        _spells[2] = spell3;
        _spells[3] = spell1;
        _spells[4] = spell2;
        _spells[5] = spell3;
    }
}
