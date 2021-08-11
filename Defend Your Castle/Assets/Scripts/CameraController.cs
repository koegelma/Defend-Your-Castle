using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool movement = true;
    private float minY = 15f;
    private float maxY = 70f;
    private float minZ = -40f;
    private float maxZ = 50f;
    private float maxX = 100f;
    private float minX = -40f;
    private float panSpeed = 30f;
    private float panBorderThickness = 10f;
    private float scrollspeed = 5f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            movement = !movement;
        }
        if (!movement)
        {
            return;
        }
        InputControllMovement();
    }

    private void InputControllMovement()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            if (transform.position.z < maxZ) { transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World); }
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            if (transform.position.z > minZ) { transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World); }
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            if (transform.position.x < maxX) { transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World); }
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            if (transform.position.x > minX) { transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World); }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollspeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
