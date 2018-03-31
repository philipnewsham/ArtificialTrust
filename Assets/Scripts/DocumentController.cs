using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DocumentController : MonoBehaviour
{
    private DocumentButton[] m_documentButtons;
	// Use this for initialization
	void Start ()
    {
        m_documentButtons = GetComponentsInChildren<DocumentButton>();
	}
	
	// Update is called once per frame
	public void CurrentButton(int curButNo)
    {
        for (int i = 0; i < m_documentButtons.Length; i++)
        {
            if (i != curButNo)
                m_documentButtons[i].StopDownloading();
        }
    }
}
