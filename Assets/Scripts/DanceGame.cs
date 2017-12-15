using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceGame : MonoBehaviour {

    [HideInInspector][SerializeField] private List<GameObject> m_selectedDanceFloorTiles = null;

    [SerializeField] private Sprite m_spriteActiveTile;

    [SerializeField] private GameObject m_centerTile;
    private GameObject m_lastTile = null;
    private GameObject m_currentTile = null;

    private GameObject m_firstTileToSetBack;
    private GameObject m_secondTileToSetBack;

    private int m_tileChoiceIndex = 0;
    private int m_index = 0;
    [HideInInspector] public int m_playerHit = 0;

    [SerializeField] private bool m_startDancePhase = false;

    [SerializeField] private GameObject m_playerObj;
    private CharacterMovement m_player;

    private void Awake()
    {
        m_player = m_playerObj.GetComponent<CharacterMovement>();
    }

    void Update ()
    {
        if(AudioManager.isOnBeat && m_startDancePhase)
        {
            SelectNextTile();
        }
        else if(AudioManager.isOnBeat && m_playerHit >= 4)
        {
            SelectNextTile();           
        }
    }

    private void SelectNextTile()
    {

        switch (m_tileChoiceIndex)
        {
            case 0:
                GetAdjacentTiles(m_centerTile.transform);
                m_index = Random.Range(0, m_selectedDanceFloorTiles.Count);
                m_currentTile = m_selectedDanceFloorTiles[m_index];
                m_firstTileToSetBack = m_currentTile;
                m_currentTile.GetComponent<DanceFloorTile>().m_activateDanceTile = true;
                m_player.m_hitNPC.GetComponent<NpcClass>().StartDanceGame(m_currentTile.transform.position + new Vector3(0,0,1));
                m_currentTile.GetComponent<SpriteRenderer>().sprite = m_spriteActiveTile;
                m_lastTile = m_currentTile;
                m_selectedDanceFloorTiles.Clear();
                m_index = 0;
                m_tileChoiceIndex++;
                break;

            case 1:
                GetAdjacentTiles(m_currentTile.transform);
                while(true)
                {
                    m_index = Random.Range(0, m_selectedDanceFloorTiles.Count);
                    if(m_selectedDanceFloorTiles[m_index] == m_centerTile)
                    {
                        continue;
                    }
                    m_currentTile = m_selectedDanceFloorTiles[m_index];
                    m_secondTileToSetBack = m_currentTile;
                    m_currentTile.GetComponent<DanceFloorTile>().m_activateDanceTile = true;
                    m_player.m_hitNPC.GetComponent<NpcClass>().StartDanceGame(m_currentTile.transform.position + new Vector3(0, 0, 1));
                    m_currentTile.GetComponent<SpriteRenderer>().sprite = m_spriteActiveTile;
                    m_selectedDanceFloorTiles.Clear();
                    m_index = 0;
                    m_tileChoiceIndex++;
                    break;
                }
                break;

            case 2:
                GetAdjacentTiles(m_currentTile.transform);
                while (true)
                {
                    m_index = Random.Range(0, m_selectedDanceFloorTiles.Count);
                    if (m_selectedDanceFloorTiles[m_index] == m_lastTile)
                    {
                        continue;
                    }
                    m_firstTileToSetBack.GetComponent<DanceFloorTile>().Deactivate();
                    m_currentTile = m_selectedDanceFloorTiles[m_index];
                    m_firstTileToSetBack = m_currentTile;
                    m_currentTile.GetComponent<DanceFloorTile>().m_activateDanceTile = true;
                    m_player.m_hitNPC.GetComponent<NpcClass>().StartDanceGame(m_currentTile.transform.position + new Vector3(0, 0, 1));
                    m_currentTile.GetComponent<SpriteRenderer>().sprite = m_spriteActiveTile;
                    m_lastTile = m_currentTile;
                    m_selectedDanceFloorTiles.Clear();
                    m_index = 0;
                    m_tileChoiceIndex++;
                    break;
                }
                break;

            case 3:
                m_secondTileToSetBack.GetComponent<DanceFloorTile>().Deactivate();
                m_secondTileToSetBack = m_centerTile;
                m_centerTile.GetComponent<DanceFloorTile>().m_activateDanceTile = true;
                m_player.m_hitNPC.GetComponent<NpcClass>().StartDanceGame(m_centerTile.transform.position + new Vector3(0, 0, 1));
                m_centerTile.GetComponent<SpriteRenderer>().sprite = m_spriteActiveTile;
                m_index = 0;
                m_tileChoiceIndex++;
                break;

            case 4:
                m_firstTileToSetBack.GetComponent<DanceFloorTile>().Deactivate();
                m_tileChoiceIndex++;
                m_firstTileToSetBack = null;
                break;

            case 5:
                m_secondTileToSetBack.GetComponent<DanceFloorTile>().Deactivate();
                m_secondTileToSetBack = null;
                m_tileChoiceIndex = 0;
                m_startDancePhase = false;
                break;

            default:
                break;
        }
    }

    private void GetAdjacentTiles(Transform currentTile)
    {
        RayCastCheck(currentTile.transform, currentTile.transform.up);
        RayCastCheck(currentTile.transform, -currentTile.transform.up);
        RayCastCheck(currentTile.transform, currentTile.transform.right);
        RayCastCheck(currentTile.transform, -currentTile.transform.right);
    }

    private void RayCastCheck(Transform rayPos, Vector3 rayDirection)
    {
        Ray ray = new Ray(rayPos.position, rayDirection);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 0.32f))
        {
            if (hit.transform.tag == "DanceTile")
            {
                m_selectedDanceFloorTiles.Add(hit.transform.gameObject);
            }
        }
    }


    //public void SetDanceTiles()
    //{
    //    for(int i = 0; i < m_danceFloor.Length; i++)
    //    {
    //        m_danceFloor[i].GetComponent<DanceFloorTile>().m_index = Random.Range(0, 3);    
    //    }
    //    for (int x = 0; x < m_danceFloor.Length; x++)
    //    {
    //        if (m_danceFloor[x].GetComponent<DanceFloorTile>().m_index == 1)
    //        {
    //            m_selectedDanceFloorTiles.Add(m_danceFloor[x]);
    //        }
    //        if(m_danceFloor[x].GetComponent<DanceFloorTile>().m_index == 2)
    //        {
    //            m_selectedDanceFloorTiles2.Add(m_danceFloor[x]);
    //        }
    //    }
    //    if(m_selectedDanceFloorTiles.Count >= m_selectedDanceFloorTiles2.Count)
    //    {
    //        m_chosenDanceFloorTiles = m_selectedDanceFloorTiles;
    //    }
    //    else if(m_selectedDanceFloorTiles2.Count > m_selectedDanceFloorTiles.Count)
    //    {
    //        m_chosenDanceFloorTiles = m_selectedDanceFloorTiles2;
    //    }
    //}


}
