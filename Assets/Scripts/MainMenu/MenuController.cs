using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [Header("Windows")]
    [Tooltip("Window of main menu")]
    [SerializeField] GameObject _mainWindow;
    [Tooltip("Window of selecting devices for players")]
    [SerializeField] GameObject _selectDevicesWindow;

    private void Start()
    {
        _mainWindow.SetActive(true);
        _selectDevicesWindow.SetActive(false);
    }

    public void OpenSelectDevicesWindow()
    {
        _mainWindow.SetActive(false);
        _selectDevicesWindow.SetActive(true);
    }
    
    public void OpenMainMenuWindow()
    {
        _selectDevicesWindow.SetActive(false);
        _mainWindow.SetActive(true);
    }
}
