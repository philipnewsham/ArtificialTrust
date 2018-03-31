using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PatternGenerator : MonoBehaviour
{
	void Start ()
    {
        CreatePattern();
	}
    private string m_wholePatternFirstPair;
    private string m_wholePatternSecondPair;
    public GameObject completedPanel;

    private string[] m_firstPairs = new string[4] { "A", "T", "C", "G" };
    private string[] m_secondPairs = new string[4] { "T", "A", "G", "C" };
    private string[] m_groupPairsA = new string[3];
    private string[] m_groupPairsB = new string[3];
    private string m_questionMark = "?";
    private int m_currentRandInt;
    public Text[] worldTexts;
    private int m_randomPair;
    public Text[] buttonTexts;
    private Text[] m_hiddenTexts;
    private int[] m_order = new int[3];

    private int m_groupPicked;
    private int m_buttonPressed;
    public Button[] buttons;

    private string[] m_hiddenLetters = new string[3];
    private int m_currentCorrect;

    public GameObject lockOutScreen;

    public Button nextPuzzleButton;
    private DoorController m_doorController;
    private bool[] m_isCorrect = new bool[3];
    void ChooseRandomGroup()
    {
        m_doorController = GetComponent<BinaryDecipher>().doorController;
        int groupOne = Random.Range(0, 1000);
        int groupTwo = Random.Range(0, 1000);
        int groupThree = Random.Range(0, 1000);

        if(groupOne > groupTwo && groupOne > groupThree)
        {
            if(groupTwo > groupThree)
            {
                m_order[0] = 0;
                m_order[1] = 1;
                m_order[2] = 2;
                //1,2,3
            }
            else
            {
                m_order[0] = 0;
                m_order[1] = 2;
                m_order[2] = 1;
                //1,3,2
            }
        }
        if (groupOne < groupTwo && groupOne > groupThree)
        {
            m_order[0] = 1;
            m_order[1] = 0;
            m_order[2] = 2;
            //2,1,3
        }
        if (groupOne > groupTwo && groupOne < groupThree)
        {
            m_order[0] = 2;
            m_order[1] = 0;
            m_order[2] = 1;
            //3,1,2
        }
        if (groupOne < groupTwo && groupOne < groupThree)
        {
            if (groupTwo > groupThree)
            {
                m_order[0] = 1;
                m_order[1] = 2;
                m_order[2] = 0;
                //2,3,1
            }
            else
            {
                m_order[0] = 2;
                m_order[1] = 1;
                m_order[2] = 0;
                //3,2,1
            }
        }

    }

    void CreatePattern()
    {
        ChooseRandomGroup();
        for (int i = 0; i < 3; i++)
        {
            m_randomPair = Random.Range(0, 10);
            for (int j = 0; j < 10; j++)
            {
                m_currentRandInt = Random.Range(0, 4);
                if (j != m_randomPair)
                {
                    
                    m_groupPairsA[i] += string.Format(" {0}",m_firstPairs[m_currentRandInt]);
                    m_groupPairsB[i] += string.Format(" {0}",m_secondPairs[m_currentRandInt]);
                }
                else
                {
                    m_groupPairsA[i] += " ?";
                    m_groupPairsB[i] += " ?";
                    m_hiddenLetters[i] = m_firstPairs[m_currentRandInt];
                }
                m_wholePatternFirstPair += string.Format(" {0}", m_firstPairs[m_currentRandInt]);
                m_wholePatternSecondPair += string.Format(" {0}", m_secondPairs[m_currentRandInt]);
            }
        }
        worldTexts[0].text = m_wholePatternFirstPair;
        worldTexts[1].text = m_wholePatternSecondPair;

        buttonTexts[0].text = m_groupPairsA[m_order[0]];
        buttonTexts[1].text = m_groupPairsB[m_order[0]];
        buttonTexts[2].text = m_groupPairsA[m_order[1]];
        buttonTexts[3].text = m_groupPairsB[m_order[1]];
        buttonTexts[4].text = m_groupPairsA[m_order[2]];
        buttonTexts[5].text = m_groupPairsB[m_order[2]];
    }
    public Button[] pairButtons;
    public Color32[] colours;
    public void ClickedGroup(int buttonNo)
    {
        m_buttonPressed = buttonNo;
        m_groupPicked = m_order[buttonNo];
        /*
        for (int i = 0; i < 3; i++)
        {
            if(i == buttonNo)
            {
                buttons[i].GetComponent<Image>().color = colours[0];
            }
            else
            {
                if(m_isCorrect[i])
                    buttons[i].GetComponent<Image>().color = colours[1];
                else
                    buttons[i].GetComponent<Image>().color = colours[2];
            }
        }
        */
        
        for (int i = 0; i < 4; i++)
        {
            pairButtons[i].interactable = true;
        }
    }
    public ChallengeLockOut challengeLockOutScript;
    public void ClickedPair(string letter)
    {
        if (letter == m_hiddenLetters[m_groupPicked])
        {
            Correct();
        }
        else
        {
            print("Incorrect");
            challengeLockOutScript.BeginCountdown(10f);
        }

        for (int i = 0; i < 4; i++)
        {
            pairButtons[i].interactable = false;
        }
    }
    public GameObject nextWayfinder;
    public ChangeTextColour changeTextColourScript;
    void Correct()
    {
        buttons[m_buttonPressed].interactable = false;
        buttons[m_buttonPressed].GetComponent<TextHoverTest>().enabled = false;

        m_isCorrect[m_buttonPressed] = true;
        m_currentCorrect += 1;
        
        if (m_currentCorrect == 3)
        {
            completedPanel.SetActive(true);
            nextPuzzleButton.interactable = true;
            m_doorController.TutorialOpenDoors(7, true);
            m_doorController.TutorialOpenDoors(6, true);
            nextWayfinder.SetActive(true);
			m_isEnabled = false;
            changeTextColourScript.AnimationTrigger();
        }
    }
    string newStringA;
    string newStringB;
    void ChangeQuestionPairs()
    {
        char[] individualLettersA = m_groupPairsA[m_buttonPressed].ToCharArray();
        char[] individualLettersB = m_groupPairsB[m_buttonPressed].ToCharArray();
        //string newStringA;
        //string newStringB;
        for (int i = 0; i < individualLettersA.Length; i++)
        {
            if (individualLettersA[i].ToString() != "?")
            {
                //newStringA += string.Format(" ");
                //newStringB += string.Format(" ");
                newStringA += string.Format("{0}", individualLettersA[i].ToString());
                newStringB += string.Format("{0}", individualLettersB[i].ToString());
            }
            else
            {
                newStringA += string.Format(" {0}", m_hiddenLetters[m_buttonPressed]);
                if (m_hiddenLetters[m_buttonPressed] == "A")
                newStringB += string.Format(" {0}", "T");
                else if (m_hiddenLetters[m_buttonPressed] == "T")
                    newStringB += string.Format(" {0}", "A");
                else if (m_hiddenLetters[m_buttonPressed] == "C")
                    newStringB += string.Format(" {0}", "G");
                else if (m_hiddenLetters[m_buttonPressed] == "G")
                    newStringB += string.Format(" {0}", "C");
            }
            if(m_buttonPressed == 0)
            {
                buttonTexts[0].text = newStringA;
                buttonTexts[1].text = newStringB;
            }
            else if(m_buttonPressed == 1)
            {
                buttonTexts[2].text = newStringA;
                buttonTexts[3].text = newStringB;
            }else if (m_buttonPressed == 2)
            {
                buttonTexts[4].text = newStringA;
                buttonTexts[5].text = newStringB;
            }
        }
    }

    void Unlocked()
    {
        lockOutScreen.SetActive(false);
    }

	bool m_isEnabled;
	public Text timerText;
	private float m_countdown = 120f;
	public void Countdown()
	{
		m_isEnabled = !m_isEnabled;
	}

	void Update()
	{
		if (m_isEnabled) 
		{
			m_countdown -= Time.deltaTime;
			timerText.text = string.Format("{0}:{1}", Mathf.Floor(m_countdown/ 60f),Mathf.Floor(m_countdown % 60f));
			if (m_countdown <= 0f) {
				m_isEnabled = false;
				timerText.text = "<color=red>00:00</color>";
			}
		}
	}

}
