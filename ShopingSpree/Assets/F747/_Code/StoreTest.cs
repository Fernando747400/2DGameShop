using NaughtyAttributes;
using UnityEngine;

public class StoreTest : MonoBehaviour
{
    public ItemsManager ItemsManager;

    public BaseItemSO TestItem;

    [Button]
    public void ChangeSprite()
    {
        ItemsManager.UpdateSprite(TestItem);
    }
}
