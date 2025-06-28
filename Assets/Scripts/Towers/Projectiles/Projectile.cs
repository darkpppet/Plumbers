using System.Collections;
using Monster;
using UnityEngine;

namespace Towers.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public Vector2 direction;
        public float speed;
        public float damage;

        private void Update()
        {
            transform.position += (Vector3)direction * (speed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            MonsterBase monster;
            if (other.gameObject.TryGetComponent<MonsterBase>(out monster))
            {
                monster.DealDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
