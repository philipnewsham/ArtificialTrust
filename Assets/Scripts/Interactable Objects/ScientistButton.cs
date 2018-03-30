using UnityEngine;
using System.Collections;

public class ScientistButton : MonoBehaviour
{
    private int m_randomiseButton;
    //how many outcomes pressing the button has
    private int m_maxButtonFunctions;

    void Start()
    {
        m_randomiseButton = Random.Range(0, m_maxButtonFunctions);
    }
}
