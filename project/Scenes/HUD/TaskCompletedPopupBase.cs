using Godot;
using System;

public abstract class TaskCompletedPopupBase : TextureRect
{
    protected Label _headline;

    protected Control _category1;
    protected Label _name1;
    protected TextureRect _comparison1;

    protected Control _category2;
    protected Label _name2;
    protected TextureRect _comparison2;

    protected Control _category3;
    protected Label _name3;
    protected TextureRect _comparison3;

    protected TextureButton _confirmButton;

    public event EventHandler<EventArgs> ConfirmButtonHandler;

    public override void _Ready()
    {
        _headline = GetNode<Label>("Headline");

        _category1 = GetNode<Control>("Category1");
        _name1 = _category1.GetNode<Label>("Name");
        _comparison1 = _category1.GetNode<TextureRect>("Comparison");

        _category2 = GetNode<Control>("Category2");
        _name2 = _category2.GetNode<Label>("Name");
        _comparison2 = _category2.GetNode<TextureRect>("Comparison");

        _category3 = GetNode<Control>("Category3");
        _name3 = _category3.GetNode<Label>("Name");
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

    /// <summary>
    /// Sets the trend texture based on comparison of provided values.
    /// </summary>
    /// <param name="textureRect">TextureRect to be set.</param>
    /// <param name="currentValue">Current value.</param>
    /// <param name="previousAverage">Average of previous values.</param>
    /// <param name="positive">Texture of positive trend.</param>
    /// <param name="negative">Texture of negative trend.</param>
    protected void SetTrendTexture(TextureRect textureRect, double currentValue, double previousAverage,
        Texture positive, Texture negative)
    {
        if (currentValue > previousAverage)
        {
            textureRect.Texture = positive;
        }
        else if (currentValue < previousAverage)
        {
            textureRect.Texture = negative;
        }
    }
}
