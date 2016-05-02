using UnityEngine;
using System.Collections;

public delegate void ProcessStorePurchase(string itemId);

public class GameManager : MonoBehaviour {
    private NetworkManager network;

    public event ProcessStorePurchase ItemBought;

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
        network.AddStoreItem("flicker", "Flicker", 500);
        network.AddStoreItem("noise", "Noise", 500);
        network.AddStoreItem("crt", "CRT", 150);

        network.ItemBought += OnItemBought;

        network.StartGame();
    }

    void OnDestroy()
    {
        if (network != null && network.ItemBought != null) network.ItemBought -= OnItemBought;
    }

    void GameOver()
    {
        if (network != null)
            network.GameOver();
    }

    void GivePoints(int amount)
    {
        if (network != null)
            network.GivePoints(amount);
    }

    // EVENT HANDLING

    public void OnItemBought(string itemId)
    {
        if (ItemBought != null)
            ItemBought(itemId);
    }
}
