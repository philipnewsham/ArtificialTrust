using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AgentStartMenu : MonoBehaviour
{
    bool m_isOpen;
    public GameObject startMenu;
    public GameObject objectivePanel;
    public GameObject mapPanel;
    public GameObject[] menuPanels;
    private bool m_isObjectives = true;
    public ChoosingMainObjectives choosingMainObjectivesScript;
    public Image[] tabs;
    public Color32[] tabColours;
    private int m_currentTabInt;
	// Use this for initialization
	void Start ()
    {
        m_isOpen = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            OpenClosePanel();
        }
        if (m_isOpen)
        {
            if (Input.GetButtonDown("ControllerX"))
            {
                SwapInformation();
            }

            if (Input.GetButtonDown("ControllerY"))
            {
                //SwapMapObjectives();
                SwapTab();
            }
        }
    }

    void OpenClosePanel()
    {
        m_isOpen = !m_isOpen;
        startMenu.SetActive(m_isOpen);
    }

    void SwapInformation()
    {
        choosingMainObjectivesScript.AgentSwitchText();
    }

    void SwapTab()
    {
        m_currentTabInt += 1;
        if (m_currentTabInt == 3)
            m_currentTabInt = 0;

        for (int i = 0; i < 3; i++)
        {
            if(m_currentTabInt == i)
            {
                tabs[i].color = tabColours[0];
                menuPanels[i].SetActive(true);
            }
            else
            {
                tabs[i].color = tabColours[1];
                menuPanels[i].SetActive(false);
            }
        }
    }

    void SwapMapObjectives()
    {
        m_isObjectives = !m_isObjectives;

        if (m_isObjectives)
            m_currentTabInt = 0;
        else
            m_currentTabInt = 2;

        mapPanel.SetActive(!m_isObjectives);
        objectivePanel.SetActive(m_isObjectives);
        ChangeTabColours();
    }

    void ChangeTabColours()
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if(m_currentTabInt != i)
            {
                tabs[i].color = tabColours[1];
            }else
            {
                tabs[i].color = tabColours[0];
            }
        }
    }
}
