namespace App.Main;
public class CliOption<TValue>
{
    public CliOption(string label, TValue value, bool isSelected, bool isHovered)
    {
        Label = label;
        Value = value;
        IsSelected = isSelected;
        IsHovered = isHovered;
    }
    public string Label { get; set; }
    public TValue Value { get; set; }
    public bool IsSelected { get; set; }
    public bool IsHovered { get; set; }
}