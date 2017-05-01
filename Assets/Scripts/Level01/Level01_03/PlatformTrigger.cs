using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace Level01_03
{
	/* Class to control platform movement in Level01_03
	 */
	public class PlatformTrigger : MonoBehaviour
	{
		private bool triggerUp = true;
		private bool platformUp = false;
		private bool movePlayer = false;

		private Vector3 startPos;
		private Vector3 endPos;
		private float startTime;
		private float way;

		private Animator platformAnim;
		private GameObject player;
		private NavMeshAgent navAgent;

		void Awake ()
		{
			// Instantiate Animator, player object, and NavMeshAgent
			platformAnim = GameObject.Find ("MeshRoot03").GetComponent<Animator> ();
			player = GameObject.FindGameObjectWithTag ("Player");
			navAgent = player.GetComponent<NavMeshAgent> ();
		}

		void Update ()
		{
			// If boolean movePlayer is set to true,
			// lerp/interpolate player movement to destination point
			if (movePlayer) {
				float traveled = (Time.time - startTime) * 5.0f;
				float frac = Mathf.Clamp (traveled / way, 0.0f, 1.0f);

				Vector3 newPos = Vector3.Lerp (startPos, endPos, frac);
				float distance = Vector3.Distance (endPos, newPos);

				// If remaining distance is greater than .1f, continue interpolation,
				// else set if NavMeshAgent does not exist, 
				// set player position to destination (just to make sure),
				// and add new NavMeshAgent
				if (distance > 0.1f) { 
					player.transform.position = newPos;
				} else {
					if (navAgent == null) {
						player.transform.position = endPos;
						player.AddComponent<NavMeshAgent> ();
						navAgent = player.GetComponent<NavMeshAgent> ();
						navAgent.radius = 0.1f;
					}
				}
			}
		}

		void OnCollisionEnter (Collision col)
		{
			// Move trigger on collision enter		
			MoveTrigger ();

			// If collision with player, elevate Platform
			if (col.collider.gameObject.CompareTag ("Player")) {
				MovePlatform ();

				// trigger interplation of player movement; see Update() method
				if (!movePlayer) {
					startPos = player.transform.position;
					endPos = startPos + new Vector3 (3, 4, 0);
					startTime = Time.time;
					way = Vector3.Distance (startPos, endPos);
				}
				movePlayer = true;
			} 
		}

		void OnCollisionExit (Collision col)
		{
			// Move trigger on collision exit
			MoveTrigger ();
		}

		private void MoveTrigger ()
		{
			float triggerMoveDistance = 0.04f;

			// Move trigger down (pushing the button), if it is up and vice versa
			if (triggerUp) {
				transform.position -= new Vector3 (0, triggerMoveDistance, 0);
				triggerUp = false;
			} else {
				transform.position += new Vector3 (0, triggerMoveDistance, 0);
				triggerUp = true;
			}
		}

		// Method to trigger platform movement animation
		private void MovePlatform ()
		{
			if (platformUp == false) {
				// Remove current NavMeshAgent, to reset later
				Destroy (navAgent);

				// Play platform movement animation
				platformAnim.Play ("movePlatform");
				platformUp = true;

				// Set NavMesh to elevated version
				SetNavMesh ();
			}
		}

		// Method to set NavMesh from standard to elevated version
		private void SetNavMesh ()
		{
			if (SceneManager.GetSceneByName ("Level01_NavMeshScene_DOWN").isLoaded) {
				SceneManager.UnloadSceneAsync ("Level01_NavMeshScene_DOWN");
				SceneManager.LoadScene ("Level01_NavMeshScene_UP", LoadSceneMode.Additive);
			}
		}
	}
}
