using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorTile : MonoBehaviour {

    public int m_index;
    public bool m_activateDanceTile = false;

    private Sprite m_spriteOriginal;

    [SerializeField] private GameObject m_danceGameObj;
    private DanceGame m_danceGame;

    private void Awake()
    {
        m_danceGame = m_danceGameObj.GetComponent<DanceGame>();
        m_spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && m_activateDanceTile)
        {
            Deactivate();
            m_danceGame.m_playerHit++;
            Debug.Log("hitTile!!!!!!!!!!!!!!!");
        }
    }

    public void Deactivate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = m_spriteOriginal;
        m_activateDanceTile = false;
    }
}
