using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuUIHoverInfo : MonoBehaviour
{
    public Text infoBox;
    public string info;

    public void OnMouseOver()
    {
        infoBox.text = info;
    }

    void OnMouseEnter()
    {
        infoBox.text = info;
    }
}
