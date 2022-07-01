using UnityEngine;
using UnityEngine.UI;

public sealed class FlexibleGrid : GridLayoutGroup
{

    private int rows;
    private int columns;


    public int Rows { get => rows; set => rows = value; }
    public int Columns { get => columns; set => columns = value; }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        CheckConstraint();

        //–егулируем размер €чеек относительно их количества и размера родительского Rect..
        AdjustingCellsSize();

        //–авномерно –еспредел€ем €чейки..
        AdjustingCellsPosition();
    }
    public override void CalculateLayoutInputVertical() {}
    public override void SetLayoutHorizontal() {}
    public override void SetLayoutVertical() {}

    private void CheckConstraint()
    {
        if (constraint == Constraint.Flexible)
        {
            float sqrt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrt);
            columns = Mathf.CeilToInt(sqrt);
        }

        if (constraint == Constraint.FixedColumnCount)
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);

        if (constraint == Constraint.FixedRowCount)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
            Debug.Log("sadas");
        }
            
    }
    private void AdjustingCellsSize()
    {
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float CellWidth = parentWidth / (float)columns;

        float CellHeight = parentHeight / (float)rows;

        m_CellSize.x = CellWidth;
        m_CellSize.y = CellHeight;
    }
    private void AdjustingCellsPosition()
    {
        int columnCount;
        int rowCount;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];
            var xPos = cellSize.x * columnCount;
            var yPos = cellSize.y * rowCount;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
}
