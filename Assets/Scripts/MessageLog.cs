using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageLog : MonoBehaviour
{
    private Text m_messageLogText;
    private string m_newLine;
    public InputField messageInputField;

    void Start()
    {
        m_messageLogText = GetComponent<Text>();
        m_newLine = "Message Log:";
    }

    public void SendText()
    {
        UpdateText(messageInputField.text);
    }

    public void UpdateText(string message)
    {
        m_newLine += string.Format("\n{0}", message);
        m_messageLogText.text = m_newLine;
    }

}
