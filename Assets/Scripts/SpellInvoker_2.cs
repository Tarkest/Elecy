using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInvoker_2 : MonoBehaviour {

    /*public float step = 0.0005f;

    public float energy;
    public float energyMax = 70f;
    public float energyMin = 0f;

    public int mightLevel;

    private string _combination;

    // FML\SML\TML\FthML - first\second\third\fourth might levels
    private readonly string[] _possibleCombinationsFML = new string[] { "Q", "E" };
    private readonly string[] _possibleCombinationsSML = new string[] { "QE", "QQ", "EQ", "EE" };
    private readonly string[] _possibleCombinationsTML = new string[] { "QEE", "QEQ", "QQE", "QQQ", "EQQ", "EQE", "EEQ", "EEE" };
    private readonly string[] _possibleCombinationsFthML = new string[] { "QQQQ", "EQQQ", "QEQQ", "EEQQ", "QQEQ", "EQEQ",
                                                                          "QEEQ", "EEEQ", "QQQE", "EQQE", "QEQE", "EEQE",
                                                                          "QQEE", "EQEE", "QEEE", "EEEE" };

    private Dictionary<string, SpellScript> _playerSpellsFML;
    private Dictionary<string, SpellScript> _playerSpellsSML;
    private Dictionary<string, SpellScript> _playerSpellsTML;
    private Dictionary<string, SpellScript> _playerSpellsFthML;

    void Start()
    {
        _combination = "";

        energy = energyMin;

        mightLevel = 1;

        SetSpells();
    }

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            UpdateCombination('Q');
            Debug.Log(_combination);
        }

        if (Input.GetKeyDown("e"))
        {
            UpdateCombination('E');
            Debug.Log(_combination);
        }

        if (Input.GetMouseButtonDown(0))
            InvokeScript(0);

        if (Input.GetMouseButtonDown(1))
            InvokeScript(1);

        UpdateEnergy();

        UpdateMightLevel();

    }

    private void UpdateEnergy()
    {
        if (energy > energyMax & mightLevel != 4) {
            energy = energyMax;
        }

        if (mightLevel == 4) {
            energy -= step;
        }
    }

    private void UpdateMightLevel() {
        if (energy > 10 && mightLevel == 1)
        {
            mightLevel = 2;
            UpdateCombination();
            Debug.Log(mightLevel);
        }
        else if (energy > 30 && (mightLevel == 2 || mightLevel == 1))
        {
            mightLevel = 3;
            UpdateCombination();
            Debug.Log(mightLevel);
        }

        if (energy < 70 && mightLevel == 4) {
            mightLevel = 2;
            energy = 10;
            UpdateCombination();
            Debug.Log(mightLevel);
        }

        if (Input.GetKeyDown("f") && energy >= 60)
        {
            mightLevel = 4;
            energy += 50f;
            UpdateCombination();
            Debug.Log(mightLevel);
        }
    }

    private void UpdateCombination()
    {
        _combination = "";
    }

    private void UpdateCombination(char val)
    {
        switch (mightLevel)
        {
            case 1:
                _combination = "" + val;
                break;
            case 2:
                if (_combination.Length < 2)
                    _combination += val;
                else
                    _combination = _combination.Substring(1) + val;
                break;
            case 3:
                if (_combination.Length < 3)
                    _combination += val;
                else
                    _combination = _combination.Substring(1) + val;
                break;
            case 4:
                if (_combination.Length < 4)
                    _combination += val;
                else
                    _combination = _combination.Substring(1) + val;
                break;
        }
    }

    private void InvokeScript(int type)
    {
        if (_combination.Length > 0)
        {
            switch (mightLevel) {
                case 1:
                    if (type == 0)
                        _playerSpellsFML[_combination].InvokeAttack();
                    else
                        _playerSpellsFML[_combination].InvokeDefense();
                    energy += 7f;
                    break;
                case 2:
                    try
                    {
                        if (type == 0)
                            _playerSpellsSML[_combination].InvokeAttack();
                        else
                            _playerSpellsSML[_combination].InvokeDefense();
                        energy += 7f;
                    }
                    catch
                    {
                        Debug.Log("Combination not found");
                    }
                    break;
                case 3:
                    try
                    {
                        if (type == 0)
                            _playerSpellsTML[_combination].InvokeAttack();
                        else
                            _playerSpellsTML[_combination].InvokeDefense();
                        energy += 7f;
                    }
                    catch
                    {
                        Debug.Log("Combination not found");
                    }
                    break;
                case 4:
                    try
                    {
                        if (type == 0)
                            _playerSpellsFthML[_combination].InvokeAttack();
                        else
                            _playerSpellsFthML[_combination].InvokeDefense();
                        energy -= 7f;
                    }
                    catch
                    {
                        Debug.Log("Combination not found");
                    }
                    break;
            }
        }
        UpdateCombination();
    }

    private void SetSpells()
    {
        _playerSpellsFML = new Dictionary<string, SpellScript>();
        _playerSpellsSML = new Dictionary<string, SpellScript>();
        _playerSpellsTML = new Dictionary<string, SpellScript>();
        _playerSpellsFthML = new Dictionary<string, SpellScript>();

        for (int i = 0; i < _possibleCombinationsFML.Length; i++)
        {
            _playerSpellsFML.Add(_possibleCombinationsFML[i], new SpellScript("script " + i));
        }

        for (int i = 0; i < _possibleCombinationsSML.Length; i++)
        {
            _playerSpellsSML.Add(_possibleCombinationsSML[i], new SpellScript("script " + i));
        }

        for (int i = 0; i < _possibleCombinationsTML.Length; i++)
        {
            _playerSpellsTML.Add(_possibleCombinationsTML[i], new SpellScript("script " + i));
        }

        for (int i = 0; i < _possibleCombinationsFthML.Length; i++)
        {
            _playerSpellsFthML.Add(_possibleCombinationsFthML[i], new SpellScript("script " + i));
        }
    }*/
}