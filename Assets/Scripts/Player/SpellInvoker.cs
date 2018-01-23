using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInvoker : MonoBehaviour
{
    private string _combination;

    private readonly string[] _possibleCombinations = new string[] {"Q", "E" , "QQ" , "EE" , "QE" , "EQ" , "QQQ" , "EEE" , "QEQ" , "QEQE"};

    private Dictionary<string, SpellScript> _playerScripts;

    private TextMesh _textMesh;

    void Start ()
	{    
	    _combination = "";

        _textMesh = gameObject.transform.Find("TestUi").GetComponent<TextMesh>();

        SetSpells();
	}
	
	void Update () {
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
        if (_combination.Length > 0)
        {
            try
            {
                if (type == 0)
                    _playerScripts[_combination].InvokeAttack();
                else
                    _playerScripts[_combination].InvokeDefense();
            }
            catch
            {
                Debug.Log("Combination not found");
            }
            
        }
        UpdateCombination();
    }

    private void SetSpells()
    {
        _playerScripts = new Dictionary<string,SpellScript>();

        for (int i = 0; i < _possibleCombinations.Length; i++)
        {
            _playerScripts.Add(_possibleCombinations[i],new SpellScript("script "+i));
        }
    }
}

//TEST muha2399, do not touch 
public class SpellScript
{
    private string _name;

    public SpellScript(string name)
    {
        _name = name;
    }

    public void InvokeAttack()
    {
        Debug.Log(_name + " Attack");
    }

    public void InvokeDefense()
    {
        Debug.Log(_name + " Defense");
    }
}

