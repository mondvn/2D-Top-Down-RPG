using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private bool stopMovingWhiteAttacking = false;

    private bool canAttack = true;

    private enum State
    {
        Roaming,
        Attacking,
    }
    private State state;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        this.enemyPathfinding = GetComponent<EnemyPathfinding>();
        this.state = State.Roaming;
    }

    private void Start()
    {
        this.roamPosition = this.GetRoamingPosition();
    }

    private void Update()
    {
        this.MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (this.state)
        {
            default:
            case State.Roaming:
                this.Roaming();
                break;
            case State.Attacking:
                this.Attacking();
                break;
        }
    }

    private void Roaming()
    {
        this.timeRoaming += Time.deltaTime;
        this.enemyPathfinding.MoveTo(roamPosition);

        if (this.IsPlayerInRange())
        {
            this.state = State.Attacking;
        }

        if (this.timeRoaming > this.roamChangeDirFloat)
        {
            this.roamPosition = this.GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (!this.IsPlayerInRange())
        {
            this.state = State.Roaming;
        }

        if (this.attackRange != 0 && this.canAttack)
        {
            this.canAttack = false;
            (enemyType as IEnemy).Attack();

            if (this.stopMovingWhiteAttacking)
            {
                this.enemyPathfinding.StopMoving();
            }
            else
            {
                this.enemyPathfinding.MoveTo(roamPosition);
            }

            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(this.attackCooldown);
        this.canAttack = true;

    }

    private bool IsPlayerInRange()
    {
        Vector2 playerPosition = PlayerController.Instance.transform.position;
        return Vector2.Distance(transform.position, playerPosition) < this.attackRange;
    }

    private Vector2 GetRoamingPosition()
    {
        this.timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
