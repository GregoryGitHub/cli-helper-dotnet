namespace App.Main;

public class UserOption<TValue>
{

    public UserOption(string label, TValue value)
    {
        Label = label;
        Value = value;
       
    }
    public string Label { get; set; }
    public TValue Value { get; set; }
}