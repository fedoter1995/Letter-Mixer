using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IUIItem
{
    [SerializeField] private Text _letter;

    private Coroutine changeParentCoroutine;

    public void ChangeParent(IUISlot slot, float timeToChange)
    {
        if (changeParentCoroutine == null)
            changeParentCoroutine = StartCoroutine(ChangeParentRoutine(slot, timeToChange));
    }
    public void SetUIItem(char symbol)
    {
        string str = "";
        str += symbol;
        _letter.text = str;
    }
    public void SetUIItem(string str)
    {
        _letter.text = str;
    }
    private void SetParent(IUISlot parent)
    {
        transform.SetParent(parent.Transform);
        transform.localPosition = Vector3.zero;
        parent.SetItem(this);
        changeParentCoroutine = null;
    }
    private IEnumerator MovementRoutine(Vector2 endPosition, float timeOfTravel)
    {
        var rectTransform = transform as RectTransform;
        float currentTime = 0;
        float normalizedValue;
        Vector2 startPosition = rectTransform.position;

        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel;

            rectTransform.position = Vector3.Lerp(startPosition, endPosition, normalizedValue);
            yield return null;
        }
    }
    private IEnumerator ChangeParentRoutine(IUISlot parent, float timeToChange)
    {
        yield return StartCoroutine(MovementRoutine(parent.Transform.position, timeToChange));
        SetParent(parent);
    }
    
}

