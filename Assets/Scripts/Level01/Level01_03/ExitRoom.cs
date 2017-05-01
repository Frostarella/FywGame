using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Level01_03
{
	/* Class to control Level01 exit and demo/tutorial end
	 */
	public class ExitRoom : MonoBehaviour
	{
		private CanvasGroup endScreen;
		private GameObject player;

		void Awake ()
		{
			// Instantiate endscreen UI
			endScreen = GameObject.Find ("EndScreen").GetComponent<CanvasGroup> ();
			endScreen.alpha = 0;

			//Instantiate player object
			player = GameObject.FindGameObjectWithTag ("Player");
		}

		void OnCollisionEnter (Collision col)
		{
			// Show endscreen and remove player object
			endScreen.alpha = 1;
			endScreen.blocksRaycasts = true;
			Destroy (player);
		}
	}
}