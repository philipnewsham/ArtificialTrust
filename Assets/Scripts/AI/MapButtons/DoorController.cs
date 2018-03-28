using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour {
    private AIPower m_aiPowerScript;
    public SpecificDoor[] m_specificDoorScripts;
    private int m_lockedDoorPower;
    private int m_unlockedDoorPower;
    private bool[] m_locked;
    private bool[] m_powered;
    public int unlockingTime;
    public int openingTime;
    private bool[] m_lockedOutAction;
    private int m_doorsLength;
    void Start()
    {
        m_aiPowerScript = gameObject.GetComponent<AIPower>();
        m_lockedDoorPower = m_aiPowerScript.doorLockedPower;
        m_unlockedDoorPower = m_aiPowerScript.doorUnlockedPower;
        m_doorsLength = m_specificDoorScripts.Length;
        m_locked = new bool[m_doorsLength];
        m_powered = new bool[m_doorsLength];
        m_lockedOutAction = new bool[m_doorsLength];
        for (int i = 0; i < m_doorsLength; i++)
        {
            m_locked[i] = false;
            m_powered[i] = true;
            m_lockedOutAction[i] = false;
        }

    }

    public void CurrentDoorPower(int newPower)
    {
        m_unlockedDoorPower += newPower;
    }

    public void Locking(int doorNo)
    {
        //print("locking: door controller");
        if (m_locked[doorNo])
        {
            if (m_lockedOutAction[doorNo])
            {
                m_specificDoorScripts[doorNo].DoorPower("Unlock");
                m_locked[doorNo] = false;
                m_lockedOutAction[doorNo] = false;
            }
            else
            {
                m_aiPowerScript.PowerExchange(m_lockedDoorPower);
                m_specificDoorScripts[doorNo].DoorPower("Unlock");
                m_locked[doorNo] = !m_locked[doorNo];
            }
        }
        else
        {
            if(m_aiPowerScript.CheckPower(m_lockedDoorPower) == true)
            {
                m_aiPowerScript.PowerExchange(-m_lockedDoorPower);
                m_specificDoorScripts[doorNo].DoorPower("Lock");
                m_locked[doorNo] = !m_locked[doorNo];
            }
            else
            {
                //no power
            }
        }
    }

    public void Powering(int doorNo)
    {
        //if that door has power
        if (m_powered[doorNo])
        {
            //if the door is locked (15pow)
            if (m_locked[doorNo])
            {
                //print("Seargent");
                m_aiPowerScript.PowerExchange(m_unlockedDoorPower);
            }
            //if the door is unlocked (5pow)
            else
            {
                m_aiPowerScript.PowerExchange(m_unlockedDoorPower);
            }

            m_specificDoorScripts[doorNo].DoorPower("Off");
            m_powered[doorNo] = !m_powered[doorNo];
        }
        else
        {
            if(m_aiPowerScript.CheckPower(m_unlockedDoorPower) == true)
            {
                m_aiPowerScript.PowerExchange(-m_unlockedDoorPower);
                m_specificDoorScripts[doorNo].DoorPower("On");
                m_powered[doorNo] = !m_powered[doorNo];
            }
            else
            {
                //no power
            }
        }
    }

    public void LockAllDoors()
    {
        for (int i = 0; i < m_locked.Length; i++)
        {
            m_lockedOutAction[i] = true;
            m_locked[i] = true;
            m_specificDoorScripts[i].DoorPower("Lock");
        }
    }


    public void AllPowerOff()
    {
        for (int i = 0; i < m_powered.Length; i++)
        {
            m_powered[i] = false;
            m_specificDoorScripts[i].DoorPower("Off");
        }
    }
}
