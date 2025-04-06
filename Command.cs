using System;
using System.Windows.Input;
using MsBox.Avalonia;

namespace EmailClient;

public class Command : ICommand
{
    private Action<object?> _action;
    private Func<object?, bool> _canExecute;
    
    public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _action = execute ?? throw new ArgumentNullException(nameof(execute));
        
        _canExecute = canExecute ?? (parameter => true);
    }

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true; 
    }

    public void Execute(object? parameter)
    {
        _action?.Invoke(parameter);
    }
    
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}