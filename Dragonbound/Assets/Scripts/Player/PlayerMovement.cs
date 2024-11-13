using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private BoxCollider2D _boxCollider;

    private float _wallJumpCooldown;
    private float _horizontalInput;

    public LayerMask _groundLayer;
    public LayerMask _wallLayer;
    public float speed;
    public float jumpSpeed;

    [Header("Coyote timer")]
    public float coyoteTime;
    private float _coyoteCounter;

    [Header("Multi Jumps")]
    public int extraJumps;
    private int _jumpCounter;

    [Header("Wall Jumps")]
    public float wallJumpX;
    public float wallJumpY;

    [Header("sfx")]
    public AudioClip jumpSound;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");
        
        // Flip player model when moving left or right.
        if (_horizontalInput > 0.01f) // moving right
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        } 
        else if(_horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        }


        //Set animator parameters
        _animator.SetBool("run", _horizontalInput != 0); // Set animation run when input is active
        _animator.SetBool("grounded", IsGrounded());

        // Wall jump system
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // set height
        if(Input.GetKeyUp(KeyCode.Space) && _rigidBody.velocity.y > 0)
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rigidBody.velocity.y / 2);
        }

        if (OnWall())
        {
            _rigidBody.gravityScale = 0;
            _rigidBody.velocity = Vector2.zero;
        }
        else
        {
            _rigidBody.gravityScale = 3;
            _rigidBody.velocity = new Vector2(_horizontalInput * speed, _rigidBody.velocity.y);

            if (IsGrounded())
            {
                _coyoteCounter = coyoteTime; // when land on ground reset the counter
                _jumpCounter = extraJumps; // when land on ground reset jump counter back to the amount of extra jumps  you can have.
            }
            else
            {
                _coyoteCounter -= Time.deltaTime; // decrease count when not on the ground.
            }
        }
    }

    private void Jump()
    {
        if(_coyoteCounter <= 0 && !OnWall() && _jumpCounter <= 0) // is counter is less than 0 and not on wall then skip jump
        {
            return;
        }

        SoundManager.instance.PlaySound(jumpSound);

        if (OnWall())
        {
            WallJump();
        }
        else
        {
            if (IsGrounded())
            {
                _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpSpeed);
            }
            else
            {
                if (_coyoteCounter > 0) // when in the air perform another jump
                {
                    _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpSpeed);
                }
                else
                {
                    if(_jumpCounter > 0)
                    {
                        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpSpeed);
                        _jumpCounter--;
                    }
                }
            }
            _coyoteCounter = 0;
        }
        
    }

    private void WallJump()
    {
        _rigidBody.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        _wallJumpCooldown = 0;
    }

    

    private bool IsGrounded()
    {
        // Casts a ray line in a direction of x amount of length that can touch multiple colliders
        // Example: Player is on the egde of something. the player box collider will hit the ground but the ray isn't so set it being not grounded.

        //Layermask: Can filter a specific layer on colliders with a

        // params: Position of the collider, size of the box collider, no rotation, down (under the player), position of the virtual box, layermask
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.1f, _groundLayer);


        return raycastHit.collider != null; // return true or false. if the raycast couldn't find anything beneath the player then it's null -> set false.
    }

    private bool OnWall()
    {
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, _wallLayer);


        return raycastHit.collider != null; 
    }

    public bool CanAttack()
    {
        return _horizontalInput == 0 && IsGrounded() && !OnWall();
    }
}
