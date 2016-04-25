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
    public int cost;
}

public class NetworkManager : MonoBehaviour
{

    private SocketIOComponent socket = null;

    public string Stage { get; set; }
    public List<StoreItem> Store { get; set; }
    public int ClientCount { get; set; }
    
	void Start ()
	{
        Store = new List<StoreItem>();

	    socket = this.gameObject.GetComponent<SocketIOComponent>();
        socket.On("game state", OnGameState);

	    StartCoroutine(StartAfterSeconds(5));
	}

    IEnumerator StartAfterSeconds(float amount)
    {
        yield return new WaitForSeconds(amount);
        StartGame();
    }

    public void StartGame()
    {
        socket.Emit("start game");
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
