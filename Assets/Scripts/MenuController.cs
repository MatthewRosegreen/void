using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
    IEnumerator Start()
    {
        Screen.SetResolution(800, 450, false);
        yield return true;
    }

    void Update()
    {

    }

    private IEnumerator WaitUntilFrameLoad()
    {
        yield return new WaitForEndOfFrame();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void LoadInstructions()
    {
        SceneManager.LoadScene("howtoplay", LoadSceneMode.Single);
    }

	public void LoadCreativeCommonsWebpage()
	{
		Application.OpenURL("https://creativecommons.org/licenses/by/4.0/legalcode");
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}
