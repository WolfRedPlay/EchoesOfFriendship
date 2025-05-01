using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectDeviceMenuController : MonoBehaviour
{
    [Header("Possible areas for icons")]
    [SerializeField] List<Image> _areas;

    [Space]

    [Header("Icon variables")]
    [Tooltip("Prefab of device icon")]
    [SerializeField] GameObject _deviceIconPrefab;

    [Tooltip("Sprite for keyboard icon")]
    [SerializeField] Sprite keyboardSprite;

    [Tooltip("Sprite for gamepad icon")]
    [SerializeField] Sprite gamepadSprite;




    MenuController _menuController;
    AudioSource _audioSource;

    int _devicesAmount = 0;


    bool _isPlayer1Finished = false;
    bool _isPlayer2Finished = false;

    InputDevice _deviceForPlayer1 = null;
    string _controlSchemeForPlayer1 = "";
    InputDevice _deviceForPlayer2 = null;
    string _controlSchemeForPlayer2 = "";

    public bool IsPlayer1Finished
    {
        set
        {
            _isPlayer1Finished = value;
            if (!value)
            {
                _deviceForPlayer1 = null;
                _controlSchemeForPlayer1 = "";
                _areas[0].color = Color.white;
            }
            else
            {
                _areas[0].color = Color.green;
            }
        }

        get { return _isPlayer1Finished; }
    }

    public bool IsPlayer2Finished
    {
        set
        {
            _isPlayer2Finished = value;
            if (!value)
            {
                _deviceForPlayer2 = null;
                _controlSchemeForPlayer2 = "";
                _areas[2].color = Color.white;
            }
            else
            {
                _areas[2].color = Color.green;
            }
        }

        get { return _isPlayer2Finished; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _menuController = FindAnyObjectByType<MenuController>(FindObjectsInactive.Include);
        if (_menuController == null)
        {
            Debug.LogError("Menu controller was not found");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        _audioSource = GetComponent<AudioSource>();
        _devicesAmount = InputSystem.devices.Count;
        foreach (var device in InputSystem.devices)
        {
            if (device.description.deviceClass == "Mouse") continue;
            if (device.description.deviceClass == "Keyboard")
            {
                CreateDeviceIcon(Constants.KeyboardControlScheme, device, keyboardSprite);
            }
            else
            {
                CreateDeviceIcon(Constants.GamepadControlScheme, device, gamepadSprite);

            }
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_devicesAmount > InputSystem.devices.Count)
        {
            _devicesAmount = InputSystem.devices.Count;
        }
        else if (_devicesAmount < InputSystem.devices.Count)
        {
            if (InputSystem.devices[_devicesAmount].description.deviceClass == "Keyboard")
            {
                CreateDeviceIcon(Constants.KeyboardControlScheme, InputSystem.devices[_devicesAmount], keyboardSprite);

            }
            else
            {
                CreateDeviceIcon(Constants.GamepadControlScheme, InputSystem.devices[_devicesAmount], gamepadSprite);

            }
            _devicesAmount = InputSystem.devices.Count;
        }
        if (_isPlayer1Finished && _isPlayer2Finished)
        {
            FindAnyObjectByType<PlayersManager>().SetDevicesAndSchemesForPlayers(_deviceForPlayer1, _controlSchemeForPlayer1, _deviceForPlayer2, _controlSchemeForPlayer2);
            SceneManager.LoadScene("Level1");
        }
    }


    public void MoveIcon(DeviceIconController icon, float direction)
    {
        icon.Position += (int)Mathf.Sign(direction);

        icon.Position = Mathf.Clamp(icon.Position, 0 ,2);

        if (icon.Position == 0 && _isPlayer1Finished) icon.Position = 1;
        if (icon.Position == 2 && _isPlayer2Finished) icon.Position = 1;

        icon.gameObject.transform.SetParent(_areas[icon.Position].transform);
        _audioSource.Play();
    }

    public void ClearArea(DeviceIconController icon, int areaIndex)
    {
        if (_areas[areaIndex].transform.childCount > 1)
        {
            int iconIndex = icon.transform.GetSiblingIndex();

            for (int i = 0; i < _areas[areaIndex].transform.childCount; i++)
            {
                if (i == iconIndex) continue;
                DeviceIconController iconToMove = _areas[areaIndex].transform.GetChild(i).GetComponent<DeviceIconController>();
                iconToMove.transform.SetParent(_areas[1].transform);
                iconToMove.Position = 1;
            }
        }
    }

    public void SetDeviceForPlayer(int player, string controlScheme, InputDevice device)
    {
        if (player == 1)
        {
            _deviceForPlayer1 = device;
            _controlSchemeForPlayer1 = controlScheme;
        }
        if (player == 2)
        {
            _deviceForPlayer2 = device;
            _controlSchemeForPlayer2 = controlScheme;
        }
    }

    public void CloseSelectDeviceWindow()
    {
        foreach (Image area in _areas) {
            foreach (Transform child in area.transform)
            {
                DeviceIconController icon;
                if(child.TryGetComponent(out icon))
                {
                    icon.RemoveIcon();
                }
                else
                {
                    Destroy(child.gameObject);
                }
            }
        }
        _menuController.OpenMainMenuWindow();
    }

    private void CreateDeviceIcon(string controlScheme, InputDevice device, Sprite sprite)
    {
        GameObject newIcon = Instantiate(_deviceIconPrefab, _areas[1].transform);
        newIcon.GetComponent<PlayerInput>().SwitchCurrentControlScheme(controlScheme, device);
        newIcon.GetComponent<DeviceIconController>().SetUsingDevice(controlScheme, device);
        newIcon.GetComponent<DeviceIconController>().SetUsingDevice(controlScheme, device);
        newIcon.GetComponent<DeviceIconController>().SetMenuController(this);
        newIcon.GetComponent<Image>().sprite = sprite;
    }
}
