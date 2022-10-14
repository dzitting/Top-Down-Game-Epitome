using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPortal : Collidable
{
     public string [] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            //Teleport Player
            GameManager.instance.SaveState();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Room");
        }
    }
}
