using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TabInventoryManager : MonoBehaviour
{
    [Header("Dependencies")]
    [Required][SerializeField] private CharacterPiecesSO _characterPieces;

    [Header("Settings")]
    [SerializeField] private BodyPartType _bodyPartType;
    [SerializeField] private List<InventoryPanelManager> _inventoryPanelList;

    private void OnEnable()
    {
        LoadPanels();
    }

    private void LoadPanels()
    {
        List<BaseItemSO> itemsList = _characterPieces.GetListOf(_bodyPartType);
       for(int i = 0; i < itemsList.Count; i++)
        {
            _inventoryPanelList[i].BaseItem = itemsList[i];
        }
    }
}
