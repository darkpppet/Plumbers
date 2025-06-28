using System.Collections;
using Towers.Projectiles;
using UnityEngine;

namespace Towers
{
    public class LongRangeTower : TowerBase
    {
        public override int Cost { get; protected set; } = 75;
        public override int Range { get; protected set; } = 2;
        public override float Damage { get; protected set; } = 0.5f;
        
        public Projectile projectile;
        
        private bool _canAttack = true;
        private const float AttackCoolTime = 1.0f;
    
        public override void Attack()
        {
            if (_canAttack && IsMonsterInRange())
            {
                ShootProjectile();
                _canAttack = false;
                StartCoroutine(StartAttackCoolTime());
            }
        }
    
        private void ShootProjectile()
        {
            Projectile p = Instantiate(projectile, transform);
            p.direction = (GameManager.Instance.Monster.transform.position - transform.position).normalized;

        }
    
        private IEnumerator StartAttackCoolTime()
        {
            yield return new WaitForSeconds(AttackCoolTime);
            _canAttack = true;
        }
    }
}
