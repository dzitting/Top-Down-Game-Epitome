using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
   //Text fields
    public Text levelText, hitpointText, pesosText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform XPBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
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
            if(currentCharacterSelection = 0)
            {
                currentCharacterSelection == GameManager.instance.playerSprites.Count - 1;

                OnSelectionChanged();
            }
            
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        //Reference to current weapon
    }

    //Update character info
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprite[0];
        upgradeCostText.text = "NOT IMPLEMENTED";

        //Meta
        levelText.text = "NOT IMPLEMENTED";
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        pesosText.Text = GameManager.instance.pesos.ToString();

        //XP Bar
        xpText.text = "NOT IMPLEMENTED";
        XPBar.localScale = new Vector3(0.5f, 0, 0);
    }
}
