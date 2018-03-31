using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SafeLocks : MonoBehaviour
{
    private int m_lockLevel = 0;
    private bool m_dPadPressed;
    public GameObject[] lockPanels;

    public Image starsignImage;
    public Sprite[] starsigns;
    private string[] starsignNames = new string[12] { "Aries", "Taurus", "Gemini", "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius", "Capricorn", "Aquarius", "Pisces" };
    private int m_currentStarsign = 0;
    private bool m_isActive = false;
    public string[] passwords = new string[4];
    public Text[] passwordText;
    public string actualPassword;
    public InputField passwordField;

    private bool m_passwordLock = false;
    private bool m_sequenceLock = false;
    private bool m_starsignLock = false;
    private bool m_allLocks = false;

    private int m_correctStarSign;

    public int[] sequenceOrder = new int[4];
    private int m_pressOrder;

    public GameObject safeCanvas;

    public GameObject safe;
    private Animator m_safeAnim;
    private AudioSource m_safeAS;
    public AudioClip[] audioClips;

    private bool m_lockedOut;
    public GameObject lockedOutPanel;
    public Text lockedOutText;
    private float m_countingDown;
    public float lockedOutTime;
    private float m_lockedOutTime;

    public GameObject ai;
    private HackingDocuments m_hackingDocuments;

    public GameObject scientist;
    private ScientistWin m_scientistWinScript;

    public GameObject gameController;
    private FreezeControls m_freezeControls;

    public Text locksText;
    public Button unlockButton;

    private int m_locksUnlocked = 0;

    public Image lockOneLight;
    public Image lockTwoLight;
    public Image lockThreeLight;
    public Sprite[] lockLights; //0 = off, 1 = on
    //bool should stop player automatically choosing A option
    private bool m_justPressed;

    public ScientistComputer scientistComputerScript;
    public Button[] documentButtons;

    void Start ()
    {
        GetReferences();
        UpdatePanels();

        m_lockedOutTime = lockedOutTime;
        m_countingDown = m_lockedOutTime;

        locksText.text = string.Format("({0}/3) Locks Unlocked", m_locksUnlocked);

        GenerateStarSign();

        safeCanvas.SetActive(false);
	}

    void GetReferences()
    {
        m_scientistWinScript = scientist.GetComponent<ScientistWin>();
        m_safeAS = safe.GetComponent<AudioSource>();
        m_freezeControls = gameController.GetComponent<FreezeControls>();
        m_hackingDocuments = ai.GetComponent<HackingDocuments>();
        m_safeAnim = safe.GetComponent<Animator>();
    }

    void GenerateStarSign()
    {
        m_correctStarSign = Random.Range(0, 12);
        starsignImage.sprite = starsigns[m_currentStarsign];
        string starsignMessage = string.Format("////CONFIDENTIAL////\n---For Authorised Personnel Only---\nThe star sign that unlocks the safe is {0}", starsignNames[m_correctStarSign]);
        documentButtons[0].GetComponent<DocumentButton>().documentText = string.Format("The star sign that unlocks the safe is {0}", starsignNames[m_correctStarSign]);
        scientistComputerScript.ReceiveStarsign(starsignMessage);

        m_hackingDocuments.RecieveDocumentMessages(starsignMessage, 4);
    }

    IEnumerator LockedOut()
    {
        PlaySound(2);
        m_lockedOut = true;
        lockedOutPanel.SetActive(true);

        while (m_countingDown > 0.0f)
        {
            m_countingDown -= 1 * Time.deltaTime;
            lockedOutText.text = string.Format("PLEASE WAIT {0} SECONDS BEFORE TRYING AGAIN", Mathf.FloorToInt(m_countingDown));
            yield return null;
        }

        lockedOutPanel.SetActive(false);
        m_lockedOut = false;
        m_countingDown = m_lockedOutTime;
    }
    
    void Update()
    {

        if (!m_isActive)
            return;

        if (Input.GetButtonDown("ControllerBack"))
            LeaveSafe();

        if (m_lockedOut)
            return;

        if (!m_dPadPressed)
        {
            m_dPadPressed = (Input.GetAxisRaw("DpadY") != 0);

            if (Input.GetAxisRaw("DpadY") > 0)
                MenuNavigation(true);

            if (Input.GetAxisRaw("DpadY") < 0)
                MenuNavigation(false);
        }

        if ((Input.GetAxisRaw("DpadY") == 0) && (Input.GetAxisRaw("DpadX") == 0) && (m_dPadPressed))
            m_dPadPressed = false;

        if (Input.GetButtonDown("ControllerA"))
        {
            if (m_justPressed)
                GetButtonA();
            else
                m_justPressed = true;
        }

        if (Input.GetButtonDown("ControllerB"))
            GetButtonB();

        if (Input.GetButtonDown("ControllerX"))
            GetButtonX();

        if (Input.GetButtonDown("ControllerY"))
            GetButtonY();
    }

    public void UpdatePasswords()
    {
        for (int i = 0; i < 4; i++)
            passwordText[i].text = passwords[i];
    }

    void GetButtonA()
    {
        switch (m_lockLevel)
        {
            case 0:
                if (m_allLocks)
                {
                    Invoke("OpenSafe", 0.5f);
                    PlaySound(1);
                }
                else
                    PlaySound(0);
                break;
            case 1:
                ButtonSequence(4);
                break;
            case 2:
                CheckPasswords(passwords[2]);
                break;
            case 3:
                if (!m_starsignLock)
                    ChangeStarSign(-1);
                break;
        }
    }

    void GetButtonB()
    {
        switch (m_lockLevel)
        {
            case 1:
                ButtonSequence(3);
                break;
            case 2:
                CheckPasswords(passwords[1]);
                break;
            case 3:
                if (!m_starsignLock)
                    CheckStarsign();
                break;
        }
    }

    void GetButtonX()
    {
        switch (m_lockLevel)
        {
            case 1:
                ButtonSequence(1);
                break;
            case 2:
                CheckPasswords(passwords[3]);
                break;
        }
    }

    void GetButtonY()
    {
        switch (m_lockLevel)
        {
            case 1:
                ButtonSequence(2);
                break;
            case 2:
                CheckPasswords(passwords[0]);
                break;
            case 3:
                if (!m_starsignLock)
                    ChangeStarSign(1);
                break;
        }
    }

    public void Interact()
    {
        if (!m_isActive)
        {
            m_isActive = true;
            safeCanvas.SetActive(true);
            m_freezeControls.FirstPersonControllerEnabled(false);
        }
    }

    void LeaveSafe()
    {
        safeCanvas.SetActive(false);
        m_justPressed = false;
        m_isActive = false;
        m_freezeControls.FirstPersonControllerEnabled(true);
    }

    void MenuNavigation(bool moveUp)
    {
        if (!moveUp)
        {
            if (m_lockLevel < lockPanels.Length - 1)
            {
                m_lockLevel += 1;
                PlaySound(6);
            }
        }
        else
        {
            if(m_lockLevel > 0)
            {
                m_lockLevel -= 1;
                PlaySound(6);
            }
        }
        UpdatePanels();
    }

    void UpdatePanels()
    {
        for (int i = 0; i < lockPanels.Length; i++)
            lockPanels[i].SetActive(i == m_lockLevel);
    }

    void ChangeStarSign(int changeDirection)
    {
        m_currentStarsign += changeDirection;

        if(m_currentStarsign > starsigns.Length - 1)
            m_currentStarsign = 0;

        if(m_currentStarsign < 0)
            m_currentStarsign = starsigns.Length - 1;

        starsignImage.sprite = starsigns[m_currentStarsign];
        PlaySound(5);
    }

    void CheckStarsign()
    {
        if (!m_starsignLock)
        {
            if (m_currentStarsign == m_correctStarSign)
            {
                m_starsignLock = true;
                lockThreeLight.sprite = lockLights[1];
                CheckLocks();
            }
            else
                StartCoroutine(LockedOut());
        }
    }

    void CheckPasswords(string password)
    {
        if (!m_passwordLock)
        {
            if (password == actualPassword)
            {
                passwordField.text = actualPassword;
                m_passwordLock = true;
                lockOneLight.sprite = lockLights[1];
                CheckLocks();
            }
            else
                StartCoroutine(LockedOut());
        }
    }

    void CheckLocks()
    {
        PlaySound(4);
        m_locksUnlocked += 1;
        m_allLocks = m_passwordLock && m_sequenceLock && m_starsignLock;
        locksText.text = string.Format("({0}/3) Locks Unlocked", m_locksUnlocked);
    }

    void ButtonSequence(int currentButton)
    {
        if (!m_sequenceLock)
        {
            m_pressOrder = sequenceOrder[m_pressOrder] == currentButton ? m_pressOrder + 1 : 0;

            if (sequenceOrder[m_pressOrder] == currentButton)
            {
                PlaySequenceSound();

                if (m_pressOrder == sequenceOrder.Length)
                {
                    m_sequenceLock = true;
                    lockTwoLight.sprite = lockLights[1];
                    CheckLocks();
                }
            }
            else
                StartCoroutine(LockedOut());
        }
    }

    void OpenSafe()
    {
        PlaySound(7);
        m_safeAnim.SetTrigger("Unlocked");
		GetComponent<Safe>().EmptySafe();
        LeaveSafe();
    }

    void PlaySequenceSound()
    {
        m_safeAS.pitch = (1 + (.1f * m_pressOrder));
        PlaySound(3);
        m_safeAS.pitch = 1;
    }

    void PlaySound(int soundNo)
    {
        m_safeAS.clip = audioClips[soundNo];
        m_safeAS.Play();
    }
}
