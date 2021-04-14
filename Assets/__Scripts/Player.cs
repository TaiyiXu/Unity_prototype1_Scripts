using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Player : MonoBehaviour
{
    //Health
    protected int currentHealth;
    public int maxHealth;
    public int Health
    {
        get => currentHealth;
        set
        {
            if (currentHealth < maxHealth)
            {
                currentHealth += value;
            }
        }
    }

    //*****************************************
    //Experience
    public int exp;

    // Player Level
    public int level=1;
    //*****************************************


    //Health Text
    protected TextMeshProUGUI healthText;
    public Canvas canvas;
    protected Canvas canvasObj;

    //Animator
    protected Animator animator;

    //*****************************************
    //Attack
    public int attack;
    //*****************************************
    
    //Speed
    protected float speed;
    public float normalSpeed;
    public float boostSpeed;


    //Invincible 
    public float timeInvincible;
    protected bool isInvincible;
    protected float invincibleTimer;

    //Move Direction
    protected float horizontal;
    protected float vertical;

    //Mouse direction(Look Direction)
    protected Vector2 lookDirection;

    //Projectile Prefab
    public GameObject projectilePrefab;

    //Read Rigidbody
    protected Rigidbody2D rigidbody2d;

    //Sprite Renderer
    protected SpriteRenderer spRenderer;
    public float flashSpeed;

    protected virtual void Start()
    {
        //Init HP Text Display
        canvasObj = Instantiate(canvas);
        healthText = canvasObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        currentHealth = maxHealth;
        SetHealthText();

        //Set Speed
        speed = normalSpeed;

        //Get some components
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spRenderer = GetComponent<SpriteRenderer>();

        DontDestroyOnLoad(gameObject.transform);
    }

    protected void SetHealthText()
    {

        healthText.text = "HP: " + Health.ToString();
    }

    //*****************************************
    protected int PlayerCurrentLevel()
    {
        return level;
    }

    public void gainExp()
    {
        exp ++;
        Debug.Log("gained 1 exp");
    }
    //*****************************************

    //*****************************************
    public void levelUp()
    {
        if (exp % 3 == 0)
        {

            level++;
            speed++;
            //stamina++;
            attack++;
        }

        Debug.Log("Level Up");
    }
    //*****************************************

    //*****************************************
    public int getAttack()
    {
        return attack;
    }
    //*****************************************


    protected virtual void Update()
    {
        //Get user input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Get mouse position, transfer it to look direction
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookDirection = worldMousePos - transform.position;
        lookDirection.Normalize();

        //Clean player model and HP Text
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            Destroy(this.gameObject);
            Destroy(this.canvasObj.gameObject);
        }

        //Set invincible State
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
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

    }

    protected virtual void FixedUpdate()
    {
        //Get current position and set to new position
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    protected IEnumerator Flash(float x)
    {
        for (int i = 0; i < 5; i++)
        {
            spRenderer.enabled = false;
            yield return new WaitForSeconds(x);
            spRenderer.enabled = true;
            yield return new WaitForSeconds(x);
        }
    }
}