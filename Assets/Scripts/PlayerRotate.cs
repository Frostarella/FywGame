using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AllLevels
{
	/* Class to rotate player while moving
	 * only neccessary if player is textured,
	 * rotation currently not visible
	 */
	public class PlayerRotate : MonoBehaviour
	{
		private NavMeshAgent navAgent;

		void Awake ()
		{
			navAgent = GetComponentInParent<NavMeshAgent> ();
		}

		void Update ()
		{
			// If NavMeshAgent on player is null, get current NavMeshAgent			
			navAgent = GetComponentInParent<NavMeshAgent> ();
		
			// If NavMeshAgent on player exists and player is moving, rotate
			if (navAgent != null) {
				if (navAgent.hasPath) {
					RotatePlayer ();
				}
			}
		}

		// Rotate player by <rotationFactor> degrees per second
		private void RotatePlayer ()
		{
			int rotationFactor = 150;
			transform.Rotate (Vector3.right, Time.deltaTime * rotationFactor);
		}
	}
}