using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
	#region Singleton

	public static PlayerManager instance;

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogWarning("There than one instance of Inventory found!");
			return;
		}
		instance = this;
	}

	#endregion

	public GameObject player;

	public void KillPlayer()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Debug.Log("KillPlayer = " + SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex));
	}
}
