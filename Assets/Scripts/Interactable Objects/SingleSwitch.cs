using UnityEngine;
using System.Collections;

public class SingleSwitch : MonoBehaviour
{
    public int switchID;
    private int m_switchID { get { return switchID; } }
    public ThreeSwitches threeSwitches;
    private bool m_switchedOn = false;
    public GameObject switchPivot;
    private Animator m_anim;
    private AudioSource m_audioSource;
    private KeyCode[] keyCodes = new KeyCode[3] { KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3 };

    void Start()
    {
        m_anim = switchPivot.GetComponent<Animator>();
        m_audioSource = gameObject.GetComponent<AudioSource>();
    }

	void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            if(m_switchID == i + 1)
            {
                if (Input.GetKeyDown(keyCodes[i]))
                    Interact();
            }
        }
    }

    public void Interact()
    {
        m_switchedOn = !m_switchedOn;

        switch (m_switchID)
        {
            case 1:
                threeSwitches.SwitchOne();
                break;
            case 2:
                threeSwitches.SwitchTwo();
                break;
            case 3:
                threeSwitches.SwitchThree();
                break;
        }

        m_anim.SetTrigger(m_switchedOn ? "On" : "Off");
        m_audioSource.Play();
    }
}
