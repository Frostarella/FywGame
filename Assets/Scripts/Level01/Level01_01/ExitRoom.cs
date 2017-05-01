using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Level01_01
{
	/* Class to control levelpart exit in Level01_01
	 */
	public class ExitRoom : MonoBehaviour
	{
		private GameObject player;
		private NavMeshAgent playerAgent;
		private GameObject sublevel01;
		private GameObject sublevel02;
		private GameObject mainCamera;
		private CanvasGroup tutorial;

		void Awake ()
		{
			// Instantiate levelpart objects
			sublevel01 = GameObject.Find ("Room01");
			sublevel02 = GameObject.Find ("Room02");

			// Instantiate player object and NavMeshAgent
			player = GameObject.FindGameObjectWithTag ("Player");
			playerAgent = player.GetComponent<NavMeshAgent> ();

			// Instantiate main camera object
			mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");

			// Instantiate movement tutorial text
			tutorial = GameObject.Find ("Tutorial").GetComponent<CanvasGroup> ();
		}

		void OnCollisionEnter (Collision col)
		{
			// Stop and reset NavMeshAgent
			playerAgent.Stop ();
			playerAgent.ResetPath ();

			// Set player object to start position in next levelpart
			player.transform.position = new Vector3 (8.5f, 0.5f, 0);

			// Set previous levelpart inactive 
			sublevel01.SetActive (false);

			// Change layer of next levelpart to visible layer
			ChangeLayer (sublevel02, "Default");

			// Set main camera viewport to next sublevel
			MoveCamera ();

			// Set movement tutorial invisible
			tutorial.alpha = 0;
		}

		// Method to change layers of a game object and all its children recursively
		private void ChangeLayer (GameObject obj, string layer)
		{
			if (obj == null) {
				return;
			}
			obj.layer = LayerMask.NameToLayer (layer);

			foreach (Transform child in obj.transform) {
				if (child == null) {
					continue;
				}
				ChangeLayer (child.gameObject, layer);
			}
		}

		// Method to move camera to next levelpart camera position
		private void MoveCamera ()
		{
			mainCamera.transform.position += new Vector3 (15, 0, 0);
		}
	}
}
