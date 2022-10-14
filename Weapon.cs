using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int damagePoint = 1;
    public float pushForce = 4.5f;

    //Upgrade Section
    public int weaponLvl = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;
    
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > coolDown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if(coll.name == "Player")
            return;
        //Create new damage object and send to fighter
        Damage dmg = new Damage()
        {
            damageAmount = damagePoint,
            origin = transform.position,
            pushForce = pushForce
        };

        coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
}

