using Code;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class RandomAttack : Attack
{
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private Vector2 direction;
    [SerializeField] private Transform origin;
    [SerializeField] protected VisualEffect[] Particles;

    public override bool IsEnemyInRange(out IDamageable target)
    {
        foreach (var hit in Physics2D.RaycastAll(origin.position, direction, 15))
        {
            if (hit.collider.TryGetComponent(out IDamageable damageable) && damageable.AttackTag == TargetTag)
            {
                target = damageable;
                return true;
            }
        }
        target = null;
        return false;
    }

    public override void Perform()
    {
        GameObject newProjectile = null;
        int rand = Random.Range(0, 100);
        if (rand <= 45) newProjectile = Instantiate(projectiles[0]);
        else if(rand <=90) newProjectile = Instantiate(projectiles[1]);
        else newProjectile = Instantiate(projectiles[2]);
        if (newProjectile.TryGetComponent<BaseProjectile>(out var projectile))
        {

            projectile.transform.position = origin.position;
            projectile.Init(direction, TargetTag);
        }
        if(newProjectile.TryGetComponent<LaserProjectile>(out var laser))
        {
            HashSet<IDamageable> damagedTargets = new HashSet<IDamageable>();
            laser.transform.position = origin.position;
            laser.Init(direction, TargetTag, damagedTargets);
        }
        
        if (Particles.Length > 0) Particles[0].Play();
    }
}
