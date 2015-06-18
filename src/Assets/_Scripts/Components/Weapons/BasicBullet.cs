using UnityEngine;
using System.Collections;

public class BasicBullet : ABullet
{
    // Update is called once per frame
    public override void Update()
    {
        //Permet override tout en gardant la function de base
        base.Update();
        //Déplace la bullet
        _transform.Translate(_shootDirection * _projectileSpeed * Time.deltaTime, Space.World);

        //La bullet est détruit si elle est hors range
        if (outOfRange)
            Destroy(gameObject);
    }
}
