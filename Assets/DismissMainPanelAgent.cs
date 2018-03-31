using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissMainPanelAgent : MonoBehaviour {
    public GameObject pressAText;
    private bool m_canDismiss;
	// Use this for initialization
	public void WaitToDismiss ()
    {
        Invoke("CanDismiss", 5);
	}

    void CanDismiss()
    {
        m_canDismiss = true;
        pressAText.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		if(m_canDismiss && Input.GetButtonDown("ControllerA"))
        {
            gameObject.SetActive(false);
        }
	}
}
