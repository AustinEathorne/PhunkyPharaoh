using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDanceGame : MonoBehaviour {

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject m_NPCDanceLocation;
    [SerializeField] private GameObject m_playerDanceLocation;
    [SerializeField] private GameObject m_danceGameObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.GetComponent<CharacterMovement>().m_npcFollowing)
        {
            NpcClass followingNPC = other.GetComponent<CharacterMovement>().m_hitNPC.GetComponent<NpcClass>();
            followingNPC.m_correctDialogueCount = 0;
            followingNPC.StartDanceGame(m_NPCDanceLocation.transform.position);
            StartCoroutine(other.GetComponent<CharacterMovement>().SmoothMove(m_playerDanceLocation.transform.position, 0.15f));
            gameManager.m_isDanceGame = true;
        }
    }
}
