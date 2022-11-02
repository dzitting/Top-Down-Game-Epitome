using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
   //Text fields
    public Text levelText, hitpointText, coinsText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform XPBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //If selection is too high on carousel
            if(currentCharacterSelection == GameManager.instance.playerSprites.Count)
            currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            //If selection is too low on carousel
            if(currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

                OnSelectionChanged();
            }
            
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update character info
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLvl];
        if(GameManager.instance.weapon.weaponLvl == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX";        
        }
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLvl].ToString();

        //Meta
        levelText.text = GameManager.instance.GetCurrentLvl().ToString();
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        coinsText.text = GameManager.instance.coins.ToString();

        //XP Bar
        int currLvl = GameManager.instance.GetCurrentLvl();
        if (currLvl == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points"; //Display Total
            XPBar.localScale = Vector3.one;
        }
        else
        {
            int prevLvlXP = GameManager.instance.GetXpToLvl(currLvl - 1);
            int currLvlXP = GameManager.instance.GetXpToLvl(currLvl);

            int diff = currLvlXP - prevLvlXP;
            int currXPIntoLvl = GameManager.instance.experience - prevLvlXP;

            float completionRatio = (float)currXPIntoLvl / (float)diff;
            XPBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXPIntoLvl.ToString() + " / " + diff;
        }
    }
}
