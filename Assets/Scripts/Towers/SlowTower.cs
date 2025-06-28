using System.Collections;
using UnityEngine;

namespace Towers
{
    public class SlowTower : TowerBase
    {
        public override int Cost { get; protected set; } = 10;
        public override int Range { get; protected set; } = 1;
        public override float Damage { get; protected set; } = 1.0f;
        
        private bool _canAttack = true;
        private const float AttackCoolTime = 1.0f;
        
        public override void Attack()
        {
            if (_canAttack)
            {
                ShootProjectile();
                _canAttack = false;
                StartCoroutine(StartAttackCooltime());
            }
        }
    
        private void ShootProjectile()
        {
        
        }
    
        private IEnumerator StartAttackCooltime()
        {
            yield return new WaitForSeconds(AttackCoolTime);
        }
    }
}
