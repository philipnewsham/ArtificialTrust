using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChoosePerks : MonoBehaviour
{
	public bool isAgent;
	private bool m_canChoose;

	private int m_currentPerk = 0;
	public int maxPerks;
    
	void Start () 
	{
		Invoke ("PerkChosen", 2f);	
	}

	void Update()
	{
		if (m_canChoose) 
		{
			//change S to down arrow
			if (Input.GetKeyDown (KeyCode.S)) 
			{
				m_currentPerk += 1;
				if (m_currentPerk >= maxPerks)
					m_currentPerk = 0;
			}
			//change W to down arrow
			if (Input.GetKeyDown (KeyCode.W)) 
			{
				m_currentPerk -= 1;
				if (m_currentPerk < 0)
					m_currentPerk = maxPerks;
			}
		}
	}

    //flips a coin to see if players can choose
	void PerkChosen()
	{
		int flipCoin = Random.Range(0,2);
        m_canChoose = (flipCoin == 0);
	}
}