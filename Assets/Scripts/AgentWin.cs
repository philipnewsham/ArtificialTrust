using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentWin : MonoBehaviour
{
    public int mainObjective;
    public bool completedMission;
	public ChoosingMainObjectives choosingObjective;

	void Start()
	{
		MainObjective ();
	}

	public void MainObjective()
	{
		mainObjective = choosingObjective.ReturnObjective(true);
	}

    public void UpdateWin(int objective)
    {
        completedMission = (objective == mainObjective);
    }
}
