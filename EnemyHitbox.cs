using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage = 1;
    public float pushForce = 3.5f;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            //Create a new damage object before sending it to fighter
              Damage dmg = new Damage()
        {
            damageAmount = damage,
            origin = transform.position,
            pushForce = pushForce
        };

        coll.SendMessage("ReceiveDamage", dmg);
        }
        
    }
}
