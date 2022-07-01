using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour, IControlPanel
{

    [SerializeField] private Button _generateButton;
    [SerializeField] private Button _mixingButton;
    [SerializeField] private NumericInput _widthInput;
    [SerializeField] private NumericInput _heightInput;
    [SerializeField] private Dropdown _languageDropdown;
    public int Width => _widthInput.Value;
    public int Height => _heightInput.Value;
    public int Language => _languageDropdown.value;
    public Button FirstButon => _generateButton;
    public Button SecondButton => _mixingButton;

    public void Initialization(int maxColumns, int maxRows, int minColumns = 1, int minRows = 1)
    {
        SetInputRange(maxColumns, maxRows, minColumns, minRows);
        string[] langNames = Enum.GetNames(typeof(Lang));
        var names = new List<string>(langNames);
        _languageDropdown.AddOptions(names);
    }
    public void SetInputRange(int maxColumns, int maxRows, int minColumns = 1, int minRows = 1)
    {
        _widthInput.MaxValue = maxColumns;
        _heightInput.MaxValue = maxRows;
        _widthInput.MinValue = minColumns;
        _heightInput.MinValue = minRows;
    }
}

public enum Lang
{
    EN = 0,
    RU = 1,
}
