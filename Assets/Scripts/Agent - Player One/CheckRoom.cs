using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckRoom : MonoBehaviour
{
    private float[] m_roomTime = new float[9];
    public Text objectiveText;
    private int m_currentObjectiveInt;
    
	public Sprite wayPoint;

	private AgentObjectiveText m_agentObjectiveTextScript;
    public DoorController doorControllerScript;
    private string[] m_objectives = new string[6]
    {
        "Follow the waypoints to the Main Laboratory",
        "Work with the AI to solve the Binary Puzzle",
        "Follow the waypoints to the Small Office",
        "Work with the AI to solve the Pattern Puzzle",
        "Follow the waypoints to the Server Room",
        "Work with the AI to solve the Geometry Puzzle"
    };

	void Start ()
    {
		m_agentObjectiveTextScript = GetComponent<AgentObjectiveText> ();
        objectiveText.text = string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
        StartCoroutine(TutorialWaypoints());
    }
    /*
    intro:
    mission one: go to main lab - ontriggerenter
    mission two: help AI - binarypuzzlecomplete
    mission three: go to office - ontriggerenter
    mission four: help AI - patternpuzzlecomplete
    mission five: go to server - ontriggerenter
    mission six: help AI - geometrypuzzlecomplete

    main:
    Unlock the safe
        1. Go to dr. Kirkoff's office
        2. Find out the password
            2a. Find out the password by asking the AI
            2b. Find out the password in the small office
        3. Find out the shape sequence
            3a. Find out the sequence by asking the AI
            3b. Find out the sequence in the small office
        4. Find out the starsign
            4a. Find out the starsign by asking the AI
            4b. Find out the starsign in the small office
        5. Unlock the Safe
        6. Go to the elevator
    */
    
    int m_roomNo = 1;
    bool m_checkingWait;
    bool m_checkingWaitAI;

	void OnTriggerEnter (Collider other)    
    {
	    if(other.gameObject.tag == "Room")
        {
            m_checkingWait = (m_roomNo == m_waitRoom);
            m_checkingWaitAI = (m_roomNo == m_waitRoomAI);
        }
	}

    private int m_waitRoom;
    private float m_waitTime;

    public void WaitObjective(int roomNo, float time)
    {
        m_waitRoom = roomNo;
        m_waitTime = time;
    }

    private int m_waitRoomAI;
    private float m_waitTimeAI;

    public void WaitObjectiveAI(int roomNo, float time)
    {
        m_waitRoomAI = roomNo;
        m_waitTimeAI = time;
    }

    bool m_objectiveComplete;
    bool m_objectiveCompleteAI;
    public AIObjectives aiObjectiveScript;

    void Update()
    {
        m_roomTime[m_roomNo] += Time.deltaTime;

        if(m_checkingWait && m_roomTime[m_roomNo] >= m_waitTime && !m_objectiveComplete)
        {
            m_objectiveComplete = true;
			GetComponent<AgentObjectives>().WaitInRoomObjective(0,true);
        }

        if(m_checkingWaitAI && m_roomTime[m_roomNo] >= m_waitTimeAI && !m_objectiveCompleteAI)
        {
            m_objectiveCompleteAI = true;
            aiObjectiveScript.WaitInRoomObjective(0, true);
        }
    }

    public void UpdateObjectiveText()
    {
		m_agentObjectiveTextScript.CompletedTask ();
        m_currentObjectiveInt += 1;
        objectiveText.text = m_currentObjectiveInt >= m_objectives.Length ? "" : string.Format("Current Objective: {0}", m_objectives[m_currentObjectiveInt]);
    }

    IEnumerator TutorialWaypoints()
    {
        CheckCurrentRoom checkRoom = FindObjectOfType<CheckCurrentRoom>();
        yield return new WaitUntil(() => checkRoom.GetCurrentRoomNo() == 0);
        UpdateObjectiveText();
        doorControllerScript.TutorialOpenDoors(1, false);
        yield return new WaitUntil(() => m_currentObjectiveInt == 2);
        yield return new WaitUntil(() => checkRoom.GetCurrentRoomNo() == 1);
        UpdateObjectiveText();
        doorControllerScript.TutorialOpenDoors(0, false);
        doorControllerScript.TutorialOpenDoors(7, false);
        yield return new WaitUntil(() => m_currentObjectiveInt == 4);
        yield return new WaitUntil(() => checkRoom.GetCurrentRoomNo() == 2);
        UpdateObjectiveText();
        doorControllerScript.TutorialOpenDoors(7, false);
        doorControllerScript.TutorialOpenDoors(6, false);
    }
}
