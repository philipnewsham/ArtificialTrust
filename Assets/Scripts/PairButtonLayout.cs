using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PairButtonLayout : MonoBehaviour
{
    void Start()
    {
        float width = (0.6f * Screen.width) / 2;
        float height = ((0.7f * Screen.height) / 4)/2;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(width, height);
    }
}
