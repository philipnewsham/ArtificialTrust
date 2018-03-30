using UnityEngine;
using System.Collections;

public class RandomOccurances : MonoBehaviour
{
    private int m_randomTime;
    private int m_randomTimeTwo;
    private float m_countingUp;

    private bool m_thingOne;
    private bool m_thingTwo;

	void Start ()
    {
        m_randomTime = Random.Range(60, 300);
        m_randomTimeTwo = Random.Range(300, 600);
	}
	
	void Update ()
    {
        m_countingUp += Time.deltaTime;
        if(m_countingUp >= m_randomTime && !m_thingOne)
            m_thingOne = true;
        if(m_countingUp >= m_randomTimeTwo && !m_thingTwo)
            m_thingTwo = true;
	}
}
