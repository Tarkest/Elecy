using System.Collections.Generic;
using UnityEngine;

internal interface IPlayer
{
    void LoadCombinations(List<GameObject> spells);
    Vector3 GetPosition();
}

