using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChallengeLockOut : MonoBehaviour
{
    public GameObject lockOutScreen;
    public Text timeText;
    private float m_countdown;
    private bool m_isCountingDown;
	// Use this for initialization
	public void BeginCountdown (float time)
    {
        m_countdown = time;
        lockOutScreen.SetActive(true);
        m_isCountingDown = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(m_isCountingDown)
        {
            m_countdown -= Time.deltaTime;
            timeText.text = string.Format("{0}", Mathf.FloorToInt(m_countdown));
            if(m_countdown <= 0)
            {
                m_isCountingDown = false;
                lockOutScreen.SetActive(false);
            }
        }
	}
}
