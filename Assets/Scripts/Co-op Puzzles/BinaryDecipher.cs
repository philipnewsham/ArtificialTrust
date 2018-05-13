using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BinaryDecipher : MonoBehaviour 
{
    public GameObject completedPanel;
	private string[] m_binaryAlphabet = new string[27] 
	{
		"0110 0001", //a
		"0110 0010", //b
		"0110 0011", //c
		"0110 0100", //d
		"0110 0101", //e
		"0110 0110", //f
		"0110 0111", //g
		"0110 1000", //h
		"0110 1001", //i
		"0110 1010", //j
		"0110 1011", //k
		"0110 1100", //l
		"0110 1101", //m
		"0110 1110", //n
		"0110 1111", //o
		"0111 0000", //p
		"0111 0001", //q
		"0111 0010", //r
		"0111 0011", //s
		"0111 0100", //t
		"0111 0101", //u
		"0111 0110", //v
		"0111 0111", //w
		"0111 1000", //x
		"0111 1001", //y
		"0111 1010", //z
        "0010 0000"  // 
	};

	private string[] m_alphabet = new string[27] 
	{
		"a", 
		"b", 
		"c",
		"d",
		"e",
		"f",
		"g",
		"h",
		"i",
		"j",
		"k",
		"l",
		"m",
		"n",
		"o",
		"p",
		"q",
		"r",
		"s",
		"t",
		"u",
		"v",
		"w",
		"x",
		"y",
		"z",
        " "
	};

    public Button[] buttons;
	private Button[] backupButtons = new Button[5];
	public GameObject[] letterCubes;
	private int m_currentLetter;
	public Material[] letterCubeMaterials;
    public GameObject lockOutPanel;
    public GameObject letterButton;
    public GameObject wordPanel;

    public List<string> possibleWord = new List<string>()
    {
        "apple",
        "march",
        "mouse",
        "paint",
		"perch",
        "goals",
        "human",
        "write",
        "quick",
        "house",
        "while"
    };

	public Animator cursorAnim;
	public Text timerText;
    public ChangeTextColour changeTextColourScript;

    void Start()
    {
        CreateWord();
    }

    void CreateWord()
    {
        //get random word
        string currentWord = possibleWord[Random.Range(0, possibleWord.Count)];
        List<string> binaryWord = GetComponent<ConvertToBinary>().ConvertWord(currentWord);

        for (int i = 0; i < binaryWord.Count; i++)
        {
            GameObject currentButton = Instantiate(letterButton, wordPanel.transform);
            currentButton.GetComponentInChildren<Text>().text = binaryWord[i];
            backupButtons[i] = currentButton.GetComponent<Button>();
        }
    }

	public Text currentBinaryText;

	public void ClickedButton(string thisText)
	{
		currentBinaryText.text = thisText;
		//check which letter it is (i.e 0110 0001 == a)
		for (int i = 0; i < m_binaryAlphabet.Length; i++) 
		{
			if (thisText == m_binaryAlphabet [i])
				m_currentLetter = i;
		}

		//change materials to light up current letter
		for (int i = 0; i < letterCubes.Length; i++)
            letterCubes[i].GetComponent<Renderer>().material = letterCubeMaterials[m_currentLetter == i ? 1 : 0];
	}

	bool m_isEnabled;
	float m_countdown = 180f;

	public void Countdown()
	{
		m_isEnabled = !m_isEnabled;
	}

	void Update()
	{
		if (Input.anyKeyDown)
			CheckKeyPress(Input.inputString);

        if(Input.GetKeyDown(KeyCode.Return))
            CheckLetter();

        if (!m_isEnabled)
            return;
        
		m_countdown -= Time.deltaTime;
		timerText.text = string.Format("{0}:{1}", Mathf.Floor(m_countdown/ 60f),Mathf.Floor(m_countdown % 60f));

		if (m_countdown <= 0f)
        {
			m_isEnabled = false;
			timerText.text = "<color=red>00:00</color>";
		}
	}

    public Text showLetterText;
    private bool m_correctLetter;

	void CheckKeyPress(string currentKey)
	{
		for (int i = 0; i < m_alphabet.Length; i++) 
		{
			if(Input.inputString == m_alphabet[i])
			{
                showLetterText.text = m_alphabet[i];
				cursorAnim.SetBool ("LetterTyped", true);
                m_correctLetter = (Input.inputString == m_alphabet[m_currentLetter]);
			}
		}
	}

    public void CheckLetter()
    {
        if(m_correctLetter)
            KeyCorrect();
        else
            KeyWrong();

        showLetterText.text = "";
		cursorAnim.SetBool ("LetterTyped", false);
    }

    int m_lettersCorrect = 0;
    public Text[] sideButtons;

	void KeyCorrect()
	{
        for (int i = 0; i < backupButtons.Length; i++)
        {
            if(backupButtons[i].GetComponentInChildren<Text>().text == m_binaryAlphabet[m_currentLetter])
            {
                backupButtons[i].GetComponentInChildren<Text>().text = m_alphabet[m_currentLetter];
                backupButtons[i].interactable = false;
            }
        }

        for (int i = 0; i < sideButtons.Length; i++)
        {
            if(sideButtons[i].text == m_binaryAlphabet[m_currentLetter])
            {
                sideButtons[i].text = m_alphabet[m_currentLetter];
				sideButtons[i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (10, 30);
            }
        }
        
		for (int i = 0; i < 5; i++) 
		{
            if (backupButtons[i].interactable == false)
                return;
		}

		UnlockNextPuzzle();
		StartCoroutine("CheckingRemainingLetters");
	}

    public DoorController doorController;
    public Button nextPuzzleButton;
    public GameObject nextWayfinder;

    private bool isCompleted = false;

    void UnlockNextPuzzle()
    {
		m_isEnabled = false;
        isCompleted = true;
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }

	void CheckRemainingLetters()
	{
		for (int j = 0; j < m_binaryAlphabet.Length; j++) 
		{
			for (int i = 0; i < sideButtons.Length; i++) 
			{
				if (sideButtons [i].text == m_binaryAlphabet [j])
                {
					sideButtons [i].text = m_alphabet [j];
					sideButtons [i].GetComponent<RectTransform> ().sizeDelta = new Vector2 (10, 30);
				}
			}
		}
	}

	IEnumerator CheckingRemainingLetters()
	{
		int i = 0;
		while (i < m_binaryAlphabet.Length)
        {
			for (int j = 0; j < sideButtons.Length; j++) 
			{
				if (sideButtons [j].text == m_binaryAlphabet [i])
                {
					sideButtons [j].text = m_alphabet [i];
					sideButtons [j].GetComponent<RectTransform> ().sizeDelta = new Vector2 (10, 30);
				}
			}
			i += 1;
			yield return new WaitForSeconds (0.2f);
		}
	}

    public ChallengeLockOut challengeLockOutScript;

	void KeyWrong()
	{
        challengeLockOutScript.BeginCountdown(5f);
	}

    void LockOutDisabled()
    {
        lockOutPanel.SetActive(false);
    }
}

