using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class NumericInput : InputField
{
    public int MaxValue { get; set; } = 1;
    public int MinValue { get; set; } = 1;
    public int Value
    {
        get
        {
            if (this.text.Length != 0)
            {
                return Convert.ToInt32(this.text);
            }
            else
            {
                return MinValue;
            }

        }
       
    }

    protected override void Start()
    {
        base.Start();
        onValidateInput += delegate (string input, int charIndex, char addedChar) { return MyValidate(addedChar); };
        onValueChanged.AddListener(CheckNumber);
    }
    private char MyValidate(char charToValidate)
    {
        if (!Regex.IsMatch(charToValidate.ToString(), @"^\d+$"))
        {
            charToValidate = '\0';
        }       
        return charToValidate;
    }
    private void CheckNumber(string value)
    {
        if(value.Length > 0)
        {
            int numb = Convert.ToInt32(value);

            if (numb > MaxValue || numb < MinValue)
            {
                string newStr = value.Substring(0, value.Length - 1);
                text = newStr;
            }
        }

    }
    
}
