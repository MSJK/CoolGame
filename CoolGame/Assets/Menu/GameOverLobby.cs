using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverLobby : MonoBehaviour {
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

        network.GameOver();
    }

    public void Quit()
    {
        network.Disconnect();
#if UNITY_5_3_OR_NEWER
        SceneManagement.SceneManager.LoadScene("MainMenu");
#else
        Application.LoadLevel("MainMenu");
#endif
    }

    public void StartGame()
    {
#if UNITY_5_3_OR_NEWER
        SceneManagement.SceneManager.LoadScene("Game");
#else
        Application.LoadLevel("Game");
#endif
    }

    void Update()
    {
        if (network == null)
            return;

        RoomCodeText.text = string.IsNullOrEmpty(network.RoomCode) ? "Connecting..." : network.RoomCode;
        PlayerCountText.text = string.Format("Players: {0}", network.ClientCount);
    }
}
