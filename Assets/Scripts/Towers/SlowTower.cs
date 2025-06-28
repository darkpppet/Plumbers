using System.Collections;
using UnityEngine;

namespace Towers
{
    public class SlowTower : TowerBase
    {
        public override int Cost { get; protected set; } = 75;
        public override int Range { get; protected set; } = 1;
        public override float Damage { get; protected set; } = 0.0f;

        private GameObject _slowAreas;

        private const float SlowRatio = 0.3f;
        private bool _isAddingSlow = false;

        private void Awake()
        {
            _slowAreas = transform.Find("SlowAreas").gameObject;
        }
        
        public override void Attack()
        {
            if (IsMonsterInRange())
            {
                if (!_isAddingSlow)
                {
                    _isAddingSlow = true;
                    _slowAreas.SetActive(true);
                    GameManager.Instance.Monster.AddSlow(SlowRatio);
                }
            }
            else
            {
                if (_isAddingSlow)
                {
                    _isAddingSlow = false;
                    _slowAreas.SetActive(false);
                    GameManager.Instance.Monster.RecoverySlow(SlowRatio);
                }
            }
        }
    }
}
