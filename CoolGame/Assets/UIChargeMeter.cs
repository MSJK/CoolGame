using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIChargeMeter : MonoBehaviour {
    [SerializeField]
    float meterMaxHeight;
    [SerializeField]
    float meterWidth;
    Mover playerMover;
    Image chargeBar;
    // Use this for initialization
    void Start()
    {
        playerMover = (Mover)GameObject.Find("Player").GetComponent(typeof(Mover));
        chargeBar = (Image)GetComponent(typeof(Image));
    }

    // Update is called once per frame
    void Update()
    {
        chargeBar.rectTransform.sizeDelta = new Vector2(meterWidth,Mathf.Clamp(playerMover.JumpCharge, 0, 1) * meterMaxHeight);
    }
}
