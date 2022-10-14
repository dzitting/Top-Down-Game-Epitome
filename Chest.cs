using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    public int coinAmount = 10;

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            //+ Coins text
            GameManager.instance.ShowText("+" + coinAmount + " coins!", 20, Color.yellow, transform.position, Vector3.up * 25, 1.25f);
        }
    }
}
