using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanelManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private ScriptableEventBaseItemSO _equipItemChannel;
    [Required][SerializeField] private Button _button;
    [Required][SerializeField] private Image _iconImage;
    
    [SerializeField] private BaseItemSO _baseItem;

    public BaseItemSO BaseItem { get => _baseItem; set { _baseItem = value; SetupItem(); } }

    private void OnEnable()
    {
        _button.onClick.AddListener(() => EquipItem());
        SetupItem();
    }
    private void OnDisable()
    {
        _button.onClick.RemoveListener(() => EquipItem());
    }

    private void SetupItem()
    {
        _button.interactable = false;
        _iconImage.color = Color.black;
        if (_baseItem == null) return;

        if (_baseItem.Purchased)
        {
            _button.interactable = true;
            _iconImage.color = Color.white;
        } 
        _iconImage.sprite = _baseItem.StoreIcon;
    }

    private void EquipItem()
    {
        _equipItemChannel.Raise(_baseItem);
    }
}
