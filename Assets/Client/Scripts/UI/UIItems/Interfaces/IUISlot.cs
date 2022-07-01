using UnityEngine;
public interface IUISlot
{
    UIItem Item { get; }
    RectTransform Transform { get; }
    void SetItem(UIItem item);
}
