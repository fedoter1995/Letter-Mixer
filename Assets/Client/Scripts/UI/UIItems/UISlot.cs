using UnityEngine;

public class UISlot : MonoBehaviour, IUISlot
{
    [SerializeField] private UIItem _item;

    public UIItem Item => _item;
    public RectTransform Transform => transform as RectTransform;
    public void SetItem(UIItem item)
    {
        _item = item;
        item.transform.SetParent(transform);
    }
}
