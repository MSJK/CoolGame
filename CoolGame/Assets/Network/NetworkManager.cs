using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public struct StoreItem
{
    public string name;
    public string id;
    public int pool;
    public int price;
}

public class NetworkManager : MonoBehaviour
{
    private SocketIOComponent socket = null;

    public string Stage { get; private set; }
    public List<StoreItem> Store { get; private set; }
    public int ClientCount { get; private set; }
    public string RoomCode { get; private set; }

    public ProcessStorePurchase ItemBought;

    private bool waitingOnOpen = false;
    
	void Start ()
	{
        DontDestroyOnLoad(gameObject);

        Store = new List<StoreItem>();

	    socket = this.gameObject.GetComponent<SocketIOComponent>();
        socket.On("open", (e) =>
        {
            if (!waitingOnOpen)
                return;

            Debug.Log("Connected to game lobby service, creating game");
            socket.Emit("create game");
            waitingOnOpen = false;
        });
        socket.On("close", (e) =>
        {
            Debug.Log("Disconnected from game lobby service");
        });
        socket.On("bad command", (e) =>
        {
            Debug.LogError("Received bad command message");
        });
        socket.On("game state", OnGameState);
        socket.On("game created", OnGameCreated);
        socket.On("game started", (e) =>
        {
            Debug.Log("Game started on lobby service");
        });
        socket.On("item bought", OnItemBought);

#if UNITY_5_3_OR_NEWER
        SceneManagement.SceneManager.LoadScene("MainMenu");
#else
        Application.LoadLevel("MainMenu");
#endif
    }

    public void Connect()
    {
        if (socket.IsConnected)
            return;

        waitingOnOpen = true;
        Debug.Log("Connecting to game lobby service...");
        socket.Connect();
    }

    public void Disconnect()
    {
        if (!socket.IsConnected)
            return;

        Debug.Log("Disconnecting from game lobby service...");
        socket.Close();
        RoomCode = string.Empty;
        ClientCount = 0;
    }

    public void AddStoreItem(string id, string name, int price)
    {
        var item = new Dictionary<string, JSONObject>();
        item.Add("id", JSONObject.CreateStringObject(id));
        item.Add("name", JSONObject.CreateStringObject(name));
        item.Add("price", new JSONObject(price));
        
        var msg = new Dictionary<string, JSONObject>();
        msg.Add("item", new JSONObject(item));
        msg.Add("roomCode", JSONObject.CreateStringObject(RoomCode));

        socket.Emit("add item", new JSONObject(msg));
    }

    public void StartGame()
    {
        socket.Emit("start game", JSONObject.CreateStringObject(RoomCode));
    }

    public void GameOver()
    {
        socket.Emit("game over", JSONObject.CreateStringObject(RoomCode));
    }

    public void GivePoints(int amount)
    {
        var msg = new Dictionary<string, JSONObject>();
        msg.Add("roomCode", JSONObject.CreateStringObject(RoomCode));
        msg.Add("amount", new JSONObject(amount));
        socket.Emit("add points", new JSONObject(msg));
    }

    // SOCKET EVENTS

    public void OnGameCreated(SocketIOEvent ev)
    {
        try
        {
            RoomCode = ev.data.str;
            Debug.LogFormat("Game created with room code {0}", RoomCode);
        }
        catch(Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void OnGameState(SocketIOEvent ev)
    {
        try
        {
            Stage = ev.data["state"]["stage"].str;
            ClientCount = (int) ev.data["players"].n;
            var store = ev.data["state"]["store"];
            Store.Clear();
            for (var i = 0; i < store.Count; ++i)
            {
                var item = store[i];
                Store.Add(new StoreItem()
                {
                    id = item["id"].str,
                    name = item["name"].str,
                    pool = (int) item["pool"].n,
                    price = (int) item["price"].n
                });
            }

            Debug.LogFormat("stage = {0}, client count = {1}", Stage, ClientCount);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void OnItemBought(SocketIOEvent ev)
    {
        try
        {
            var itemId = ev.data["id"].str;
            Debug.LogFormat("Item was bought: {0}", itemId);
            if (ItemBought != null)
                ItemBought(itemId);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
