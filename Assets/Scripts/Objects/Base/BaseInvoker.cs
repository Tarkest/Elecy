using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public abstract class BaseInvoker : MonoBehaviour, IBaseObjectSpecifier<Player>
{
    protected BaseObject mBaseObject;
    protected string _combination;
    protected string[] _posibleCombinations;

    public Player BaseObject
    {
        get
        {
            return mBaseObject as Player;
        }
    }

    #region Unity's

    void Update()
    {
        if (Input.GetButtonDown("CombinationInputLeft"))
        {
            UpdateCombination('Q');
        }

        if (Input.GetButtonDown("CombinationInputRight"))
        {
            UpdateCombination('E');
        }

        if (Input.GetButtonDown("ResetCombination"))
        {
            UpdateCombination();
        }

        if (Input.GetButtonDown("AttackInvoke"))
        {
            InvokeSpell(Invoke(_posibleCombinations, _combination));
        }

        if (Input.GetButtonDown("DefenceInvoke"))
        {
            InvokeSpell(Invoke(_posibleCombinations, _combination));
        }
    }

    #endregion

    #region Update Combination

    protected void UpdateCombination()
    {
        _combination = "";
    }

    protected void UpdateCombination(char val)
    {
        if (_combination.Length <= 4)
        {
            _combination += val;
        }
        else
        {
            _combination = "";
        }
    }

    #endregion

    #region Initialization 

    public virtual void Init(BaseObject obj, List<GameObject> spells)
    {
        _combination = "";
        mBaseObject = obj;
        LoadCombinations(spells);
    }

    #endregion

    protected void InvokeSpell(int number)
    {
        if (number != -1)
        {
            Vector3 _spawnPoint = Vector3.zero;
            Vector3 _targetPoint = Vector3.zero;
            Quaternion _spawnRotation = Quaternion.identity;
            switch (Network.currentManager.prefabList[number].GetComponent<SpellStats>().Stats.Movement)
            {
                case SpellMovement.CasterToPointMovement:
                    _spawnPoint = gameObject.transform.position;
                    _spawnRotation = gameObject.transform.rotation;
                    _targetPoint = MouseController.mousePosition;
                    break;

            }
            Network.NetworkInstantiate(Network.currentManager.prefabList[number].gameObject, _spawnPoint, _targetPoint, _spawnRotation);
        }
        else
        {
            Debug.Log("My reactor is not ready for this");
        }
        UpdateCombination();
    }

    protected int Invoke(string[] Combinations, string CurrentCombination)
    {
        _posibleCombinations.Contains(CurrentCombination);
        for (int i = 0; i < Combinations.Length; i++)
        {
            if (Combinations[i] == CurrentCombination)
            {
                return i;
            }
        }
        return -1;
    }

    protected void LoadCombinations(List<GameObject> spells)
    {
        for (int i = 0; i < spells.Count; i++)
        {
            _posibleCombinations[i] = spells[i].GetComponent<SpellStats>().Stats.Combination;
        }
    }

}

