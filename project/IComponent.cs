public interface IComponent
{
    //sets possible values for given component and a default value
	void SetPossibleValues(object[] possibleValues, object defaultValue);
	
	//sets a new selected value for the component
	void SetValue(object newValue);
	
	//expectedValue : value from the assignment for the current task (component's default value if null)
	//returns true if matches with selected value, else false
	bool CheckSelectedValue(object expectedValue);

	//Reset currently selected values to default state
	void ResetState();
}