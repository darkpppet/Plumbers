using UnityEngine;

namespace Towers
{
    public abstract class TowerBase : MonoBehaviour
    {
        public virtual int Cost { get; protected set; }
        public (int X, int Y) Position { get; set; }
        public virtual int Range { get; protected set; }
        public virtual float Damage { get; protected set; }
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        protected void Update()
        {
            if (IsMonsterInRange())
            {
                Attack();
            }
        }

        public abstract void Attack();

        protected bool IsMonsterInRange()
        {
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
