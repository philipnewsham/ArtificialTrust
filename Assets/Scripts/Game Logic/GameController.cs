using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public FilingCabinet[] filingCabinetScripts;
    public SafeLocks safeScript;
    public SingleSwitch[] switchScripts;
    public ThreeSwitches threeSwitches;
    public SpecificDoor[] specificDoors;
    public SwitchesInfoScientist switchInfoScientist;
    public DisableCamera[] disableCameras;
    public LightController lightController;
    public LightToggleInstantiate lightToggleInstantiateScript;
    public AudioSource[] lightSwitchesAS;
    public ScientistComputer scientistComputerScript;

    public GameObject panicButton;
    private AudioSource m_panicButtonAS;
    private Renderer m_panicButtonRenderer;
    public Material switchedOffBlackMat;
    private bool m_panicButtonPressed = false;

    void Start()
    {
        m_panicButtonAS = panicButton.GetComponent<AudioSource>();
        m_panicButtonRenderer = panicButton.GetComponent<Renderer>();
    }

    //0 - safe/1,2,3 - Filing Cabinets/4,5,6 - Switches/7 - SwitchesButton/8 - Switches info/9 - PanicButton(Scientist)/10...19 - Doors/20...29 - Lights/30...39 - Cameras/40 - Scientist Computer
    public void InteractedWith(int objectID)
    {
        switch (objectID)
        {
            case 0:
                safeScript.Interact();
                break;
            case 1:
                filingCabinetScripts[0].Interact();
                break;
            case 2:
                filingCabinetScripts[1].Interact();
                break;
            case 3:
                filingCabinetScripts[2].Interact();
                break;
            case 4:
                switchScripts[0].Interact();
                break;
            case 5:
                switchScripts[1].Interact();
                break;
            case 6:
                switchScripts[2].Interact();
                break;
            case 7:
                threeSwitches.Interact();
                break;
            case 8:
                switchInfoScientist.Interact();
                break;
            case 9:
                if (!m_panicButtonPressed)
                {
                    gameObject.GetComponent<PanicButton>().Interact();
                    m_panicButtonRenderer.material = switchedOffBlackMat;
                    panicButton.tag = "Untagged";
                    m_panicButtonAS.Play();
                    m_panicButtonPressed = true;
                }
                break;
            case 40:
                scientistComputerScript.Interact();
                break;
            default:
                if (objectID >= 10 && objectID <= 19)
                    specificDoors[objectID - 10].Interact();
                if (objectID >= 20 && objectID <= 29)
                    lightToggleInstantiateScript.LightToggled(objectID - 20);
                    lightSwitchesAS[objectID - 20].Play();
                if (objectID >= 30 && objectID <= 39)
                    disableCameras[objectID - 30].Interact();
                break;
        }
    }

    public void AIInteractedWith(int number)
    {
        InteractedWith(number);
    }
}
