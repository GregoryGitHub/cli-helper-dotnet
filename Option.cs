namespace App.Main;
// (string Name, bool isSelected, bool isHovered);
public class Option
{

    public Option(string name, bool isSelected, bool isHovered)
    {
        Name = name;
        IsSelected = isSelected;
        IsHovered = isHovered;
    }
    public string Name { get; set; }
    public bool IsSelected { get; set; }
    public bool IsHovered { get; set; }
}