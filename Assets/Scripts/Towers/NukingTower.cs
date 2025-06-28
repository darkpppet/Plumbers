using System.Collections;
using UnityEngine;

namespace Towers
{
    public class NukingTower : TowerBase
    {
        public override int Cost { get; protected set; } = 100;
        public override int Range { get; protected set; } = 1;
        private readonly float[] _damages = { 1.0f, 2.0f, 3.0f };
        private int _damagesIndex = 0;
        public override float Damage => _damages[_damagesIndex];

        private const float ChargeTime = 1.0f;
        private bool _isCharging = false;
        private bool _SFXPlaying = false;

        private GameObject _laser;
        
        private void Awake()
        {
            _laser = transform.Find("LaserPart").gameObject;
        }

        public override void Attack()
        {
            if (IsMonsterInRange())
            {
                if (!_isCharging)
                {
                    _isCharging = true;
                    StartCoroutine(StartAttackCharge());
                    _laser.SetActive(true);
                }

                SetLaserTransform();

                GameManager.Instance.Monster.DealDamage(Damage * Time.deltaTime);
                if (!_SFXPlaying)
                {
                    StartCoroutine(LaserSFX());
                }
            }
            else
            {
                _laser.SetActive(false);
                StopAllCoroutines();
                _isCharging = false;
                _damagesIndex = 0;
            }
        }

        private void SetLaserTransform()
        {
            Vector2 targetPos = GameManager.Instance.Monster.transform.position;
            Vector2 startPos = transform.position;
            
            float distance = (targetPos - startPos).magnitude;
            Vector2 direction = (targetPos - startPos).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
            
            _laser.transform.position = (startPos + targetPos) / 2;
            _laser.transform.position += new Vector3(0, 0, -2);
            _laser.transform.localScale = new Vector2(distance, 0.3f);
            _laser.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private IEnumerator LaserSFX()
        {
            _SFXPlaying = true;
            AudioManager.Instance.PlaySFX(5);
            yield return new WaitForSeconds(3);
            _SFXPlaying = false;
        }

        private IEnumerator StartAttackCharge()
        {
            while (true)
            {
                yield return new WaitForSeconds(ChargeTime);
                _damagesIndex = _damagesIndex < _damages.Length - 1 ? _damagesIndex + 1 : _damages.Length - 1;
            }
        }
    }
}
