using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScientistRaycast : MonoBehaviour
{
    public float distance;
    private float m_distance{ get { return distance; }}

    private Ray m_ray;
    private RaycastHit m_hit;

    public GameController gameController;
    public Text interactText;
    public GameObject aButton;

    private bool m_showingRules;
    public GameObject scientistRules;

    void Start ()
    {
        interactText.text = "";
	}
	
	void Update ()
    {
        Vector3 origin = transform.position;
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        m_ray = new Ray(origin, direction);

        if (Physics.Raycast(m_ray, out m_hit))
        {
            if(m_hit.collider.tag == "Interactable" && m_hit.distance <= distance)
            {
                interactText.text = "Interact!";
                aButton.SetActive(true);
                if (Input.GetButtonDown("ControllerA"))
                {
                    gameController.InteractedWith(m_hit.collider.gameObject.GetComponent<InteractableObject>().interactableID);
                }
            }
            else
            {
                interactText.text = "";
                aButton.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            m_showingRules = !m_showingRules;
            scientistRules.SetActive(m_showingRules);
        }
	}
}
