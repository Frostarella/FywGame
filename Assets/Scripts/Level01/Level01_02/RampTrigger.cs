using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level01_02
{
	public class RampTrigger : MonoBehaviour
	{

		private bool triggerUp = true;
		private Animator rampAnim;

		void Awake ()
		{
			// Instantiate Animator object for 'ramp'
			rampAnim = GameObject.Find ("MeshRoot02").GetComponent<Animator> ();
		}

		void OnCollisionEnter (Collision col)
		{
			// Move trigger on collision enter	
			MoveTrigger ();

			// If collision with player, move 'ramp'
			if (col.collider.gameObject.CompareTag ("Player")) {
				MoveRamp ();
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

		// Method to play movement animation for 'ramp'
		private void MoveRamp ()
		{
			rampAnim.Play ("moveRamp");

			// Set NavMesh to elevated version
			SetNavMesh ();
		}

		// Method to set NavMesh from standard version to elevated version
		private void SetNavMesh ()
		{
			if (SceneManager.GetSceneByName ("Level01_NavMeshScene_DOWN").isLoaded) {
				SceneManager.UnloadSceneAsync ("Level01_NavMeshScene_DOWN");
				SceneManager.LoadScene ("Level01_NavMeshScene_UP", LoadSceneMode.Additive);
			}
		}
	}
}
