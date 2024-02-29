using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardUpdate : MonoBehaviour
{
	void Start()
	{
		// Create a temporary reference to the current scene.
		Scene currentScene = SceneManager.GetActiveScene();

		// Retrieve the name of this scene.
		string sceneName = currentScene.name;

		if (sceneName == "End")
		{
			PlayerManager.isGameCompleted = true;
			PlayerPrefs.DeleteAll();
		}
	}
		
}