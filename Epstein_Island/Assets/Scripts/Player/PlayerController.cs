using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.8f;

    private CharacterController controller;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        ApplyGravity();
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
    }

    void ApplyGravity()
    {
        // если стоим на земле — прижимаем вниз
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // добавляем гравитацию
        velocity.y += gravity * Time.deltaTime;

        // применяем движение вниз
        controller.Move(velocity * Time.deltaTime);
    }
}