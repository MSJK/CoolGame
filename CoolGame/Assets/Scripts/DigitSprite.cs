using UnityEngine;
using System.Collections;

public class DigitSprite : MonoBehaviour {

    //SpriteArray Vars
    private SpriteRenderer thisRenderer;
    public Sprite[] sprites = new Sprite[2];
    public int spriteIndex;

    //Cycle Vars
    private static float Cycle_Interval = 0.5f;
    public static bool Is_Cycling = false;

	// Use this for initialization
	void Start ()
    {

        //Choose random int, either 0 or 1
		int spriteIndex = Random.Range (0, 2);

        //get this object's Sprite Renderer component
        thisRenderer = this.GetComponent<SpriteRenderer>();

        //set the sprite to one of the elements of the sprite array
        thisRenderer.sprite = sprites[spriteIndex];

        StartCoroutine(CycleSprite());
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator CycleSprite()
    {
        for(;;)
        {
            if(Is_Cycling)
            {
                spriteIndex = (spriteIndex + 1) % 2;
                thisRenderer.sprite = sprites[spriteIndex];
            }

            yield return new WaitForSeconds(Cycle_Interval);
        }

    }
}
