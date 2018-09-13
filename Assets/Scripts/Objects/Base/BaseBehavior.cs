using System;
using UnityEngine;

public abstract class BaseBehavior : MonoBehaviour
{

    internal abstract void Invoke();

    protected internal Tags StringToTag(string tag)
    {
        return (Tags)Enum.Parse(typeof(Tags), tag);
    }
}

