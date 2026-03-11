using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 10f;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}