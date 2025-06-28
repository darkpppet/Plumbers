using UnityEngine;

namespace Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        public virtual int Cost { get; protected set; }
        public (int X, int Y) Position { get; set; }
        public virtual int Range { get; protected set; }
        public virtual float Damage { get; protected set; }
        
        // Update is called once per frame
        protected void Update()
        {
            Attack();
        }

        public abstract void Attack();

        protected bool IsMonsterInRange()
        {
            if (!GameManager.Instance.Monster.gameObject.activeInHierarchy)
            {
                return false;
            }
            
            for (int i = -Range; i <= Range; i++)
            {
                for (int j = -Range; j <= Range; j++)
                {
                    int x = Position.X + i;
                    int y = Position.Y + j;

                    if (GameManager.Instance.Monster.Position == (x, y))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
