using UnityEngine;
using System.Collections;

public class AIWin : MonoBehaviour
{
    public int mainObjective;
    public bool completedMission;
	public ChoosingMainObjectives choosingMainObjectiveScript;
	void Start()
	{
		MainObjective ();
	}
	public void MainObjective()
	{
		mainObjective = choosingMainObjectiveScript.ReturnObjective (false);
	}
    // Update is called once per frame
    public void UpdateWin(int objective)
    {
        if (objective == mainObjective)
        {
            completedMission = true;
        }
    }
}
