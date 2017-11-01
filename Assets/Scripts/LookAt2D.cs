using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt2D : MonoBehaviour {

	[SerializeField]
	private Transform m_lookTarget;

	private Transform m_transform;

	void Start () 
	{
		this.m_transform = this.gameObject.transform;

		if(this.m_lookTarget == null)
		{
			this.m_lookTarget = Camera.main.transform; // for da npc prefabs
		}
	}

	void Update () 
	{
		this.LookAtTarget();
	}

	private void LookAtTarget()
	{
		Vector3 lookDirection = this.m_lookTarget.position - this.transform.position;
		this.m_transform.forward = new Vector3(lookDirection.x, 0.0f, lookDirection.z);
	}
}
