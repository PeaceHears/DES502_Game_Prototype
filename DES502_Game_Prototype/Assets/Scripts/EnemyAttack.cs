using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Collider2D hitboxCollider;
    public float damage = 1;
    public Animator animator;

    private Vector2 rightAttackOffset;

    private void Start()
    {
        rightAttackOffset = transform.position;
    }

    public void AttackLeft()
    {
        hitboxCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void AttackRight()
    {
        hitboxCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void StopAttack()
    {
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();

            if (player)
            {
                player.animator.SetTrigger("Hurt");
                player.Health -= damage;
            }
        }
    }
}
