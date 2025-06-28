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
            
            if (!GameManager.Instance.IsPlaying && Input.GetKeyDown(KeyCode.E))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 커서의 스크린 좌표를 월드 좌표로 변환
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); // 해당 월드 좌표에서 2D 레이캐스트를 실행
                if (hit.collider is not null && hit.collider.gameObject == this.gameObject) // 레이캐스트가 어떤 콜라이더를 감지했고, 그 콜라이더가 이 게임 오브젝트의 콜라이더와 같다면
                {
                    GameManager.Instance.Credit += Cost;
                    GameManager.Instance.Stage.towers.Remove(this);
                    Destroy(gameObject);
                    AudioManager.Instance.PlaySFX(1);
                }
            }
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
