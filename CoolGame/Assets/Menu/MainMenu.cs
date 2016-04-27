using UnityEngine;
using System.Collections;

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

#if UNITY_5_3_OR_NEWER
        SceneManagement.SceneManager.LoadScene("GameLobby");
#else
        Application.LoadLevel("GameLobby");
#endif
    }

    public void Quit()
    {
        Application.Quit();
    }
}
