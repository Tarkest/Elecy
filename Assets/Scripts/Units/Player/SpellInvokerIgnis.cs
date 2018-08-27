using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInvokerIgnis : MonoBehaviour {

    private string _combination;

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
            Quaternion _spawnRotation = Quaternion.identity;
            switch(Network.currentManager.dynamicPropList.Get(number).SpawnPoint)
            {
                case NetworkObjectController.StartTransform.Caster:
                    _spawnPoint = gameObject.transform.position;
                    _spawnRotation = gameObject.transform.rotation;
                    break;

                case NetworkObjectController.StartTransform.Mouse:
                    _spawnPoint = MouseController.mousePosition;
                    break;

                case NetworkObjectController.StartTransform.Behaviour:

                    break;
            }
            Network.NetworkInstantiate(Network.currentManager.dynamicPropList.Get(number).gameObject, _spawnPoint, _spawnRotation);
            _combination = "";
        }
        else
        {
            Debug.Log("My reactor is not ready for this");
        }

    } 

    public void LoadCombinations(List<GameObject> spells)
    {
        for(int i = 0; i <= GSC.IgnisSpellCount; i++)
        {
            try
            {
                _posibleCombination[i] = spells[i].GetComponent<NetworkObjectController>().combination;
            }
            catch
            {
                _posibleCombination[i] = null;
            }
        }
    }
}
