using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimationMove : MonoBehaviour
{
	private Vector3 startPos;
	public Transform parentTran;

	void Start ()
    {
		startPos = new Vector3 (0,-1,0);
	}
	
	void FixedUpdate () 
	{
		transform.position = parentTran.position + startPos;
		transform.eulerAngles = new Vector3 (parentTran.eulerAngles.x, parentTran.eulerAngles.y, parentTran.eulerAngles.z);
	}
}
