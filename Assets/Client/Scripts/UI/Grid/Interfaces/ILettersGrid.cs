public interface ILettersGrid
{
    int Width { get; }
    int Height { get; }

    void Initialization(string lettersMap, int width, int height);
    public void SetupGrid(int width, int height, string lettersMap);
    public void MixTheCells(float mixTime);
}
