using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int[] damagePoint = {1, 2, 3, 4, 5};
    public float[] pushForce = {2.0f, 2.2f, 3.5f, 4.1f, 4.7f};

    //Upgrade Section
    public int weaponLvl = 1;
    private SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float coolDown = 0.5f;
    private float lastSwing;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
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
            damageAmount = damagePoint[weaponLvl],
            origin = transform.position,
            pushForce = pushForce[weaponLvl]
        };

        coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLvl++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];
    }

    public void SetWeaponLvl(int lvl)
        {
            weaponLvl = lvl;
            spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];
        }
}

