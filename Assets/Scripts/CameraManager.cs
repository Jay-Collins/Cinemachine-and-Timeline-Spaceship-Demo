using System.Collections;
using Cinemachine;
using UnityEngine;
public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject _cockpit;
    [SerializeField] private GameObject _ship;
    [SerializeField] private GameObject[] _shipCameras;
    [SerializeField] private GameObject[] _idleCameras;

    private bool _idleCamRunning;
    private float _idleTimer = 15f;
    private bool _idleMouse;
    private bool _idleKeys;
    private bool _idleCam;
    private int _shipCamSelector = 1;
    private int _idleCamSelector;
    
    private void Update()
    {
        PlayerInput();
        ShipOrCockpitSelector();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _shipCamSelector++;
            
            if (_shipCamSelector > 1)
            {
                _shipCamSelector = 0;
            }
            ShipCameraSetter();
        }
        
        // player lack of input
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        _idleMouse = mouseMovement.magnitude == 0;
        _idleKeys = !Input.anyKeyDown && !Input.anyKey;

        if (_idleKeys && _idleMouse)
        {
            if (_idleTimer > 0)
            {
                _idleTimer -= Time.deltaTime;
                _idleCam = false;
                StopCoroutine(IdleCameraCycle());
                _idleCamRunning = false;
                ShipCameraSetter();
            }
            else
            {
                _idleCam = true;
                
                if (!_idleCamRunning)
                {
                    StartCoroutine(IdleCameraCycle());
                }
            }
        }
        else
        {
            _idleTimer = 5f;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void ShipCameraPriorityReset()
    {
        foreach (var camera in _shipCameras)
        {
            camera.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        }
    }

    private void ShipCameraSetter()
    {
        ShipCameraPriorityReset();
        _shipCameras[_shipCamSelector].GetComponent<CinemachineVirtualCamera>().Priority = 15;
    }

    private void ShipOrCockpitSelector()
    {
        switch (_shipCamSelector)
        {
            case 0:
                _ship.SetActive(false);
                _cockpit.SetActive(true);
                break;
            case 1:
                _ship.SetActive(true);
                _cockpit.SetActive(false);
                break;
        }
    }

    private IEnumerator IdleCameraCycle()
    {
        _idleCamRunning = true;
        _shipCamSelector = 1;
        _idleCamSelector = 0;
        
        ShipCameraPriorityReset();
        
        while (_idleCam)
        {
            foreach (var idleCam in _idleCameras)
            {
                if (idleCam.TryGetComponent(out CinemachineVirtualCamera vCam))
                {
                    vCam.Priority = 10;
                }
                else if (idleCam.TryGetComponent(out CinemachineBlendListCamera blendCam))
                {
                    blendCam.Priority = 10;
                }
            }
            
            if (_idleCameras[_idleCamSelector].TryGetComponent(out CinemachineVirtualCamera vCamSet))
            {
                vCamSet.Priority = 15;
                if (vCamSet.Follow != null && vCamSet.Follow.transform.TryGetComponent(out CinemachineDollyCart cart))
                {
                    cart.m_Position = 0 ;
                }
            }
            else if (_idleCameras[_idleCamSelector].TryGetComponent(out CinemachineBlendListCamera blendCamSet))
            {
                blendCamSet.Priority = 15;
            }

            yield return new WaitForSeconds(5);
            _idleCamSelector++;
            if (_idleCamSelector >= _idleCameras.Length)
            {
                _idleCamSelector = 0;
            }
        }
        
        ShipCameraSetter();
        _idleCamRunning = false;
    }
}