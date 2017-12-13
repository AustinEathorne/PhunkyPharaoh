using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceGame : MonoBehaviour {


    [SerializeField] private GameObject[] m_danceFloor;
    private List<GameObject> m_selectedDanceFloorTiles;
    private List<GameObject> m_selectedDanceFloorTiles2;
    [SerializeField] private List<GameObject> m_chosenDanceFloorTiles;


    // Use this for initialization
    void Start ()
    {
        SetDanceTiles();	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetDanceTiles()
    {
        for(int i = 0; i < m_danceFloor.Length; i++)
        {
            m_danceFloor[i].GetComponent<DanceFloorTile>().m_index = Random.Range(0, 3);    
        }
        for (int x = 0; x < m_danceFloor.Length; x++)
        {
            if (m_danceFloor[x].GetComponent<DanceFloorTile>().m_index == 1)
            {
                m_selectedDanceFloorTiles.Add(m_danceFloor[x]);
            }
            if(m_danceFloor[x].GetComponent<DanceFloorTile>().m_index == 2)
            {
                m_selectedDanceFloorTiles2.Add(m_danceFloor[x]);
            }
        }
        if(m_selectedDanceFloorTiles.Count > m_selectedDanceFloorTiles2.Count)
        {
            m_chosenDanceFloorTiles = m_selectedDanceFloorTiles;
        }
        else if(m_selectedDanceFloorTiles2.Count > m_selectedDanceFloorTiles.Count)
        {
            m_chosenDanceFloorTiles = m_selectedDanceFloorTiles2;
        }
    }

    //private IEnumerator StartDanceGame()
    //{
    //    for(int i = 0; i < m_chosenDanceFloorTiles.Count; i++)
    //    {

    //    }
    //}

}
