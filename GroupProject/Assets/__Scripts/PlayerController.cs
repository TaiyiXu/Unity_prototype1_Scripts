using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public int health { get { return currentHealth; } }
    int currentHealth;
    public int maxHealth = 5;
    public TextMeshProUGUI healthText;

    public Camera cam;

    public float speed;
    public float normalSpeed = 3.0f;
    public float runSpeed = 6.0f;


    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public GameObject projectilePrefab;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Vector2 lookDirection;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        SetHealthText();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (currentHealth <= 0)
            Destroy(gameObject);

        if (Input.GetMouseButtonDown(0))
        {
            Launch();
        }

        //Player Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        //Player Conversention with NPC trigger
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        //Get mouse position, transfer it to look direction
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = worldMousePos - transform.position;
        lookDirection.Normalize();
        //Debug.Log(lookDirection);

    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        SetHealthText();
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    //Launch projectile
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

    }

    KeyCode getKeyCode()
    {
        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                return kcode;
            }
        }
        return KeyCode.None;
    }

    void SetHealthText()
    {
        healthText.text = "HP: " + health.ToString();
    }
}
