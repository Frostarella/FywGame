using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AllLevels
{
	/* Class to control Player movement in the Game
	 */
	public class PlayerMove : MonoBehaviour
	{
		private NavMeshAgent navAgent;

		void Awake ()
		{
			navAgent = GetComponent<NavMeshAgent> ();
		}

		void Update ()
		{
			// Get Raycast of mousclick
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			// If NavMeshAgent on player is null, get current NavMeshAgent
			if (navAgent == null) {
				navAgent = GetComponent<NavMeshAgent> ();
			}

			// If right mouse button is clicked and if Raycast hit on walkable area,
			// call MovePlayer method
			if (Input.GetButtonDown ("Fire1") && navAgent != null) {
				if (Physics.Raycast (ray, out hit, 100)) {
					MovePlayer (hit);
				}
			}
		}

		// Set NavMeshAgent's destination to hit position of Raycast
		private void MovePlayer (RaycastHit hit)
		{
			navAgent.destination = hit.point;
			navAgent.Resume ();
		}
	}
}
