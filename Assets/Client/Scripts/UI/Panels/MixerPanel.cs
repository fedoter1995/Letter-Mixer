using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MixerPanel : MonoBehaviour
{
    [SerializeField] private ControlPanel _controlPanel;
    [SerializeField] private LettersGrid _grid;
    [SerializeField] private float _timeToMix = 2f;
    [SerializeField] private int _maxColumns = 20;
    [SerializeField] private int _maxRows = 20;
    [SerializeField] private int _minColumns = 1;
    [SerializeField] private int _minRows = 1;
    public int GridWidth => _controlPanel.Width;
    public int GridHeight => _controlPanel.Height;

    private Dictionary<Lang, string> characterMap;

    private Coroutine mixingProgress;

    private void Awake()
    {
        Initialization();
    }
    private void Initialization()
    {       
        _controlPanel.Initialization(_maxColumns, _maxRows, _minColumns, _minRows);
        _controlPanel.FirstButon.onClick.AddListener(GridGeneration);
        _controlPanel.SecondButton.onClick.AddListener(GridMix);
        characterMap = SetLocalizations();
        string lettersMap = characterMap[(Lang)_controlPanel.Language];
        _grid.Initialization(lettersMap, GridWidth, GridHeight);
    }
    private void GridGeneration()
    {
        if (mixingProgress == null)
        {
            string lettersMap = characterMap[(Lang)_controlPanel.Language];
            _grid.SetupGrid(GridWidth, GridHeight, lettersMap);
        }
    }
    private void GridMix()
    {
        if (mixingProgress == null)
            mixingProgress = StartCoroutine(MixerRoutine(_timeToMix));
    }
    private Dictionary<Lang, string> SetLocalizations()
    {
        var map = new Dictionary<Lang, string>
        {
            { Lang.EN, "ABCDEFGHIJKLMNOPQRSTUVWXVZ" },
            { Lang.RU, "¿¡¬√ƒ≈®∆«»… ÀÃÕŒœ–—“”‘’÷◊ÿŸ⁄€‹›ﬁﬂ" }
        };

        return map;
    }
    private IEnumerator MixerRoutine(float timeToMix)
    {
        _grid.MixTheCells(timeToMix);
        yield return new WaitForSeconds(timeToMix);
        mixingProgress = null;
    }
}
