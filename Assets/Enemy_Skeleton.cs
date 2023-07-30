using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy_Skeleton : Entity
{
    bool isAttacking;

    [Header("Move info")]
    [SerializeField] private float moveSpeed;

    [Header("Player detection")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.velocity = new Vector2(moveSpeed* 1.5f * facingDir, rb.velocity.y);
                Debug.Log("i can see the player");
                isAttacking = false;
            }
            else
            {
                Debug.Log("Attack !" + isPlayerDetected);
                isAttacking = true;
            }
        }

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }

        rb.velocity = new Vector2(moveSpeed * facingDir, rb.velocity.y);


    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * facingDir, whatIsPlayer);
    }
}
