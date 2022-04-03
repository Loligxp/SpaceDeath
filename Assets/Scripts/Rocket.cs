using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private bool scanForward;
    [SerializeField] private float scanAngle;
    [SerializeField] private float currentScan;
    [SerializeField] private float scanDistance;
    [SerializeField] private float scanSpeed;
    [SerializeField] private LayerMask scanMask;
    [SerializeField] private GameObject trackingTarget;
    
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float damage;
    [SerializeField] private DamageEffects damageEffect;
    [SerializeField] private LineRenderer lineRend;
    [SerializeField] private GameObject deathEffect;

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        
        if (trackingTarget == null)
        {
            currentScan += scanSpeed * Time.deltaTime * (scanForward ? -1f : 1f);

            if (!scanForward)
                if (currentScan > scanAngle)
                    scanForward = !scanForward;
            if (scanForward)
                if (currentScan < -scanAngle)
                    scanForward = !scanForward;

            var scanRay = transform.up;
            var scanRotation = Quaternion.Euler(0, 0, currentScan);
            scanRay = scanRotation * scanRay;

            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, transform.position + scanRay * scanDistance);

            lineRend.startColor = new Color(1f, 0f, 0f, 0.2f); 
            lineRend.endColor = new Color(1f, 0f, 0f, 0.2f);

            var rayHit = Physics2D.Raycast(transform.position, scanRay, scanDistance, scanMask);

            if (rayHit.collider != null)
                trackingTarget = rayHit.collider.gameObject;
        }
        else
        {
            lineRend.SetPosition(0, transform.position);
            lineRend.SetPosition(1, trackingTarget.transform.position);

            lineRend.startColor = new Color(0f, 1f, 0f, 0.20f); 
            lineRend.endColor = new Color(0f, 1f, 0f, 0.20f); 

            var trackDir = (Vector2) trackingTarget.transform.position - (Vector2)transform.position;
            trackDir.Normalize();

            var rotDir = Vector3.Cross(trackDir, transform.up).z;
            
            if(rotDir > 0)
                transform.Rotate(0,0,-rotationSpeed * Time.deltaTime);
            else
                transform.Rotate(0,0,rotationSpeed * Time.deltaTime);

        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().TakeDamage(damage,damageEffect);
            Instantiate(deathEffect, transform.position, Quaternion.Euler(90,0,0));
            GameObject.Destroy(this.gameObject);
        }
    }

    
}
