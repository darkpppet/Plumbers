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
                AudioManager.Instance.PlaySFX(4, 0.2f);
                StartCoroutine(StartAttackCoolTime());
            }
        }
    
        private void ShootProjectile()
        {
            Vector2 targetPos = GameManager.Instance.Monster.transform.position;
            Vector2 startPos = transform.position;
            
            Vector2 direction = (targetPos - startPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
            
            Projectile p = Instantiate(projectile, transform);
            p.direction = direction;
            p.damage = Damage;
        }
    
        private IEnumerator StartAttackCoolTime()
        {
            yield return new WaitForSeconds(AttackCoolTime);
            _canAttack = true;
        }
    }
}
