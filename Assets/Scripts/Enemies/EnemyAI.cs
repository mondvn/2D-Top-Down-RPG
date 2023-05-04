using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    }

    [SerializeField] private float roamChangeDirFloat = 2f;

    private State state;
    private EnemyPathfinding enemyPathfinding;

    private void Awake()
    {
        this.enemyPathfinding = GetComponent<EnemyPathfinding>();
        this.state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (this.state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            this.enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(this.roamChangeDirFloat);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
