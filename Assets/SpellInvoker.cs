using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInvoker : MonoBehaviour
{
    private string _combination;

    private SpellScript[] _playerScripts;

	// Use this for initialization
	void Start ()
	{
	    _combination = "";

        SetSpells();
	}
	
	// Update is called once per frame
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
    }

    private void UpdateCombination(char val)
    {
        if (_combination.Length <= 5)
            _combination += val;
    }


    //TODO simplify
    private void InvokeScript(int type)
    {
        switch (_combination)
        {
            case "QQE":
                if (type == 0)
                    _playerScripts[0].InvokeAttack();
                else
                    _playerScripts[0].InvokeDefense();
                break;
            case "QEE":
                if (type == 0)
                    _playerScripts[1].InvokeAttack();
                else
                    _playerScripts[1].InvokeDefense();
                break;
            default:
                if (type == 0)
                    _playerScripts[0].InvokeAttack();
                else
                    _playerScripts[0].InvokeDefense();
                break;

        }

        _combination = "";
    }

    private void SetSpells()
    {
        _playerScripts = new SpellScript[]
        {
            new SpellScript("TestSpell1"), new SpellScript("TestSpell2"), new SpellScript("TestSpell3"),
            new SpellScript("TestSpell4"),
        };
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