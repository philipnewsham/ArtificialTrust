using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AgentFinishTutorial : MonoBehaviour
{
    public GameObject[] tutorialObjectives;
    public GameObject[] mainObjectives;
    public Text hudObjective;
    public void TutorialFinished()
    {
        for (int i = 0; i < tutorialObjectives.Length; i++)
        {
            tutorialObjectives[i].SetActive(false);
        }
        for (int i = 0; i < mainObjectives.Length; i++)
        {
            mainObjectives[i].SetActive(true);
        }
        
        hudObjective.text = "Press the start button to see objectives";
    }
}
