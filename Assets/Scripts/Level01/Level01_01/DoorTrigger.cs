using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level01_01
{
	/* Class to control door opening mechanism in Level01_01
	 */
	public class DoorTrigger : MonoBehaviour
	{
		private bool triggerUp = true;
		private Animator doorAnim;
		private GameObject exit;

		void Awake ()
		{
			//load NavMesh for level setup
			SceneManager.LoadScene ("Level01_NavMeshScene_DOWN", LoadSceneMode.Additive);

			// Instantiate Animator and levelpart exit object
			doorAnim = GameObject.Find ("MeshRoot01").GetComponent<Animator> ();
			exit = GameObject.Find ("Exit01");

			// Set levelpart exit inactive; must not be active until door is open
			exit.SetActive (false);
		}


		void OnCollisionEnter (Collision col)
		{
			// Move trigger on collision enter		
			MoveTrigger ();

			// If collision with player, open door
			if (col.collider.gameObject.CompareTag ("Player")) {
				OpenDoor ();
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


		private void OpenDoor ()
		{
			// Open the door to the next levelpart and activate exit	
			doorAnim.Play ("openDoor");
			exit.SetActive (true);
		}
	}
}
