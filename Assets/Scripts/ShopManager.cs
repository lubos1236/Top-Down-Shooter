using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class ShopManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject weaponParent;


    private bool[] weaponUnlocked = { true, false, false, false, false, false };
    private int[] weaponCost = { 0, 20, 50, 120, 300, 500 };
    [SerializeField]
    private GameObject[] weaponBuyText, weaponsPrefab;

    private void Start()
    {
        weaponParent = GameObject.Find("Weapon");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        UpdateShop();
    }
    public void getWeapon(int weaponIndex)
    {
        if (weaponUnlocked[weaponIndex] == false)
        {
            if (gameManager.getCoins() >= weaponCost[weaponIndex])
            {
                gameManager.DeductCoins(weaponCost[weaponIndex]);
                weaponUnlocked[weaponIndex] = true;
                UpdateShop();
                EquipWeapon(weaponsPrefab[weaponIndex]);
                gameManager.MenuOpening();
            }
        }
        else
        {
            EquipWeapon(weaponsPrefab[weaponIndex]);
            gameManager.MenuOpening();
        }
    }

    private void UpdateShop()
    {
        int index=0;
        foreach (GameObject textObj in weaponBuyText)
        {
            string text = weaponUnlocked[index] == true ? "Use" : weaponCost[index].ToString() + " C";
            textObj.GetComponent<TMP_Text>().text = text;
            index++;
        }
    }
    private void EquipWeapon(GameObject weapon)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weapon,weaponParent.transform);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}
