using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    private NetworkManager network;
    
    void Start()
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

        // Initial Items
        network.AddStoreItem("screen-shake", "Screen Shake", 300);

        network.StartGame();
    }

    // Update is called once per frame
    void Update () {
	
	}
}
