using System;
using UnityEngine;

namespace Tiles
{
    public abstract class TileBase : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        
        public (int Row, int Column) Position { get; set; }
        public bool IsEnabled { get; private set; } = true;
        
        public virtual bool Left { get; protected set; }
        public virtual bool Right { get; protected set; }
        public virtual bool Top { get; protected set; }
        public virtual bool Bottom { get; protected set; }
        
        public virtual int Cost { get; protected set; }
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 커서의 스크린 좌표를 월드 좌표로 변환
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); // 해당 월드 좌표에서 2D 레이캐스트를 실행
                if (hit.collider is not null && hit.collider.gameObject == this.gameObject) // 레이캐스트가 어떤 콜라이더를 감지했고, 그 콜라이더가 이 게임 오브젝트의 콜라이더와 같다면
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (IsEnabled)
                        {
                            Remove();
                        }
                        else
                        {
                            Restore();
                        }
                    }
                    else if (Input.GetMouseButtonDown(0))
                    {
                        RotateLeft();
                    }
                    else if (Input.GetMouseButtonDown(1))
                    {
                        RotateRight();
                    }
                }
            }
        }

        private void RotateLeft()
        {
            bool temp = Top;
            Top = Right;
            Right = Bottom;
            Bottom = Left;
            Left = temp;
            
            RotateSpriteLeft();
        }

        private void RotateRight()
        {
            bool temp = Top;
            Top = Left;
            Left = Bottom;
            Bottom = Right;
            Right = temp;
            
            RotateSpriteRight();
        }

        private void RotateSpriteLeft()
        {
            _spriteRenderer.transform.Rotate(0, 0, 90);
        }

        private void RotateSpriteRight()
        {
            _spriteRenderer.transform.Rotate(0, 0, -90);
        }

        public void Remove()
        {
            if (IsEnabled)
            {
                IsEnabled = false;
                _spriteRenderer.enabled = false;
                GameManager.Instance.Credit += Cost;
            }
        }

        public void Restore()
        {
            if (!IsEnabled)
            {
                IsEnabled = true;
                _spriteRenderer.enabled = true;
                GameManager.Instance.Credit -= Cost;
            }
        }
    }
}
