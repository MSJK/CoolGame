using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine.SceneManagement;

public struct StoreItem
{
    public string name;
    public string id;
    public int pool;
    public int cost;
}

public class NetworkManager : MonoBehaviour
{

    private SocketIOComponent socket = null;

    public string Stage { get; set; }
    public List<StoreItem> Store { get; set; }
    public int ClientCount { get; set; }
    public string RoomCode { get; set; }
    
	void Start ()
	{
        DontDestroyOnLoad(gameObject);

        Store = new List<StoreItem>();

	    socket = this.gameObject.GetComponent<SocketIOComponent>();
        socket.On("connect", (e) =>
        {
            socket.Emit("create game");
        });
        socket.On("game state", OnGameState);
        socket.On("game created", OnGameCreated);

	    SceneManager.LoadScene("MainMenu");
	}

    public void Connect()
    {
        if (socket.IsConnected)
            return;

        socket.Connect();
    }

    public void Disconnect()
    {
        if (!socket.IsConnected)
            return;

        socket.Close();
    }

    public void StartGame()
    {
        socket.Emit("start game");
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
            Stage = ev.data["stage"].str;
            ClientCount = (int) ev.data["clients"].n;
            var store = ev.data["store"];
            Store.Clear();
            for (var i = 0; i < store.Count; ++i)
            {
                var item = store[i];
                Store.Add(new StoreItem()
                {
                    id = item["id"].str,
                    name = item["name"].str,
                    pool = (int) item["pool"].n,
                    cost = (int) item["cost"].n
                });
            }

            Debug.LogFormat("stage = {0}, client count = {1}", Stage, ClientCount);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
