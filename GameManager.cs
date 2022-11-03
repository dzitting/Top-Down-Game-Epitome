using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    //Logic
    public int coins;
    public int experience;

    //Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade Weapon
    public bool TryUpgradeWeapon()

    {
        //Is weapon max level?
        if (weaponPrices.Count <= weapon.weaponLvl)
        {
            return false;
        }

        if (coins >= weaponPrices[weapon.weaponLvl])
        {
            coins -= weaponPrices[weapon.weaponLvl];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    //Experience system
    public int GetCurrentLvl()
    {
        int r = 0;
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count) // If maxed level
            {
                return r;
            }
        }

        return r;
    }
    
    public int GetXpToLvl(int lvl)
    {
        int r = 0;
        int xp = 0;

        while (r < lvl)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXP(int xp)
    {
        int currLvl = GetCurrentLvl();
        experience += xp;
        if(currLvl < GetCurrentLvl())
        {
            OnLevelUp();
        }
    }

    public void OnLevelUp()
    {
        Debug.Log("levelUp");
        player.OnLevelUp();
    }
    //Save state
    /*
     * INT perferredSkin
     * INT coins
     * experience
     * weaponLvl
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + " | ";
        s += coins.ToString() + " | ";
        s += experience.ToString() + " | ";
        s += weapon.weaponLvl.ToString();

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        coins = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        player.SetLevel(GetCurrentLvl());
        //Change weapon lvl and sprite
        weapon.SetWeaponLvl(int.Parse(data[3]));

        Debug.Log("LoadState");

        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
}
