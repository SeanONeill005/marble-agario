using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private const float SCALE_REDUCTION = 0.003f;

    public float speed = 0;
    private Rigidbody rb;
    public GameObject winTextObject;
    private float movementX;
    private float movementY;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

        var playerMass = gameObject.GetComponent<Rigidbody>().mass;
        var playerScale = gameObject.transform.localScale;
        playerMass -= SCALE_REDUCTION;
        playerScale -= new Vector3(SCALE_REDUCTION, SCALE_REDUCTION, SCALE_REDUCTION);
        gameObject.GetComponent<Rigidbody>().mass = playerMass;
        gameObject.transform.localScale = playerScale;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            winTextObject.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyScale = collision.gameObject.transform.localScale;
            var playerScale = gameObject.transform.localScale;
            var EnemyMass = collision.gameObject.GetComponent<Rigidbody>().mass;
            var PlayerMass = gameObject.GetComponent<Rigidbody>().mass;

            if (PlayerMass > EnemyMass)
            {
                PlayerMass += EnemyMass;
                gameObject.GetComponent<Rigidbody>().mass = PlayerMass;
                playerScale += enemyScale;
                gameObject.transform.localScale = playerScale;
                Destroy(collision.gameObject);
            }
            else if (EnemyMass > PlayerMass)
            {
                EnemyMass += PlayerMass;
                collision.gameObject.GetComponent<Rigidbody>().mass = EnemyMass;
                enemyScale += playerScale;
                collision.gameObject.transform.localScale = enemyScale;
                Destroy(gameObject);
                winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
                winTextObject.SetActive(true);
            }
        }
    }



}
