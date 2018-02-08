using System;
using UnityEngine;

public class SpellInvoker : MonoBehaviour
{
    private string _combination;

    private readonly string[] _possibleCombinations = new string[] { "", "Q", "E", "QQ", "EE", "QE", "EQ", "QQQ", "EEE", "QEQ", "QEQE" };

    private string[] _spells;

    [System.NonSerialized]
    public int spellType;
    private int _snCost = 0;
    private int _currentSN;

    private TextMesh _textMesh;
    private TextMesh _snTextMesh;

    [System.NonSerialized]
    public GameObject spellConteiner;

    [System.NonSerialized]
    public string spellName;

    private bool isStunned = false;
    private bool isCasting = false;

    void Start()
    {
        _combination = "";

        _textMesh = gameObject.transform.Find("TestUi").GetComponent<TextMesh>();

        _snTextMesh = gameObject.transform.Find("TestUiSnCost").GetComponent<TextMesh>();

        _currentSN = GetComponent<PlayerStats>().playerCurrentSN;

        SpellsContainer();

        spellName = "Spells/" + _spells[Invoke(_possibleCombinations, _combination)];
    }

    void Update()
    {
        isStunned = GetComponent<PlayerStats>().isStunned;
        isCasting = GetComponent<PlayerStats>().isCasting;

        _currentSN = GetComponent<PlayerStats>().playerCurrentSN;

        if(!isCasting)
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
        }

        if(!isStunned && !isCasting)
        {
            if (Input.GetMouseButtonDown(0))
                InvokeScript(0);

            if (Input.GetMouseButtonDown(1))
                InvokeScript(1);
        }
    }

    private void UpdateCombination()
    {
        spellType = 0;
        _combination = "";
        _textMesh.text = _combination;
        _snTextMesh.text = "";
        _snCost = 0;
        spellName = "Spells/" + _spells[Invoke(_possibleCombinations, _combination)];
    }

    private void UpdateCombination(char val)
    {
        if (_combination.Length <= 4)
            _combination += val;

        _textMesh.text = _combination;

        spellName = "Spells/" + _spells[Invoke(_possibleCombinations, _combination)];

        spellConteiner = (Resources.Load(spellName, typeof(GameObject)) as GameObject);
        spellConteiner.GetComponent<SpellContainer>().SpellConteinerLoad();

        _snCost = (Resources.Load(spellName, typeof(GameObject)) as GameObject).GetComponent<SpellContainer>().sunergyCost;

        _snTextMesh.text = Convert.ToString(_snCost);
    }

    private void InvokeScript(int type)
    {
        if (_currentSN >= _snCost)
        {
            try
            {
                if (type == 0)
                {
                    Instantiate(Resources.Load(spellName, typeof(GameObject)));
                    spellType = 1;
                }
                else
                {
                    Instantiate(Resources.Load(spellName, typeof(GameObject)));
                    spellType = 2;
                }

                GetComponent<PlayerStats>().PlayerSynergyUpdate(-(_snCost));
            }
            catch
            {
                Debug.Log("Combination not found");
            }
            UpdateCombination();
        }
        else
        {
            Debug.Log("Not enought synergy");
            UpdateCombination();
        }

    }

    private int Invoke(string[] combinations, string combination)
    {
        int _spellnumber = 99;
        for (int i = 0; i < combinations.Length; i++) {
            if (combinations[i] == combination)
                _spellnumber = i;
        }
        return _spellnumber;
    }

    // Test
    private void SpellsContainer()
    {
        _spells = new string[6];

        _spells[0] = "Test2";
        _spells[1] = "FireBall 1";
        _spells[2] = "FireBall 2";
        _spells[3] = "FireBall 3";
        _spells[4] = "Test2";
        _spells[5] = "Test3";
    }
}
