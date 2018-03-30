using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class IncreaseTextSize : MonoBehaviour
{
    private Text m_thisText;

	void Start ()
    {
        m_thisText = gameObject.GetComponent<Text>();
        m_thisText.fontSize += 10;
	}
}
