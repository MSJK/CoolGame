using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class UI : MonoBehaviour {
    Mover playerMover;
    Text chargeText;
	// Use this for initialization
	void Start () {
        playerMover = (Mover)GameObject.Find("Player").GetComponent(typeof(Mover));
        chargeText = (Text)GetComponent(typeof(Text));
	}
	
	// Update is called once per frame
	void Update () {
        chargeText.text = Math.Truncate(Mathf.Clamp(playerMover.JumpCharge, 0, 1) * 100) + "%";
	}
}
