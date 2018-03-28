using UnityEngine;
using System.Collections;

public class AICamera : MonoBehaviour
{
    private bool m_cameraDrag;

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
            m_cameraDrag = true;

        if (Input.GetMouseButtonUp(0))
            m_cameraDrag = false;
	}
}
