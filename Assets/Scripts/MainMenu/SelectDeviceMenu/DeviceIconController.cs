using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

[RequireComponent(typeof(PlayerInput))]
public class DeviceIconController : MonoBehaviour
{
    string _controlScheme = string.Empty;
    InputDevice _device;
    int _chosenPlayer = 0;


    bool _isActive = true;
    int _position = 1;
    bool _checker = true;

    SelectDeviceMenuController _menuController;

    public int Position {
        set { _position = value; }  
        get { return _position; } 
    }

    public void SetMenuController(SelectDeviceMenuController menuController) { _menuController = menuController; }

    public void SetUsingDevice(string newScheme, InputDevice newDevice)
    {
        _controlScheme = newScheme;
        _device = newDevice;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (_menuController == null)
        {
            Debug.LogError("Menu controller is not assigned!!!");
            return;
        }
        if (!_isActive) return; 

        float direction = context.ReadValue<Vector2>().x;

        if (direction == 0f)
        {
            _checker = true;
            return;
        }

        if (!_checker) return;

        _menuController.MoveIcon(this, direction);
        _checker = false;
    }


    public void ConfirmChoice()
    {
        if (!_isActive) return;

        switch (_position)
        {
            case 0:
                _menuController.IsPlayer1Finished = true;
                _menuController.SetDeviceForPlayer(1, _controlScheme, _device);
                _chosenPlayer = 1;
                
            break;

            case 1: return;

            case 2:
                _menuController.IsPlayer2Finished = true;
                _menuController.SetDeviceForPlayer(2, _controlScheme, _device);
                _chosenPlayer = 2;
            break;
        }
        _menuController.ClearArea(this, _position);

        _isActive = false;
    }


    public void RemoveIcon()
    {
        if (GetComponent<PlayerInput>().user.valid)
        {
            GetComponent<PlayerInput>().actions?.Disable();
            GetComponent<PlayerInput>().user.UnpairDevicesAndRemoveUser();
        }
        Destroy(gameObject);
    }


    public void Cancel()
    {
        if (_chosenPlayer == 0)
        {
            _menuController.CloseSelectDeviceWindow();
            return;
        }
        if (_chosenPlayer == 1)
        {
            _menuController.IsPlayer1Finished = false;
        }
        if (_chosenPlayer == 2)
        {
            _menuController.IsPlayer2Finished = false;
        }
        _isActive = true;
    }


}
