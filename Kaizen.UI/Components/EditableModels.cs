namespace Kaizen.UI.Components;

public enum ActionType
{
    Save,
    Delete
}

public record OnActionCompletedArgs<T>(ActionType ActionType, T Data);
