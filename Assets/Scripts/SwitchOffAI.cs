using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SwitchOffAI : MonoBehaviour
{
    private float[] m_leverTimers = new float[3];
    private float[] m_multipliers = new float[3];
    public bool[] subObjectives = new bool[3];
    bool[] m_countingDown = new bool[3];
    public float maxTime;
    private int m_currentLever;
    private bool m_leverOn;
    public Image[] percentImages;
	public Animator[] leverAnimations;
	private bool[] m_isFlippedOn = new bool[3];

    void Start()
    {
        for (int i = 0; i < 3; i++)
            m_multipliers[i] = 1f;
    }

    public void UpdateSubObjectives(bool[] objectivesComplete)
    {
        for (int i = 0; i < 3; i++)
        {
            subObjectives[i] = objectivesComplete[i];
            m_multipliers[i] = subObjectives[i] ? 2.5f : 1.0f;
        }
    }
	
    public void Interacted(int lever)
    {
        m_countingDown[lever] = !m_countingDown[lever];
		m_isFlippedOn [lever] = !m_isFlippedOn [lever];
		leverAnimations [lever].SetTrigger ("Flip");

        if(m_countingDown[lever])
        {
            m_leverOn = true;
            for (int i = 0; i < 3; i++)
            {
                if(i != lever)
                {
                    m_countingDown[i] = false;
					if(m_isFlippedOn[i])
					{
						leverAnimations [i].SetTrigger ("Flip");
						m_isFlippedOn [i] = false;
					}
                }
                else
                    m_currentLever = i;
            }
        }
        else
            m_leverOn = false;
    }
    
    bool[] m_leverComplete = new bool[3];

	void Update ()
    {
        if (!m_leverOn && !(m_countingDown[m_currentLever] && !m_leverComplete[m_currentLever]))
            return;

        m_leverTimers[m_currentLever] += Time.deltaTime * m_multipliers[m_currentLever];
        percentImages[m_currentLever].fillAmount = m_leverTimers[m_currentLever] / maxTime;

        if (m_leverTimers[m_currentLever] >= maxTime)
        {
            m_leverComplete[m_currentLever] = true;
			CheckLevers ();
        }
	}

    public GameObject aiLoseScreen;
	public GameObject aiLoseText;
	public AgentWin agentWinScript;

    void CheckLevers()
    {
        for (int i = 0; i < 3; i++)
        {
            if (!m_leverComplete[i])
                return;
        }

		aiLoseScreen.SetActive (true);
		aiLoseText.SetActive (true);
		agentWinScript.UpdateWin (1);
    }
}
