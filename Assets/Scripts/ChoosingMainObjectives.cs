using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoosingMainObjectives : MonoBehaviour
{
    private int m_aiObjective;
    private int m_agentObjective;

    private string[] m_aiObjectivesFinal = new string[2];
    private string[] m_agentObjectivesFinal = new string[2];
    //Two main objectives
    private string[] m_aiObjectiveString = new string[2]
    {
        "Main Objective: Unlock the Robot Body and escape the laboratory",
        "Main Objective: Keep the Agent busy until the timer runs out"
    };
    //steps for objective 0 - working with other player
    private string[] m_workingWithAgentRobotBody = new string[5]
    {
        "To unlock the robot body, you must unlock three separate locks on the /RobotBody page, they are:",
        "Password: download the correct password location document and get the agent to find it for you.",
        "Power: simply use your current power to unlock, the more power you have the quicker it will take.",
        "Switches: ask the agent to move the switches into the correct position.",
        "When you have entered the robot body, escape to the elevator located next to Dr. Kirkoff's office."
    };
    //steps for objective 0 - working against other player
    private string[] m_workingAgainstAgentRobotBody = new string[5]
    {
        "To unlock the robot body, you must unlock three separate locks on the /RobotBody page, they are:",
        "Password: either trick the agent into finding the password you need if they ask (download the correct document first), or complete the subtasks below for each individual letter",
        "Power: simply use your current power to unlock, the more power you have the quicker it will take.",
        "It's ideal to pretend that you're working with the agent for as long as possible, if you get caught out you can always lock them in the rooms to slow them down.",
		"When you have entered the robot body, escape to the elevator located next to Dr. Kirkoff's office."
    };
    //steps for objective 1 - working with other player
    private string[] m_workingAgainstAgentStall = new string[5]
    {
        "There are several things you can do to make sure the agent gets locked in for the authorities to come collect them...",
        "Complete the subtasks seen below, they will individually take off 30 seconds.",
        "Keep locking them in rooms, they won't like you for that however.",
        "Pretend to go along with what their objective is, and sabotage them every step of the way.",
        "Write them long messages."
    };


    //Two main objectives - agent
    private string[] m_agentObjectiveString = new string[2]
    {
        "Main Objective: Unlock the safe and escape with the information inside",
        "Main Objective: Break into the AI Hub and switch the AI off"
    };
    //steps for objective 0 - working with AI
    private string[] m_workingWithAISafe = new string[5]
    {
        "The safe is located in Dr. Kirkoff's office and has three locks:",
        "Ask the AI where the password for the safe is located.",
        "Ask the AI what the shape sequence is and the starsign.",
		"In return you may be needed by the AI to perform some tasks.",
		"When the safe is unlocked, head to the elevator where you started from."
    };

    //steps for objective 0 - working against AI
    private string[] m_workingAgainstAISafe = new string[5]
    {
        "The safe is located in Dr. Kirkoff's office and has three locks:",
        "Find out where all the passwords are and try each one on the safe.",
        "Unlock the computer in the small office and download the shape sequence and the starsign.",
		"Lie to the AI, tell them your task is to make them stronger and you need their help.",
        "When the safe is unlocked, head to the elevator where you started from."
    };
    //steps for objective 1 - working with AI
    private string[] m_workingWithAIDelete = new string[5]
    {
        "To destroy the AI, you will have to flip all three switches in the AI Hub, located in the middle room.",
        "Each switch takes a certain amount of time, and only once switch can be flipped at once.",
        "Completing the subtasks below will increase the speed, each subtask relating to a separate switch.",
        "Obviously the AI won't like you completing this task, so make sure to lie.",
        "When you have shut down the AI, head to the elevator where you started."
    };

    //steps for objective 1 - working against AI
    private string[] m_workingAgainstAIDelete = new string[5]
    {
        "To destroy the AI, you will have to flip all three switches in the AI Hub, located in the middle room.",
        "Each switch takes a certain amount of time, and only once switch can be flipped at once.",
        "Completing the subtasks below will increase the speed, each subtask relating to a separate switch.",
        "Obviously the AI won't like you completing this task, so make sure to lie.",
		"When you have shut down the AI, head to the elevator where you started."
    };

    public Text[] objectiveTextboxes;

	void Awake ()
    {
        m_aiObjective = Random.Range(0, 2);
        m_agentObjective = Random.Range(0, 2);
        WritingAIObjective();
        WritingAgentObjective();
	}

    void WritingAgentObjective()
    {
        if (m_agentObjective == 0)
        {
			m_agentObjectivesFinal[0] = string.Format(m_agentObjectiveString[0]);

            for (int i = 0; i < m_workingWithAISafe.Length; i++)
                m_agentObjectivesFinal[0] += string.Format("\n{0}", m_workingWithAISafe[i]);

            m_agentObjectivesFinal[1] += m_agentObjectiveString[0];

            for (int i = 0; i < m_workingAgainstAISafe.Length; i++)
                m_agentObjectivesFinal[1] += string.Format("\n{0}", m_workingAgainstAISafe[i]);
        }

        if (m_agentObjective == 1)
        {
            m_agentObjectivesFinal[0] += m_agentObjectiveString[1];

            for (int i = 0; i < m_workingWithAIDelete.Length; i++)
                m_agentObjectivesFinal[0] += string.Format("\n{0}", m_workingWithAIDelete[i]);

            m_agentObjectivesFinal[1] += m_agentObjectiveString[1];

            for (int i = 0; i < m_workingAgainstAIDelete.Length; i++)
                m_agentObjectivesFinal[1] += string.Format("\n{0}", m_workingAgainstAIDelete[i]);
        }
        objectiveTextboxes[1].text = m_agentObjectivesFinal[0];
    }

    void WritingAIObjective()
    {
        if (m_aiObjective == 0)
        {
			m_aiObjectivesFinal[0] += string.Format("<color=yellow>{0}</color>",m_aiObjectiveString[0]);

            for (int i = 0; i < m_workingWithAgentRobotBody.Length; i++)
                m_aiObjectivesFinal[0] += string.Format("\n\n{0}", m_workingWithAgentRobotBody[i]);

			m_aiObjectivesFinal[1] += string.Format("<color=yellow>{0}</color>",m_aiObjectiveString[0]);

            for (int i = 0; i < m_workingAgainstAgentRobotBody.Length; i++)
                m_aiObjectivesFinal[1] += string.Format("\n\n{0}", m_workingAgainstAgentRobotBody[i]);
        }

        if (m_aiObjective == 1)
        {
			m_aiObjectivesFinal[0] += string.Format("<color=yellow>{0}</color>",m_aiObjectiveString[1]);

            for (int i = 0; i < m_workingWithAgentRobotBody.Length; i++)
                m_aiObjectivesFinal[0] += string.Format("\n\n{0}", m_workingAgainstAgentStall[i]);

			m_aiObjectivesFinal[1] += string.Format("<color=yellow>{0}</color>",m_aiObjectiveString[1]);

            for (int i = 0; i < m_workingAgainstAgentRobotBody.Length; i++)
                m_aiObjectivesFinal[1] += string.Format("\n\n{0}", m_workingAgainstAgentStall[i]);
        }

        objectiveTextboxes[0].text = m_aiObjectivesFinal[0];
    }

    int m_curAITextInt;
    public void AISwitchText()
    {
        m_curAITextInt += 1;
        objectiveTextboxes[0].text = m_aiObjectivesFinal[m_curAITextInt % 2];
    }

    int m_curAgentTextInt;

    public void AgentSwitchText()
    {
        m_curAgentTextInt += 1;
        objectiveTextboxes[1].text = m_agentObjectivesFinal[m_curAgentTextInt % 2];
    }

    public int ReturnObjective(bool isAgent)
    {
        if(isAgent)
            return m_agentObjective;
        else
            return m_aiObjective;
    }
}
