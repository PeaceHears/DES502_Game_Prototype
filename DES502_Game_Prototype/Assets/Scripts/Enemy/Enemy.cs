using Globals;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ENEMY_TYPE type;
    public ROOM_TYPE[] roomTypes;
    public float health = 5;
    public int count = 5;
    public float moveSpeed = 1;
    public EnemyAttack attack;
    public float collisionOffset = 0.01f;
    public ContactFilter2D movementFilter;
    public Animator animator;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    public float Health
    {
        set
        {
            health = value;

            if (health <= 0)
            {
                Defeated();
            }
        }
        get
        {
            return health;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateTargetDirection();
        Move();
        RotateTowardsTarget();

        if(_playerAwarenessController.NearbyPlayer)
        {
            AnimateAttack();
        }
    }

    private void UpdateTargetDirection()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
        else
        {
            _targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if (_targetDirection == Vector2.zero)
        {
            return;
        }

        if (_targetDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (_targetDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Move()
    {
        // If movement input is not 0, try to move
        if (_targetDirection != Vector2.zero)
        {
            bool success = TryMove(_targetDirection);

            if (!success)
            {
                success = TryMove(new Vector2(_targetDirection.x, 0));
            }

            if (!success)
            {
                success = TryMove(new Vector2(0, _targetDirection.y));
            }

            animator.SetBool("isWalking", success);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Check for potential collisions
            int count = _rigidbody.Cast(
                direction, // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
                movementFilter, // The settings that determine where a collision can occur on such as layers to collide with
                castCollisions, // List of collisions to store the found collisions into after the Cast is finished
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset

            if (count == 0)
            {
                _rigidbody.MovePosition(_rigidbody.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                AnimateAttack();
                return false;
            }
        }
        else
        {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    public void StartAttack()
    {
        if (spriteRenderer.flipX == true)
        {
            attack.AttackLeft();
        }
        else
        {
            attack.AttackRight();
        }
    }

    public void EndAttack()
    {
        attack.StopAttack();
    }

    public void Defeated()
    {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void AnimateAttack()
    {
        animator.SetTrigger("Attacked");
    }
}
