public interface IUIItem
{
    void ChangeParent(IUISlot slot, float timeToChange);
    public void SetUIItem(char symbol);
    public void SetUIItem(string str);
}
