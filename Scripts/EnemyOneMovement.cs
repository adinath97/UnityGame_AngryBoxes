using UnityEngine;

public class EnemyOneMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    public static bool stopMovement, beginGame;
    
    private Rigidbody2D myRigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        stopMovement = false;
        beginGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(stopMovement) { 
            this.GetComponent<Animator>().enabled = false;
            myRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            return; 
        }
        if(!beginGame) { return; }
        Move();
    }

    private bool IsFacingRight() {
        return transform.localScale.x < 0;
    }

    private void Move() {
        if(IsFacingRight()) {
            myRigidbody.velocity = new Vector2(moveSpeed,0f);
        }
        else {
            myRigidbody.velocity = new Vector2(-moveSpeed,0f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2((Mathf.Sign(myRigidbody.velocity.x)),1f);
    }
}
