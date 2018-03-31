using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResizingText : MonoBehaviour
{
    private Text m_thisText;
    private float m_width;
	// Use this for initialization
	void Start () {
        m_thisText = GetComponent<Text>();
        m_width = Screen.width;
	}
}
