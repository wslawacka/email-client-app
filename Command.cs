using System;
using System.Windows.Input;
using MsBox.Avalonia;

namespace EmailClient;

public class Command : ICommand
{
    // action to execute when the command is invoked
    private Action<object?> _action;
    // function to determine whether the command can be executed
    private Func<object?, bool> _canExecute;
    
    public Command(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        // ensure execute is not null  
        _action = execute ?? throw new ArgumentNullException(nameof(execute));
        // if canExecute is not provided, default to always true
        _canExecute = canExecute ?? (parameter => true);
    }

    // event required by ICommand to signal that the result of CanExecute has changed
    public event EventHandler? CanExecuteChanged;

    // determines whether the command can execute
    public bool CanExecute(object? parameter)
    {
        return _canExecute?.Invoke(parameter) ?? true; 
    }

    // executes the command
    public void Execute(object? parameter)
    {
        _action?.Invoke(parameter);
    }
    
    // raises the CanExecuteChanged event to notify UI to requery CanExecute
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}