using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float velocidad = 10f;
    private float tiempoVida = 5f;
    private float tiempoActivacion;

    void OnEnable()
    {
        tiempoActivacion = Time.time;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

        if (Time.time > tiempoActivacion + tiempoVida)
        {
            BulletPool.Instance.ReturnBullet(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            health.RecibirDanio(10f);
        }

        BulletPool.Instance.ReturnBullet(gameObject);
    }
}