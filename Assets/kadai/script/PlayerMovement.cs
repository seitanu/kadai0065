using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 90f;

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.W))
            moveInput = 1f;
        else if (Input.GetKey(KeyCode.S))
            moveInput = -1f;

        // 前方方向に移動
        transform.position += transform.forward * moveInput * moveSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        float yaw = 0f;
        float pitch = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
            yaw = -1f;
        else if (Input.GetKey(KeyCode.RightArrow))
            yaw = 1f;

        if (Input.GetKey(KeyCode.UpArrow))
            pitch = -1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            pitch = 1f;

        // 回転処理（Y軸：左右、X軸：上下）
        transform.Rotate(pitch * rotationSpeed * Time.deltaTime, yaw * rotationSpeed * Time.deltaTime, 0f, Space.Self);
    }
}