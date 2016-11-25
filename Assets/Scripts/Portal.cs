using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Portal : PhilInteractable {

    public string nextLevelName;

	public override void Interact(GameObject interacted)
    {
        SceneManager.LoadScene(nextLevelName);
    }
}

