using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    protected int facingDir = 1;
    protected bool facingRight = true;

    [Header("Collision info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool isGrounded;


    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponentInChildren<Animator>();
    }

    protected virtual void Update ()
    {
        CollisionChecks();
    }

    protected virtual void CollisionChecks()
    {
        // Cast a ray against colliders in the scene (gameObject position, direction, max distance, LayerMask)
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }


    protected virtual void Flip()
    {
        // facingDir sera multiplié par -1 à chaque appel, ce qui aura pour effet de changer la direction automatiquement ( -1 * -1 = 1) 
        facingDir = facingDir * -1;

        // On inverse le booléen
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    protected virtual void OnDrawGizmos()
    {
        // Drawing a line to the feet of the character
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
}
