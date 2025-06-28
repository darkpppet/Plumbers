using System.Linq;
using Tiles;
using TMPro;
using Towers;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TowerBase tower;
    public string towerName;
    
    private TextMeshProUGUI _nameText;
    private Transform _wireFrame;

    private bool _isCatching = false;
    
    private void Awake()
    {
        _nameText = GetComponentsInChildren<TextMeshProUGUI>().First(x => x.name == "NameText");
        _nameText.text = $"{towerName}\n${tower.Cost}";
        
        _wireFrame = transform.Find("WireFrame");
    }

    // Update is called once per frame
    private void Update()
    {
        if (!GameManager.Instance.IsPlaying)
        {
            if (_isCatching)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = -1;
                _wireFrame.transform.position = mousePos;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 마우스 커서의 스크린 좌표를 월드 좌표로 변환
                RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero); // 해당 월드 좌표에서 2D 레이캐스트를 실행

                if (!_isCatching && hit.collider is not null && hit.collider.gameObject == this.gameObject && GameManager.Instance.Credit >= tower.Cost)
                {
                    GameManager.Instance.Credit -= tower.Cost;
                    _isCatching = true;
                    _wireFrame.gameObject.SetActive(true);
                }
                else if (_isCatching)
                {
                    TileBase location;
                    if (hit.collider is not null && hit.collider.TryGetComponent<TileBase>(out location))
                    {
                        if (!location.IsEnabled)
                        {
                            TowerBase installed = Instantiate(tower, location.transform.position, Quaternion.identity);
                            installed.Position = location.Position;
                            GameManager.Instance.Stage.towers.Add(installed);
                            _isCatching = false;
                            _wireFrame.gameObject.SetActive(false);
                        }
                    }
                }
            
            }
            else if (_isCatching && Input.GetMouseButtonDown(1))
            {
                GameManager.Instance.Credit += tower.Cost;
                _isCatching = false;
                _wireFrame.gameObject.SetActive(false);
            }
        }
        else if (_isCatching)
        {
            GameManager.Instance.Credit += tower.Cost;
            _isCatching = false;
            _wireFrame.gameObject.SetActive(false);
        }
    }
}
