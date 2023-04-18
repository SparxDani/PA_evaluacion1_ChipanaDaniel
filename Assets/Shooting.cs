using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileForce = 500f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Dispara al hacer clic izquierdo
        {
            Shoot();
        }
        else if (Input.GetMouseButtonDown(1)) // Dispara al hacer clic derecho
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // La distancia de la cámara al arma
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = (worldPosition - transform.position).normalized;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
        projectileRigidbody.AddForce(direction * projectileForce);

        // Reproduce un efecto de sonido y una animación de disparo
    }
}
