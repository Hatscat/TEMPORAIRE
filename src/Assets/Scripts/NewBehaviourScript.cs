using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {

    public float speed;
    public Vector3 direction;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
