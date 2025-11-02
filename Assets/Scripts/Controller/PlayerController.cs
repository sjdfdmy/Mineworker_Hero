using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("玩家属性")]
    public int health = 4;
    public int attack = 1;
    public int maxHealth = 4;

    [Header("UI显示")]
    public Text healthText;
    public Text attackText;

    public float moveSpeed = 5f;
    public float digSpeed = 1f;
    
   

    public Slider timeProgressBar;
   
   
    public float totalTime = 60f; // 总时间
    public int currentLayer = 1;
    public float currentDepth = 0f;
    public float maxDepth = 25f; 

    public Vector2 facingDirection = Vector2.down;
    public SpriteRenderer characterSprite;

    private Rigidbody2D rb;
    private bool isDigging = false;
    private Ore currentOre;

    private float currentTime;
    public  enum CharacterDirection
    {
        Down = 0,
        Left = 1,
        Right = 2,
        Up = 3
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    void Update()
    {
        HandleMovement();
        
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");


        rb.velocity  = new Vector2(horizontal*moveSpeed,rb.velocity.y);
       

        if (Input.GetKeyDown(KeyCode.S))
        {
            facingDirection = Vector2.down;
        }
    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            currentOre = other.GetComponent<Ore>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            if (currentOre == other.GetComponent<Ore>())
            {
                currentOre = null;
            }
        }


        void UpdateGameTime()
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                currentTime = 0;

            }
            // 更新UI时间进度条
            if (timeProgressBar != null)
            {
                timeProgressBar.value = currentTime / totalTime;
            }


        }
    }
}