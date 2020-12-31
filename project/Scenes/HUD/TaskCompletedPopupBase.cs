using Godot;
using System;

public class TaskCompletedPopupBase : TextureRect
{
    protected Label _headline;
    protected Label _currentPerformance;
    protected Label _previousAverage;

    protected Control _category1;
    protected Label _name1;
    protected Label _current1;
    protected Label _previous1;
    protected TextureRect _comparison1;

    protected Control _category2;
    protected Label _name2;
    protected Label _current2;
    protected Label _previous2;
    protected TextureRect _comparison2;

    protected Control _category3;
    protected Label _name3;
    protected Label _current3;
    protected Label _previous3;
    protected TextureRect _comparison3;

    protected TextureButton _confirmButton;

    protected DynamicFont _headlineFont;
    protected DynamicFont _tableHeaderFont;
    protected DynamicFont _tableDataFont;

    public event EventHandler<EventArgs> ConfirmButtonHandler;

    public override void _Ready()
    {
        _headline = GetNode<Label>("Headline");
        _currentPerformance = GetNode<Label>("CurrentPerformance");
        _previousAverage = GetNode<Label>("PreviousAverage");

        _category1 = GetNode<Control>("Category1");
        _name1 = _category1.GetNode<Label>("Name");
        _current1 = _category1.GetNode<Label>("Current");
        _previous1 = _category1.GetNode<Label>("Previous");
        _comparison1 = _category1.GetNode<TextureRect>("Comparison");

        _category2 = GetNode<Control>("Category2");
        _name2 = _category2.GetNode<Label>("Name");
        _current2 = _category2.GetNode<Label>("Current");
        _previous2 = _category2.GetNode<Label>("Previous");
        _comparison2 = _category2.GetNode<TextureRect>("Comparison");

        _category3 = GetNode<Control>("Category3");
        _name3 = _category3.GetNode<Label>("Name");
        _current3 = _category3.GetNode<Label>("Current");
        _previous3 = _category3.GetNode<Label>("Previous");
        _comparison3 = _category3.GetNode<TextureRect>("Comparison");

        _confirmButton = GetNode<TextureButton>("ConfirmButton");

        _confirmButton.Connect("pressed", this, nameof(ConfirmButtonOnClick));
    }

    /// <summary>
    /// Handler of confirm button pressed signal. Hides the popup and calls assigned event handler.
    /// </summary>
    public void ConfirmButtonOnClick()
    {
        ConfirmButtonHandler?.Invoke(this, new EventArgs());
        this.QueueFree();
    }
}
