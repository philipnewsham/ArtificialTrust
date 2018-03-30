using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    public int[] possibleTimes;
    private int[] m_possibleTimes { get { return possibleTimes; } }
    private float m_currentTime;
    private float m_timerSpeed = 1f;
    public bool countingDown;

    public GameObject scientistLosesCanvas;
    public GameObject aiLosesCanvas;
	
	void Update ()
    {
        if (countingDown)
        {
            m_currentTime -= m_timerSpeed * Time.deltaTime;

            if (m_currentTime <= 0)
            {
                countingDown = false;
                ShutDown();
            }
        }
	}

    public void ChangeTime(int newTime)
    {
        newTime *= 60;
        print(newTime + " seconds");
        m_currentTime = newTime;
    }

    public void ChangeTimeSpeed(float newSpeed)
    {
        m_timerSpeed = newSpeed;
    }

    void ShutDown()
    {
        scientistLosesCanvas.SetActive(true);
        aiLosesCanvas.SetActive(true);
        Invoke("MainMenu", 10f);
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}