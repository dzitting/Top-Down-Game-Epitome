using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int healingAmount = 1;
    public int totalAvl = 5;
    private float healCoolDown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
        return;

        if (Time.time - lastHeal > healCoolDown)
        {
            lastHeal = Time.time;
            if (totalAvl > 0)
            {
                GameManager.instance.player.Heal(healingAmount);
                totalAvl--;
            }
            else
            {
                GameManager.instance.ShowText("Fountain Sources Depleted", 20, Color.red , transform.position, Vector3.up * 100, 2.0f);
            }
        }
    }
}
