using UnityEngine;

public class Movement : MonoBehaviour
{
    private float _mousePositionX;
    [SerializeField] private float _speed;
    public float Speed { get 
        { return _speed; } 
        set 
        { _speed = value; } }


    private void Update()
    {

        transform.Rotate(0f, 0f, 300f * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            _mousePositionX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float newPosition = Input.mousePosition.x - _mousePositionX;

            transform.position += new Vector3(newPosition, 0f, 0f) * Time.deltaTime * _speed;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, -20f, 20f),
                transform.position.y, 
                transform.position.z);

            _mousePositionX = Input.mousePosition.x;
            
        }


    }


}
