using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject[] cameraGameObjects;
    public Camera[] cameras;
    private bool[] m_camerasOn;
    private AIPower m_aiPowerScript;
    private int m_cameraPower;
    private bool m_showingCamerasA = false;
    private bool m_showingCamerasB = false;
    public Material[] materials;
    public GameObject[] cameraLights;
    private Renderer[] m_cameraLightRenderers;
    private int m_cameraLightLength;

	void Start ()
    {
        m_aiPowerScript = gameObject.GetComponent<AIPower>();
        m_cameraPower = m_aiPowerScript.cameraPower;
        m_cameraLightLength = cameras.Length;
        m_camerasOn = new bool[m_cameraLightLength];
        
        m_cameraLightRenderers = new Renderer[m_cameraLightLength];
        for (int i = 0; i < m_cameraLightLength; i++)
        {
            m_cameraLightRenderers[i]  = cameraLights[i].GetComponent<Renderer>();
            m_camerasOn[i] = true;
        }
	}

    public void CurrentCameraPower(int newPower)
    {
        m_cameraPower += newPower;
    }

    public void CameraSwitch(int camNo)
    {
        if (m_camerasOn[camNo] == true)
        {
            cameras[camNo].enabled = false;
            m_cameraLightRenderers[camNo].material = materials[0];
            m_aiPowerScript.PowerExchange(m_cameraPower);
            m_camerasOn[camNo] = !m_camerasOn[camNo];
        }
        else
        {
            if (m_aiPowerScript.CheckPower(m_cameraPower))
            {
                m_cameraLightRenderers[camNo].material = materials[1];
                cameras[camNo].enabled = true;
                m_aiPowerScript.PowerExchange(-m_cameraPower);
                m_camerasOn[camNo] = !m_camerasOn[camNo];
            }
        }
    }

    public void WaitAndShowCamerasA()
    {
            ShowCamerasA();
            HideCamerasB();
    }

    public void WaitAndShowCamerasB()
    {
            ShowCamerasB();
            HideCamerasA();
    }

    public void LoadCameras()
    {
        Invoke("ShowCamerasA", 1f);
    }

    public void WaitAndHideCameras()
    {
        Invoke("HideAllCameras", 1f);
    }

    void ShowCamerasA()
    {
        for (int i = 0; i < m_cameraLightLength - 4; i++)
            cameraGameObjects[i].SetActive(true);
    }

    void HideCamerasB()
    {
        for (int i = m_cameraLightLength - 4; i < m_cameraLightLength; i++)
            cameraGameObjects[i].SetActive(false);
    }
    
    void ShowCamerasB()
    {
        for (int i = m_cameraLightLength - 4; i < m_cameraLightLength; i++)
            cameraGameObjects[i].SetActive(true);
    }

    void HideCamerasA()
    {
        for (int i = 0; i < m_cameraLightLength - 4; i++)
            cameraGameObjects[i].SetActive(false);
    }

    void HideAllCameras()
    {
        for (int i = 0; i < m_cameraLightLength; i++)
            cameraGameObjects[i].SetActive(false);
    }
}