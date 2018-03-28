using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class LightController : MonoBehaviour {
    public Light[] lights;
    private bool[] m_lightOn;
    private AIPower m_aiPowerScript;
    private int m_lightPower;

	void Start ()
    {
        m_aiPowerScript = gameObject.GetComponent<AIPower>();
        m_lightPower = m_aiPowerScript.lightPower;
        m_lightOn = new bool[lights.Length];
        for (int i = 0; i < lights.Length; i++)
        {
            m_lightOn[i] = true;
        }
	}

    public void CurrentLightPower(int newPower)
    {
        m_lightPower += newPower;
    }

    public void LightSwitch(int lightNo)
    {
        print("light switched on");
        if(m_lightOn[lightNo] == true)
        {
            //lights[lightNo].intensity = 0f;
            lights[lightNo].enabled = false;
            m_aiPowerScript.PowerExchange(m_lightPower);
            m_lightOn[lightNo] = !m_lightOn[lightNo];
        }
        else
        {
            if(m_aiPowerScript.CheckPower(m_lightPower) == true)
            {
                //lights[lightNo].intensity = 3f;
                lights[lightNo].enabled = true;
                m_aiPowerScript.PowerExchange(-m_lightPower);
                m_lightOn[lightNo] = !m_lightOn[lightNo];
            }
            else
            {
                //no power
            }
        }
    }

}
