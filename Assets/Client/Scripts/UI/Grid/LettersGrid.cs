using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(FlexibleGrid))]
public class LettersGrid : MonoBehaviour, ILettersGrid
{
    [SerializeField] RectTransform _lettersContainer;

    private FlexibleGrid grid;
    private Pool<UISlot> itemsPool;
    private List<IUISlot> slots;
    private string letters;

    public int Width { get; private set; }
    public int Height { get; private set; }

    public void Initialization(string lettersMap, int width, int height)
    {
        LoadResources();
        try
        {
            grid = GetComponent<FlexibleGrid>();
            grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        }
        catch
        {
            throw new System.Exception($"Can't get component : {typeof(FlexibleGrid)}, on {this}");
        }
        finally
        {
            SetupGrid(width, height, lettersMap);
        }
    }
    public void SetupGrid(int width, int height, string lettersMap)
    {
        //—брасываем Parent дл€ корректной отрисовки €чеек. »з-за использовани€ пула.
        ResetParents();

        Width = width;
        Height = height;
        letters = lettersMap;

        GenerateGrid();
    }
    public void MixTheCells(float mixTime)
    {
        var usedSlots = new List<IUISlot>();

        foreach(IUISlot slot in slots)
            ChangeSlotItem(usedSlots, slot, mixTime);        
    }
    private void GenerateGrid()
    {
        grid.Columns = Width;

        slots = new List<IUISlot>();

        for (int i = 0; i < Width * Height; i++)
        {
            int rndNum = UnityEngine.Random.Range(0, letters.Length);
            var slot = itemsPool.GetFreeObject();

            slot.transform.SetParent(transform);
            slot.Item.SetUIItem(letters[rndNum]);

            slots.Add(slot);
        }
    }
    private List<IUISlot> ChangeSlotItem(List<IUISlot> usedSlots, IUISlot curentSlot, float timeToChange)
    {
        IUISlot newSlot = GetRandomSlot();

        if (usedSlots.Contains(newSlot))
            return ChangeSlotItem(usedSlots, curentSlot, timeToChange);

        curentSlot.Item.ChangeParent(newSlot, timeToChange);
        usedSlots.Add(newSlot);
        return usedSlots;
    }
    private IUISlot GetRandomSlot()
    {
        int randomNumb = Random.Range(0, slots.Count);
        var slot = slots[randomNumb];
        return slot;
    }
    private void LoadResources()
    {
        try
        {
            UISlot resource = Resources.Load<UISlot>("UISlot");
            SetupPool(resource);
        }
        catch
        {
            throw new System.Exception($"Failed to load a file : {typeof(UISlot)}, from the resource folder");
        }
    }
    private void SetupPool(UISlot itemPref)
    {
        itemsPool = new Pool<UISlot>(itemPref, Width * Height, _lettersContainer, true);
    }
    private void ResetParents()
    {   
        if(slots != null)       
            foreach (UISlot slot in slots)
            {
                slot.transform.SetParent(_lettersContainer);
                slot.gameObject.SetActive(false);
            }    
    }
}
