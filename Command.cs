using System;
using System.Windows.Input;
using MsBox.Avalonia;

namespace EmailClient;

public class Command : ICommand
{
    
    public Command(Action<object?> action) { _action = action; }
     
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) { return true; }
    public void Execute(object? parameter) { _action?.Invoke(parameter); }
    
    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
    
    private Action<object?> _action;
}