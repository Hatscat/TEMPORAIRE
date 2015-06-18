using UnityEngine;
using System.Collections;

public abstract class ABullet : MonoBehaviour
{
    protected Transform _transform;

    protected Vector3 _shootDirection;
    protected float _fireRange;
    protected float _projectileSpeed;

    protected Vector3 _firedPosition;
    protected bool outOfRange;

    public virtual void InitBullet(Vector3 direction, float range, float speed)
    {
        _transform = transform;

        _shootDirection = direction;
        _fireRange = range;
        _projectileSpeed = speed;
        _firedPosition = _transform.position;
    }

    public virtual void Update()
    {
        if (!outOfRange && Vector3.Distance(_firedPosition, _transform.position) >= _fireRange)
            outOfRange = true;
    }

    public virtual void OnTriggerEnter()
    {
        Destroy(gameObject);
    }
}
