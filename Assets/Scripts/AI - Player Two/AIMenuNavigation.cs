using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class AIMenuNavigation : MonoBehaviour
{
    public GameObject[] panels;
    public Button[] panelButtons;
    private TextHoverTest[] m_textHoverTextScripts;
    private int m_currentPanelNo;
	public GameObject backgroundPanel;
    void Start()
    {
        m_textHoverTextScripts = new TextHoverTest[panelButtons.Length];
        for (int i = 0; i < panelButtons.Length; i++)
        {
            m_textHoverTextScripts[i] = panelButtons[i].GetComponent<TextHoverTest>();
        }

        m_textHoverTextScripts[3].HoverOver(true);
        ChangePanel(3);
        HoverText(6);
    }

    public void ChangePanel(int panelNo)
    {
        m_currentPanelNo = panelNo;
		if (m_currentPanelNo != 4)
			backgroundPanel.SetActive (true);
		
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == panelNo)
            {
                panels[i].SetActive(true);
                panelButtons[i].interactable = false;
                m_textHoverTextScripts[i].AllowHovering(false);
            }
            else
            {
				if (i != 0) 
				{
					panels [i].SetActive (false);
				}
					panelButtons [i].interactable = true;
					m_textHoverTextScripts [i].AllowHovering (true);
            }
        }
    }

    public Text hoverText;
    private string[] m_hoverTextString = new string[8]
    {
        "Open map to control the power",
        "Open document page to gather information",
        "Open robotic locks to gain access to robot body",
        "Open to find out your current objective",
        "Open cameras to find out where the agent is",
        "Send a message directly to the agent",
        "",
        "Error. Unknown what will happen when button is pressed"
    };

    private string[] m_panelTextString = new string[7]
    {
		string.Format("\\Map:\nClick on buttons in map to switch on/off.\n@ = Light On, -@- = Light Off\n|x| = Door Locked, |u| = Door Unlocked, | | = Door Open\n∞< = Camera On, -∞<- Camera Off\nUse power (located in the top right of the screen) to download information faster."),
       string.Format("\\Documents:\nClick on unlocked documents to begin hacking.\nDifferent documents will take different amounts of time to unlock.\nYou can increase the speed in which a document downloads by how much power you have."),
       string.Format("\\Unlock Robot Body:\nThis is where you can unlock the robot body.\nIt consists of three locks:\nPower: Temporarily use all of your current power to power up the robot, the more power you have the quicker it will go.\nSwitches: In the server room there are three switches, either get the agent to put them in the right positions or find a way to flip them yourself.\nPassword: If your goal is to unlock the robot body, you can either get to agent to find the password (the location of the password can be downloaded in the documents panel) or by completing the subobjectives you can find the letters that form the password.\nPress the Enter button when unlocked!"),
       string.Format("\\Objective:\nThese are your current objectives.\nYou have to complete the main objective.\nCompleting the subobjectives will help you in completing you main objective."),
       string.Format("\\Camera:\nClick the < or > buttons to cycle through the cameras."),
       string.Format("\\Message:\nWrite a message for the other player to read and press send.\nYou can use this to leave orders, clues, or even threats."),
       string.Format("\\Unknown:\nERROR@~%^INV*L)D_SYST&M£OVER£@DE")
    };

    public void HoverText(int panelNo)
    {
        hoverText.text = m_hoverTextString[panelNo];
        if(panelNo == 6)
        {
            hoverText.text = m_panelTextString[m_currentPanelNo];
        }
    }
}
