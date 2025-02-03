using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D hitboxCollider;
    public float damage = 2;

    private Vector2 rightAttackOffset;

    private void Awake()
    {
        rightAttackOffset = transform.localPosition;
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
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.animator.SetTrigger("Hurt");
                enemy.Health -= damage;
            }
        }
    }
}
