using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum SelectionResult
    {
        None,
        UpgradeA,
        UpgradeB,
    }
    
    public GameObject TitleText;
    public GameObject SelectText;
    public TextMeshProUGUI SelectWeaponNameText;
    public GameObject NoUpgradeText;
    
    public Button UpgradeButtonA;
    public TextMeshProUGUI UpgradeButtonAText;
    public Button UpgradeButtonB;
    public TextMeshProUGUI UpgradeButtonBText;

    public SelectionResult Selection;
    
    private Canvas Canvas;
    
    public void Start()
    {
        Canvas = GameObject.FindObjectOfType<Canvas>();
        HideAll();
    }
    
    public void HideAll()
    {
        TitleText.gameObject.SetActive(false);
        SelectText.gameObject.SetActive(false);
        SelectWeaponNameText.gameObject.SetActive(false);
        NoUpgradeText.gameObject.SetActive(false);
        UpgradeButtonA.gameObject.SetActive(false);
        UpgradeButtonB.gameObject.SetActive(false);

        Selection = SelectionResult.None;
    }

    public void ShowTitle()
    {
        TitleText.gameObject.SetActive(true);
    }
    
    public void ShowSelectText(String weaponName)
    {
        SelectText.gameObject.SetActive(true);
        SelectWeaponNameText.gameObject.SetActive(true);
        SelectWeaponNameText.text = weaponName;
    }
    
    public void ShowNoUpgradeText()
    {
        NoUpgradeText.gameObject.SetActive(true);
    }

    public void ShowButtons(String upgradeAText, String upgradeBText)
    {
        UpgradeButtonA.gameObject.SetActive(true);
        UpgradeButtonAText.text = upgradeAText;
        UpgradeButtonB.gameObject.SetActive(true);
        UpgradeButtonBText.text = upgradeBText;
    }

    public void OnUpgradeA()
    {
        Selection = SelectionResult.UpgradeA;
    }   
    
    public void OnUpgradeB()
    {
        Selection = SelectionResult.UpgradeB;
    }
}