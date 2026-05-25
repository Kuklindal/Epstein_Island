using UnityEngine;

public class EnemyRun : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float speed = 7f;
    public float stopDistance = 1.5f;

    private bool chasePlayer = false;

    public void StartChase()
    {
        chasePlayer = true;
    }

    public void StopChase()
    {
        chasePlayer = false;
    }

    void Update()
    {
        if (!chasePlayer) return;
        if (target == null) return;

        Vector3 dir = target.position - transform.position;
        dir.y = 0f;

        if (dir.magnitude <= stopDistance)
            return;

        dir.Normalize();

        transform.position += dir * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(dir),
            Time.deltaTime * 8f
        );
    }
}