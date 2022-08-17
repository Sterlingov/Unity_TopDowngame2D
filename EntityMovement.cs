using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float chaseRadius;
    [SerializeField] private float attackRadius;
    Vector2 direction;
    Vector2 movement;
    private LayerMask whatIsPlayer;
    private Transform target;
    private Animator animator;
    private bool isInChaseRange;
    private bool isInAttackRange;
    private Rigidbody2D rb;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        direction = target.position - transform.position;
        direction.Normalize();
        movement = direction;
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);

    }

    void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }

        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }

    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + dir * speed * Time.deltaTime);
    }

}
