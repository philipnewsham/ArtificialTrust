﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PasswordGenerator : MonoBehaviour
{
    private string m_currentPassword;
    private string[] m_lockObjectNames = new string[3] { "Robot Body", "Computer", "Safe" };
    private List<string> m_keyObjectNames = new List<string> { "Filing Cabinet 1", "Filing Cabinet 2", "Filing Cabinet 3" };
    private Passwords m_passwordSavingScript;

    private List<int> m_passwordIDs = new List<int>();

    public List<ReceivePasswords> receivePasswordsLocks = new List<ReceivePasswords>();
    private List<ReceivePasswords> m_receivePasswordsLocks;

    public List<ReceivePasswords> receivePasswordsKeys = new List<ReceivePasswords>();
    private List<ReceivePasswords> m_receivePasswordsKeys;

    public GameObject ai;
    private HackingDocuments m_hackingDocumentScript;

    public Text[] safePasswordText;
    public GameObject safeLogic;
    private SafeLocks m_safeLocksScript;
    private string[] m_safePasswords = new string[4];
    private string[] m_allPasswords = new string[5];
    private List<int> m_numbers = new List<int>();
    private List<int> m_lockNumbers = new List<int>();

    public RobotBodyPasswordButton robotBodyPasswordButtonScript;

    public DocumentButton[] documentButtons;

    private GeneratePassword generatePassword;

    void Start ()
    {
        GetReferences();

        m_receivePasswordsKeys = receivePasswordsKeys;
        m_receivePasswordsLocks = receivePasswordsLocks;
        m_allPasswords = new string[m_receivePasswordsKeys.Count];

        for (int i = 0; i < 4; i++)
            m_numbers.Add(i);

        for (int i = 0; i < m_receivePasswordsLocks.Count; i++)
            m_lockNumbers.Add(i);

        CreatePassword();
	}

    void GetReferences()
    {
        generatePassword = FindObjectOfType<GeneratePassword>();
        m_safeLocksScript = safeLogic.GetComponent<SafeLocks>();
        m_hackingDocumentScript = ai.GetComponent<HackingDocuments>();
        m_passwordSavingScript = gameObject.GetComponent<Passwords>();
    }

    public AIObjectives aiObjectivesScript;

    void CreatePassword()
    {
        for (int i = 0; i < m_receivePasswordsLocks.Count; i++)
            m_allPasswords[i] = generatePassword.GetGeneratePassword(3);

        for (int i = 0; i < 3; i++)
        {
            m_receivePasswordsLocks[i].password = m_allPasswords[i];
            int randomKey = Random.Range(0, m_lockNumbers.Count);

            switch (i)
            {
                case 0:
                    robotBodyPasswordButtonScript.ReceivePassword(m_allPasswords[0]);
                    aiObjectivesScript.ReceivePassword(m_allPasswords[0]);
                    break;
                case 2:
                    m_safePasswords[3] = m_allPasswords[i];
                    m_safeLocksScript.actualPassword = m_allPasswords[i];
                    break;
            }

            m_receivePasswordsKeys[(m_lockNumbers[randomKey])].password = m_allPasswords[i];
            string message = string.Format("////CONFIDENTIAL////\n---For Authorised Personnel Only---\n{0} has the password for {1}", m_keyObjectNames[(m_lockNumbers[randomKey])], m_lockObjectNames[i]);
            documentButtons[i].documentText = message;
            m_hackingDocumentScript.RecieveDocumentMessages(message, i);
            m_lockNumbers.Remove(m_lockNumbers[randomKey]);
        }

        SafePasswords();
    }

    void SafePasswords()
    {
        //fake passwords
        for (int i = 0; i < 3; i++)
            m_safePasswords[i] = generatePassword.GetGeneratePassword(3);

        for (int i = 0; i < 4; i++)
        {
            int randNum = Random.Range(0, m_numbers.Count);
            m_safeLocksScript.passwords[i] = m_safePasswords[(m_numbers[randNum])];
            m_numbers.Remove(m_numbers[randNum]);
        }

        m_safeLocksScript.UpdatePasswords();
    }
}
