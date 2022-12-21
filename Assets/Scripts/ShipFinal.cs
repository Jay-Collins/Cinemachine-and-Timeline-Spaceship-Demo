using System.Collections.Generic;
using System.Data.Common;
using System.Numerics;
using UnityEngine;

public class ShipFinal : MonoBehaviour
{
    private int _switcher;

    [SerializeField] private GameObject[] _idleCameras;
    [SerializeField] private GameObject[] _shipCameras;
    [SerializeField] private GameObject _cockpit;
    [SerializeField] private GameObject _ship;
    private float _idleTimer = 5f;
    private bool _idleMouse;
    private bool _idleKeys;
    private bool _idleCam;
    private int _shipCamSelector;
    private int _idleCamSelector;

    private void Update()
    {
        ViewSwitcher();
        IdleCheck();
        IdleCamera();
    }

    private void ViewSwitcher()
    {
        if (!Input.GetKeyDown(KeyCode.R))
        {
            foreach (var camera in _shipCameras)
            {
                if (_shipCamSelector == _switcher)
                {
                    camera.SetActive(true);
                }
                else
                {
                    camera.SetActive(false);
                }
                _shipCamSelector++;
            }
            _switcher++;
            _shipCamSelector = 0;
        }

        switch (_switcher)
        {
            case 0:
                _cockpit.SetActive(false);
                _ship.SetActive(true);
                break;
            case 1:
                _ship.SetActive(false);
                _cockpit.SetActive(true);
                break;
            case 2:
                _switcher = 0;
                break;
        }
    }

    private void IdleCheck()
    {
        var mouseMovement = new UnityEngine.Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _idleMouse = mouseMovement.magnitude == 0;
        _idleKeys = !Input.anyKeyDown;

        if (_idleKeys && _idleMouse)
        {
            if (_idleTimer > 0)
            {
                _idleTimer -= Time.deltaTime;
                _idleCam = false;
            }
            else
            {
                _idleCam = true;
            }
        }
        else
        {
            _idleTimer = 5f;
        }
    }

    private void IdleCamera()
    {
        if (_idleCam)
        {
            
        }
        else
        {
            
        }
    }
}
