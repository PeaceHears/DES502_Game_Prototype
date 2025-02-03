using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider2D hitboxCollider;
    public float damage = 2;

    Vector2 rightAttackOffset;

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
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy)
            {
                enemy.Health -= damage;
            }
        }
    }
}
