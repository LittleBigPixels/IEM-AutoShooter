using System;
using System.Collections.Generic;
using UnityEngine;

public class ExempleEnumerator
{
    public List<int> MyValues;

    public void Enumerate()
    {
        foreach (var value in GetValues())
        {
            Debug.Log("My value is: " + value);
        }
    }

    public IEnumerable<int> GetValues()
    {
        foreach (int value in MyValues)
        {
            yield return value;
        }
    }

    public IEnumerable<int> EnumerateFactoriel(int count)
    {
        int value = 1;
        for (int i = 1; i < count; i++)
        {
            value *= i;
            yield return value;
        }
    }
}