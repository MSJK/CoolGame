using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    private NetworkManager network;

	// Use this for initialization
	void Start ()
	{
	    var socketIO = GameObject.Find("SocketIO");
	    if (socketIO == null)
	    {
	        Debug.LogError("Could not find SocketIO object. Make sure you run the game from the 'Initialization' scene!!!");
	        return;
	    }

        network = socketIO.GetComponent<NetworkManager>();
	    if (network == null)
	    {
            Debug.LogError("Could not find NetworkManager component. Make sure you run the game from the 'Initialization' scene!!!");
	        return;
	    }
	}

    public void CreateGame()
    {
        network.Connect();
        SceneManager.LoadScene("GameLobby");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
