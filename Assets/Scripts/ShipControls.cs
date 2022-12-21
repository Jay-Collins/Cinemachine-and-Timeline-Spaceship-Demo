using UnityEngine;

public class ShipControls : MonoBehaviour
{
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _currentSpeed;
    private float _vertical;
    private float _horizontal;
    [SerializeField] private float _maxRotate;

    void Start()
    {
        _currentSpeed = 1;
    }

    private void Update()
    {
        ShipMovement();
    }

    private void ShipMovement()
    {
        _vertical = Input.GetAxis("Vertical");
        _horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.T))
        {
            _currentSpeed++;
            if (_currentSpeed > 4)
            {
                _currentSpeed = 4;
            }
        }//increase speed

        if (Input.GetKeyDown(KeyCode.G))
        {
            _currentSpeed--;
            if (_currentSpeed < 1)
            {
                _currentSpeed = 1;
            }
        }//decrease speed

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 rotateYaw = new Vector3(0, 1, 0); 
            transform.Rotate(rotateYaw * _rotSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            Vector3 rotateYaw = new Vector3(0, -1, 0); 
            transform.Rotate(rotateYaw * _rotSpeed * Time.deltaTime);
        }

        Vector3 rotateV = new Vector3(_vertical, 0, 0);
        transform.Rotate(rotateV * _rotSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, 0, -_horizontal * 0.2f), Space.Self);

        transform.position += transform.forward * _currentSpeed * Time.deltaTime;
    }
}
