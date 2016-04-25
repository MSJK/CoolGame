using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    private NetworkManager network;

	// Use this for initialization
	void Start () {
        network = GameObject.Find("SocketIO").GetComponent<NetworkManager>();
	}

    public void CreateGame()
    {
        network.Connect();
    }
}
