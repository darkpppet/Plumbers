using System.Collections.Generic;
using System.Linq;
using Tiles;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public abstract class MonsterBase : MonoBehaviour
    {
        public (int Row, int Column) Position { get; private set; }

        public virtual float MaxHp { get; protected set; }

        private float _nowHp;
        public float NowHp
        {
            get => _nowHp;
            protected set
            {
                _nowHp = value;
                _hpBar.fillAmount = HpPercentage;
            }
        }
        
        public float HpPercentage => NowHp / MaxHp;

        private Image _hpBar;
        
        public virtual float Speed { get; protected set; }
        
        private List<(int X, int Y)> Path { get; set; }
        private int _targetIndex = 0;
        private Vector2 _targetDirection;
        private Vector2 _targetPosition;

        private void Start()
        {
            _hpBar = GetComponentsInChildren<Image>().First(x => x.name == "HpBar");
            NowHp = MaxHp;
        }

        // Update is called once per frame
        private void Update()
        {
            if (gameObject.activeInHierarchy)
            {
                Move(Time.deltaTime);

                if (Position.Column >= 0)
                {
                    DealDamage(GameManager.Instance.Stage.Map[Position.Row, Position.Column].TileDamage * Time.deltaTime);
                }
            }
        }

        public void DealDamage(float damage)
        {
            NowHp = Mathf.Clamp(NowHp - damage, 0.0f, MaxHp);

            if (NowHp <= 0.0f)
            {
                SetDie();
            }
        }

        public void AddSlow(float ratio)
        {
            Speed *= 1 - ratio;
        }

        public void RecoverySlow(float ratio)
        {
            Speed /= 1 - ratio;
        }

        private void SetGoal()
        {
            gameObject.SetActive(false);
            GameManager.Instance.LoseStage();
        }
        
        private void SetDie()
        {
            gameObject.SetActive(false);
            GameManager.Instance.WinStage();
        }

        private void Move(float deltaTime)
        {
            _targetDirection = (_targetPosition - (Vector2)transform.position).normalized;
            transform.position += (Vector3)_targetDirection * (Speed * deltaTime);

            if (Vector2.Distance(transform.position, _targetPosition) <= GameManager.Instance.Stage.Length / 2.0f)
            {
                Position = _targetIndex < Path.Count ? Path[_targetIndex] : (GameManager.Instance.Stage.EndRow, GameManager.Instance.Stage.MapSize.Column);
            }
            if (Vector2.Distance(transform.position, _targetPosition) <= 0.002f)
            {
                _targetIndex++;
                if (_targetIndex < Path.Count)
                {
                    _targetPosition = GameManager.Instance.Stage.Map[Path[_targetIndex].X, Path[_targetIndex].Y].transform.position;
                    
                }
                else if (_targetIndex == Path.Count)
                {
                    _targetPosition = GameManager.Instance.Stage.EndObject.transform.position;
                }
                else
                {
                    SetGoal();
                }
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            
            Path = GameManager.Instance.Stage.Path;
            
            Position = (GameManager.Instance.Stage.StartRow, -1);

            transform.position = GameManager.Instance.Stage.StartObject.transform.position + new Vector3(0, 0, -1);
            _targetPosition = GameManager.Instance.Stage.Map[Path[0].X, Path[0].Y].transform.position;
            _targetDirection = (_targetPosition - (Vector2)transform.position).normalized;
        }
    }
}
