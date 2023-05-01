using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileForce = 500f;
    public float dispersionAngle = 10f;
    public float fireRate = 0.1f;
    public float projectileLifetime = 5f;

    private bool isFiring = false;
    private float lastFireTime = 0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Dispara un proyectil al hacer clic izquierdo
        {
            Shoot(1);
            isFiring = true;
        }
        else if (Input.GetMouseButtonDown(1)) // Dispara tres proyectiles al hacer clic derecho
        {
            Shoot(3);
            isFiring = true;
        }
        else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) // Detiene el disparo al soltar el clic
        {
            isFiring = false;
        }

        if (isFiring && Time.time - lastFireTime > fireRate)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot(1);
            }
            else if (Input.GetMouseButton(1))
            {
                Shoot(3);
            }
            lastFireTime = Time.time;
        }
    }

    private void Shoot(int projectileCount)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // La distancia de la cámara al arma
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector3 direction = (worldPosition - transform.position).normalized;

        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();

            if (projectileCount == 1)
            {
                projectileRigidbody.AddForce(direction * projectileForce);
            }
            else
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float angleOffset = (i - (projectileCount - 1) / 2f) * dispersionAngle;
                Vector3 dispersion = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward) * Vector3.right;
                projectileRigidbody.AddForce(dispersion * projectileForce);
            }

            // Destruye el proyectil después de "projectileLifetime" segundos
            Destroy(projectile, projectileLifetime);

            // Reproduce un efecto de sonido y una animación de disparo
        }
    }
}
