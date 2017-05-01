using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Level01_02
{
	/* Class to control levelpart exit in Level01_02
	 */
	public class ExitRoom : MonoBehaviour
	{
		private GameObject player;
		private NavMeshAgent playerAgent;
		private GameObject sublevel02;
		private GameObject sublevel03;
		private GameObject mainCamera;

		void Awake ()
		{
			// Instantiate levelpart objects
			sublevel02 = GameObject.Find ("Room02");
			sublevel03 = GameObject.Find ("Room03");

			// Instantiate player object and NavMeshAgent
			player = GameObject.FindGameObjectWithTag ("Player");
			playerAgent = player.GetComponent<NavMeshAgent> ();

			// Instantiate main camera object
			mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		}

		void OnCollisionEnter (Collision col)
		{
			// Stop and reset NavMeshAgent
			playerAgent.Stop ();
			playerAgent.ResetPath ();

			// Set player object to start position in next levelpart
			player.transform.position = new Vector3 (8.56f, 0.5f, -15);

			// Set previous levelpart inactive 
			sublevel02.SetActive (false);

			// Change layer of next levelpart to visible layer
			ChangeLayer (sublevel03, "Default");

			// Set main camera viewport to next sublevel
			MoveCamera ();

			// Reset NavMesh to standard version
			SetNavMesh ();
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
			mainCamera.transform.position += new Vector3 (0, 0, -15);
		}

		// Method to SetNavMesh from elevated version back to standard
		private void SetNavMesh ()
		{
			if (SceneManager.GetSceneByName ("Level01_NavMeshScene_UP").isLoaded) {
				SceneManager.UnloadSceneAsync ("Level01_NavMeshScene_UP");
				SceneManager.LoadScene ("Level01_NavMeshScene_DOWN", LoadSceneMode.Additive);
			}
		}
	}
}
