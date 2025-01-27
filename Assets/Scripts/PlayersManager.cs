using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    InputDevice _deviceForPlayer1 = null;
    string _controlSchemeForPlayer1 = "";
    InputDevice _deviceForPlayer2 = null;
    string _controlSchemeForPlayer2 = "";

    private void Awake()
    {
        if (FindObjectsOfType<PlayersManager>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void SetDevicesAndSchemesForPlayers(InputDevice deviceForPlayer1, string controlSchemeForPlayer1, InputDevice deviceForPlayer2, string controlSchemeForPlayer2)
    {
        _deviceForPlayer1 = deviceForPlayer1;
        _controlSchemeForPlayer1 = controlSchemeForPlayer1;
        _deviceForPlayer2 = deviceForPlayer2;
        _controlSchemeForPlayer2 = controlSchemeForPlayer2;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name!= "Menu")
        {
            List<PlayerInput>players = FindObjectsOfType<PlayerInput>().ToList();

            AssignDeviceToPlayer(players[0], _controlSchemeForPlayer1, _deviceForPlayer1);
            AssignDeviceToPlayer(players[1], _controlSchemeForPlayer2, _deviceForPlayer2);
        }
    }

    public void AssignDeviceToPlayer(PlayerInput player, string controlScheme, InputDevice inputDevice)
    {
        if (controlScheme == Constants.KeyboardControlScheme)
        {
            List<InputDevice> inputDevices = new List<InputDevice>();
            inputDevices.Add(inputDevice);
            foreach (InputDevice device in InputSystem.devices)
            {
                if (device.description.deviceClass == "Mouse")
                {
                    inputDevices.Add(device);
                    break;
                }
            }
            player.SwitchCurrentControlScheme(controlScheme, inputDevices.ToArray());
        }
        else
            player.SwitchCurrentControlScheme(controlScheme, inputDevice);
    }
}
