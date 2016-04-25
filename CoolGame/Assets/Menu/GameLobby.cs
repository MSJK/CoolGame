using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLobby : MonoBehaviour {
    private NetworkManager network;

    public Text RoomCodeText;
    public Text PlayerCountText;

    // Use this for initialization
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

        network.Connect();
    }

    public void Quit()
    {
        network.Disconnect();
        SceneManager.LoadScene("MainMenu");
    }
    
    void Update ()
    {
        if (network == null)
            return;

        RoomCodeText.text = string.IsNullOrEmpty(network.RoomCode) ? "Connecting..." : network.RoomCode;
        PlayerCountText.text = string.Format("Players: {0}", network.ClientCount);
    }
}
