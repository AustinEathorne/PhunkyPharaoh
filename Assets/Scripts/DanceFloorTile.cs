using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceFloorTile : MonoBehaviour {

    public int m_index;
    public bool m_activateDanceTile = false;

    private Sprite m_spriteOriginal;

    private void Awake()
    {
        m_spriteOriginal = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && m_activateDanceTile)
        {
            Deactivate();
            Debug.Log("hitTile!!!!!!!!!!!!!!!");
        }
    }

    public void Deactivate()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = m_spriteOriginal;
        m_activateDanceTile = false;
    }
}
