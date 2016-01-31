using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {
    public float projectileSpeed = 2;

    Vector3 goalPosition;

    public void setGoalPosition(Vector3 goalPosition)
    {
        this.goalPosition = goalPosition;
    }

    void Update()
    {
        if (goalPosition == null)
        {
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, goalPosition, Time.deltaTime * projectileSpeed);
        checkDestroy();
    }

    void checkDestroy()
    {
        if ((transform.position - goalPosition).magnitude < .01)
        {
            Destroy(this.gameObject);
        }
    }
}
