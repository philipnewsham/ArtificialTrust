using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubObjectiveLights : MonoBehaviour
{
    int m_lightAmount;

    void Start()
    {
        m_lightAmount = Random.Range(0, 8);
    }

    public void CheckObjective(int amount)
    {
    }
}
