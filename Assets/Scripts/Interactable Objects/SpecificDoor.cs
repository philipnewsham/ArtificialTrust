using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SpecificDoor : MonoBehaviour {
    public GameObject door;
    private GameObject m_door { get { return door; } }
    public bool m_powerOn;
    public bool m_locked;

    private int m_unlockingTime;

    private int m_openingTime;

    private Vector3 m_closePosition;

    private Animator m_animator;
    private AudioSource m_doorAudioSource;

    private GameObject m_ai;
    private DoorController m_doorControllerScript;

    private Text[] m_panelStatus;

    public Material[] materials;
    private Renderer m_renderer;

    private AudioSource m_audioSource;
    public AudioClip[] audioClips;
    public DoorToggleInstantiate doorToggleInstantiateScript;

    private InteractableObject m_interactableObjectScript;
    private int m_doorID;
    private bool m_inAction = false;

    private bool m_unlockedByScientist;
    void Awake()
    {
        m_ai = GameObject.FindGameObjectWithTag("AI");
        m_audioSource = gameObject.GetComponent<AudioSource>();
        m_doorControllerScript = m_ai.GetComponent<DoorController>();
        m_animator = door.GetComponent<Animator>();
        m_doorAudioSource = door.GetComponent<AudioSource>();
        m_renderer = gameObject.GetComponent<Renderer>();
        m_interactableObjectScript = gameObject.GetComponent<InteractableObject>();
        m_doorID = m_interactableObjectScript.interactableID - 10;
        m_unlockingTime = m_doorControllerScript.unlockingTime;
        m_openingTime = m_doorControllerScript.openingTime;
        m_panelStatus = gameObject.GetComponentsInChildren<Text>();
        if (!m_powerOn)
        {
            OpenDoor();
        }
        CheckMaterial();
    }

    public void TutorialDoor(bool isOpening)
    {
        if(isOpening)
        {
            m_animator.SetTrigger("Open");
            //m_renderer.material = materials[1];
        }
        else
        {
            m_animator.SetTrigger("Closed");
            //m_renderer.material = materials[0];
        }
    }



    public void Interact()
    {
        print("Interacted");
        if (m_powerOn && !m_inAction)
        {
            if (m_locked)
            {
                m_unlockedByScientist = true;
                Invoke("OpenDoor", m_unlockingTime);
                m_audioSource.clip = audioClips[0];
                m_audioSource.loop = true;
                m_audioSource.Play();
            }
            else
            {
                Invoke("OpenDoor", m_openingTime);
                m_audioSource.clip = audioClips[2];
                m_audioSource.Play();
            }
            m_inAction = true;
        }
    }

    void UnlockedSound()
    {
        m_audioSource.Stop();
        m_audioSource.loop = false;
        m_audioSource.clip = audioClips[1];
        m_audioSource.Play();
    }
	public Sprite[] lockImages;
	public Color[] backgroundColours;

	public Image[] keypadLockImages;
	public Image[] backgroundColourImage;
	public Text[] lockText;
    void CheckMaterial()
    {
		//print ("should change " + m_locked);
        if (m_locked)
        {
            //m_renderer.material = materials[0];
			for (int i = 0; i < 2; i++) 
			{
				keypadLockImages[i].sprite = lockImages [1];
				backgroundColourImage [i].color = backgroundColours [1];
				lockText[i].text = "Locked";
			}
        }
        else
        {
            //m_renderer.material = materials[1];
			for (int i = 0; i < 2; i++) 
			{
				keypadLockImages [i].sprite = lockImages [0];
				backgroundColourImage [i].color = backgroundColours [0];
				lockText[i].text = "Unlocked";
			}
		}
    }

	void SwitchOff()
	{			
		for (int i = 0; i < 2; i++) 
		{
			keypadLockImages [i].sprite = lockImages[2];
			backgroundColourImage [i].color = new Color(0,0,0,1);
			lockText[i].text = "";
		}

	}

    public void DoorPower(string toggleDoor)
    {
        if (toggleDoor == "Lock")
        {
            m_locked = true;
            if (m_powerOn)
            {
                //m_renderer.material = materials[0];
				CheckMaterial();
            }
        }
        if (toggleDoor == "Unlock")
        {
            m_locked = false;
            if (m_powerOn)
            {
                //m_renderer.material = materials[1];
				CheckMaterial();
            }
        }
        if (toggleDoor == "Off")
        {
            m_powerOn = false;
            OpenDoor();
            //m_renderer.material = materials[2];
			SwitchOff();
			//CheckMaterial();
        }
		if (toggleDoor == "On") 
		{
			m_powerOn = true;
			CloseDoor ();
			CheckMaterial ();
		}
    }

    void OpenDoor()
    {
        m_animator.SetTrigger("Open");
        m_doorAudioSource.Play();
        if (m_powerOn)
        {
            Invoke("CloseDoor", 5f);
        }
        if (m_locked && m_unlockedByScientist)
        {
            m_locked = false;
            //m_renderer.material = materials[1];
			CheckMaterial();
            //doorToggleInstantiateScript.DisabledLock(m_doorID);
            m_unlockedByScientist = false;
        }
        UnlockedSound();
    }

    void CloseDoor()
    {
        m_animator.SetTrigger("Closed");
        m_doorAudioSource.Play();
        m_inAction = false;
    }
}
