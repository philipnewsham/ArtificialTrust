using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckCurrentRoom : MonoBehaviour
{
    private string[] m_roomName = new string[9] { "Main Laboratory", "Small Office", "Server Room", "AI HUB", "Archives", "Dr. Kirkoff's Office", "Corridor One", "Corridor Two", "Corridor Three" };
    private int roomNo = 6;
    public Text roomNameText;
    private int mapButtonLength;
    public Button[] mapLocations;
    private Sprite[] m_locationSprites;
    public Sprite standingMan;

    void Awake()
    {
        UpdateRoomNameText();
        mapButtonLength = mapLocations.Length;
        m_locationSprites = new Sprite[mapButtonLength];

        for (int i = 0; i < mapButtonLength; i++)
            m_locationSprites[i] = mapLocations[i].GetComponent<Image>().sprite;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Room")
        {
            roomNo = other.gameObject.GetComponent<CurrentRoom>().currentRoom;
            UpdateRoomNameText();
            UpdateMapLocation();
        }
    }

    void UpdateRoomNameText()
    {
        roomNameText.text = m_roomName[roomNo];
    }

    void UpdateMapLocation()
    {
        for (int i = 0; i < mapButtonLength; i++)
            mapLocations[i].GetComponent<Image>().sprite = (i == roomNo) ? standingMan : m_locationSprites[i];
    }

    public int GetCurrentRoomNo()
    {
        return roomNo;
    }
}
