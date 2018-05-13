using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialWalkthrough : MonoBehaviour
{
    private BinaryDecipher binaryDecipher;
    private DoorController doorController;

    public GameObject[] nextWayfinders;
    public Button nextPuzzleButton;
    public ChangeTextColour changeTextColourScript;
    public GameObject completedPanel;

    void Start()
    {
        binaryDecipher = FindObjectOfType<BinaryDecipher>();
        doorController = FindObjectOfType<DoorController>();
    }

    IEnumerator Walkthrough()
    {
        yield return new WaitUntil(()=>binaryDecipher.IsCompleted());
        BinaryDecipherCompleted();
        //yield return new WaitUntil(()=>)
    }

    void BinaryDecipherCompleted()
    {
        completedPanel.SetActive(true);
        TutorialOpenDoors(7);
        TutorialOpenDoors(0);
        nextWayfinders[0].SetActive(true);
        nextPuzzleButton.interactable = true;
        changeTextColourScript.AnimationTrigger();
    }

    void TutorialOpenDoors(int doorNo)
    {
        doorController.TutorialOpenDoors(doorNo, true);
    }
}
