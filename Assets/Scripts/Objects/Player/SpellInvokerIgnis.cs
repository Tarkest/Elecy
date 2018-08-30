using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInvokerIgnis : MonoBehaviour {

    public string _combination = "";

    private string[] _posibleCombination = new string[GSC.IgnisSpellCount];

    void Update()
    {
        if(Input.GetButtonDown("CombinationInputLeft"))
        {
            UpdateCombination('Q');
        }

        if(Input.GetButtonDown("CombinationInputRight"))
        {
            UpdateCombination('E');
        }

        if(Input.GetButtonDown("ResetCombination"))
        {
            UpdateCombination();
        }

        if (Input.GetButtonDown("AttackInvoke"))
        {
            InvokeSpell(Invoke(_posibleCombination, _combination));
        }

        if (Input.GetButtonDown("DefenceInvoke"))
        {
            InvokeSpell(Invoke(_posibleCombination, _combination));
        }
    }

    void UpdateCombination()
    {
        _combination = "";
    }

    void UpdateCombination(char val)
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

    int Invoke(string[] Combinations, string CurrentCombination)
    {
        for(int i = 0; i< Combinations.Length; i++)
        {
            if(Combinations[i] == CurrentCombination)
            {
                return i;
            }
        }
        return -1;
    }

    void InvokeSpell(int number)
    {
        if(number != -1)
        {
            Vector3 _spawnPoint = Vector3.zero;
            Vector3 _targetPoint = Vector3.zero;
            Quaternion _spawnRotation = Quaternion.identity;
            switch ((Network.currentManager.prefabList[number].GetComponent<SpellStats>().stats as SpellMenu).targetType)
            {
                case StartTransform.Caster:
                    _spawnPoint = gameObject.transform.position;
                    _spawnRotation = gameObject.transform.rotation;
                    _targetPoint = MouseController.mousePosition;
                    break;

                case StartTransform.Mouse:
                    _spawnPoint = MouseController.mousePosition;
                    _targetPoint = gameObject.transform.position;
                    break;

                case StartTransform.Behaviour:
                    _spawnPoint = MouseController.mousePosition;
                    break;
            }
            Network.NetworkInstantiate(Network.currentManager.prefabList[number].gameObject, _spawnPoint, _targetPoint, _spawnRotation);
            _combination = "";
        }
        else
        {
            Debug.Log("My reactor is not ready for this");
        }

    } 

    public void LoadCombinations(List<GameObject> spells)
    {
        for(int i = 0; i < spells.Count; i++)
        {
            _posibleCombination[i] = (spells[i].GetComponent<SpellStats>().stats as SpellMenu).combination;
        }
    }
}
