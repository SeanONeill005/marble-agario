using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);

            var enemyScale = collision.gameObject.transform.localScale;
            var playerScale = gameObject.transform.localScale;
            if (playerScale.x > enemyScale.x)
            {
                playerScale += enemyScale;
                gameObject.transform.localScale = playerScale;
            }

            var EnemyMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            var PlayerMass = gameObject.GetComponent<Rigidbody>().mass;

            if (PlayerMass > EnemyMass)
            {
                PlayerMass += EnemyMass;
                gameObject.GetComponent<Rigidbody>().mass = PlayerMass;
            }
        }
    }



}
