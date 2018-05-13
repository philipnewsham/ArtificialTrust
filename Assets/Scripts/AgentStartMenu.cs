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

	void Start ()
    {
        m_isOpen = true;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            OpenClosePanel();

        if (!m_isOpen)
            return;
        
        if (Input.GetButtonDown("ControllerX"))
            SwapInformation();

        if (Input.GetButtonDown("ControllerY"))
            SwapTab();
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
        m_currentTabInt = (m_currentTabInt + 1) % 3;

        for (int i = 0; i < 3; i++)
        {
            tabs[i].color = tabColours[m_currentTabInt == i ? 0 : 1];
            menuPanels[i].SetActive(m_currentTabInt == i);
        }
    }

    void SwapMapObjectives()
    {
        m_isObjectives = !m_isObjectives;
        m_currentTabInt = m_isObjectives ? 0 : 2;
        mapPanel.SetActive(!m_isObjectives);
        objectivePanel.SetActive(m_isObjectives);
        ChangeTabColours();
    }

    void ChangeTabColours()
    {
        for (int i = 0; i < tabs.Length; i++)
            tabs[i].color = tabColours[m_currentTabInt != i ? 1 : 0];
    }
}
