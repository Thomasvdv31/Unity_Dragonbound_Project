using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Animator _animate;
    private BoxCollider2D _boxCollider;

    public float speed;

    private float _direction;
    private bool _hit;
    private float _lifetime;

    private void Awake()
    {
        _animate = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (_hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);

        _lifetime += Time.deltaTime;

        if (_lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _hit = true;
        _boxCollider.enabled = false;
        _animate.SetTrigger("explode");

        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
   

    public void SetDirection(float direction)
    {
        _lifetime = 0;
        _direction = direction;
        gameObject.SetActive(true);
        _hit = false;
        _boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;

        // check if the fireball faces the right direction
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    //Deactive the fireball when the explosion has finished
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
