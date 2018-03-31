using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GridMapSize : MonoBehaviour
{
    public bool isOverriding;
    public Vector2 canvasSize;
    GridLayoutGroup m_gridLayoutGroup;
	void Start ()
    {
        float width = (Screen.width * 0.6f)/12;
        float height = (Screen.height * 0.7f)/9;
        if(isOverriding)
        {
            width = (Screen.width * canvasSize.x) / 12;
            height = (Screen.height * canvasSize.y) / 12;
        }
        m_gridLayoutGroup = GetComponent<GridLayoutGroup>();
        m_gridLayoutGroup.cellSize = new Vector2(width, height);
	}

}
