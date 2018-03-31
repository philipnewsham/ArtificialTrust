using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentButton : MonoBehaviour
{
    public float powerNeeded;
    private float m_downloadedPower;
    float m_currentPower;
    private AIPower m_aiPower;

    public string hoverText;
    public string documentText;
    public int passwordNo;
    public Text hoverTextbox;
    public Text informationTextbox;

    public Button[] unlockedButtons;

    bool m_isCompleted;

    private Image m_fillImage;
    public bool isUnlocked;
    public string docName;
    private string[] m_currentStatus = new string[3] { "Unlock previous document first", "downloading:", "paused at" };
    private Text m_buttonText;

    private DocumentController m_documentControllerScript;
    public int thisID;

	void Start ()
    {
        m_documentControllerScript = GetComponentInParent<DocumentController>();
        m_aiPower = GameObject.FindGameObjectWithTag("AI").GetComponent<AIPower>();
        m_buttonText = GetComponentInChildren<Text>();
        
        UpdateButtonText(isUnlocked ? 2 : 0, 0.0f);
    }

    bool isDownloading;

    public void Clicked()
    {
        m_documentControllerScript.CurrentButton(thisID);

        if (!m_isCompleted)
            isDownloading = !isDownloading;

        m_currentPower = m_aiPower.CurrentPower();
        hoverTextbox.text = hoverText;

        if (m_isCompleted)
            informationTextbox.text = documentText;
    }

    public void StopDownloading()
    {
        if (isDownloading)
            isDownloading = false;
    }

    public void HoverText(bool isHover)
    {
        if(isHover)
            hoverTextbox.text = hoverText;
    }

    float percentage;

	void Update ()
    {
        if (!isDownloading)
            return;

        m_downloadedPower += m_currentPower * Time.deltaTime;
        percentage = (m_downloadedPower / powerNeeded) * 100f;

        if (m_downloadedPower >= powerNeeded)
        {
            DownloadComplete();
            percentage = 100f;
        }
        
        UpdateButtonText(1, percentage);
	}

    public void Unlocked()
    {
        UpdateButtonText(2, 0.0f);
    }

    void UpdateButtonText(int statusType,float percentage)
    {
        m_buttonText.text = string.Format("{0} - {1} {2}%", docName, m_currentStatus[statusType], percentage);
    }

    void DownloadComplete()
    {
        isDownloading = false;
        m_isCompleted = true;
        informationTextbox.text = documentText;

        for (int i = 0; i < unlockedButtons.Length; i++)
        {
            unlockedButtons[i].interactable = true;
            unlockedButtons[i].GetComponent<DocumentButton>().Unlocked();
        }
    }
}
