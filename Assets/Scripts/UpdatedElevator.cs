using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatedElevator : MonoBehaviour
{
    public AgentWin agentWinScript;
    public AIWin aiWinScript;

    public bool agentEscaped;
    public bool aiEscaped;

    public Timer timerScript;

    public GameObject aiEnd;
    public GameObject aiLoses;
    public GameObject aiWins;

    public GameObject agentEnd;
    public GameObject agentWins;
    public GameObject agentLoses;

    private Animator m_animator;

    public AudioClip doorOpeningSFX;
    private AudioSource m_audioSource;

    void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_audioSource = GetComponent<AudioSource>();
        Invoke("OpenDoor", 8f);
    }

    void OpenDoor()
    {
        m_animator.SetTrigger("Open");
        m_audioSource.clip = doorOpeningSFX;
        m_audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Agent")
        {
            print("man entered");
            if (agentWinScript.completedMission == true)
            {
                if (agentWinScript.mainObjective == 0)
                {
                    if (aiWinScript.mainObjective == 0)
                    {
                        if (aiEscaped)
                        {
                            //end
                            //agentEnd.SetActive(true);
                            //agentWins.SetActive(true);
                        }
                        else
                        {
                            timerScript.ChangeMultiplier(1.5f);
                        }
                        agentEnd.SetActive(true);
                        agentWins.SetActive(true);
                    }
                    else 
                    {
                        //AI shuts down
                        aiEnd.SetActive(true);
                        aiLoses.SetActive(true);
                        agentEnd.SetActive(true);
                        agentWins.SetActive(true);
                    }
                }
                else
                {
                    //win
                    agentEnd.SetActive(true);
                    agentWins.SetActive(true);
                }
                m_animator.SetTrigger("Close");
            }
        }

        if(other.gameObject.tag == "AI")
        {
			print ("ai entered elevator");
            if (aiWinScript.completedMission == true)
            {
                aiEnd.SetActive(true);
                aiWins.SetActive(true);
                if(agentWinScript.mainObjective == 0 && !agentEscaped)
                {
					agentEnd.SetActive(true);
					agentLoses.SetActive(true);
					aiEnd.SetActive(true);
					aiWins.SetActive(true);
                }
                else if(agentWinScript.mainObjective == 1)
                {
                    //agent failed
                    agentEnd.SetActive(true);
                    agentLoses.SetActive(true);
					aiEnd.SetActive(true);
					aiWins.SetActive(true);
                }
            }
        }
    }
}
