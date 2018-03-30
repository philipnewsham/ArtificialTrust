using UnityEngine;
using System.Collections;

public class DisableSavedCanvas : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetButtonDown("ControllerA"))
            gameObject.SetActive(false);
	}
}
