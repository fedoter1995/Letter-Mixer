using UnityEngine.UI;
public interface IControlPanel
{
    int Width { get; }
    int Height { get; }
    int Language { get; }
    public Button FirstButon { get; }
    public Button SecondButton { get; }
    void Initialization(int maxColumns, int maxRows, int minColumns = 1, int minRows = 1);
    void SetInputRange(int maxColumns, int maxRows, int minColumns = 1, int minRows = 1);
}
