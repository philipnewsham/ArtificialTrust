using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissIntroPanelAgent : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject pressAText;
    private bool m_canCancel;
	// Use this for initialization
	void Start () {
        Invoke("CanDismissPanel", 5f);
	}
	
    void CanDismissPanel()
    {
        pressAText.SetActive(true);
        m_canCancel = true;
    }    
        
	void Update () {
		if(Input.GetButtonDown("ControllerA") && m_canCancel)
        {
            introPanel.SetActive(false);
        }
	}
}
