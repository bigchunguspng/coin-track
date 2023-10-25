using System;
using System.Windows.Input;

namespace CoinTrack.Helpers;

public class RelayCommand : ICommand
{
    private readonly Action _execute;
    private readonly Func<bool>? _canExecute;

    public event EventHandler? CanExecuteChanged;


    public RelayCommand(Action execute)
    {
        ArgumentNullException.ThrowIfNull(execute);

        _execute = execute;
    }
    
    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        ArgumentNullException.ThrowIfNull(canExecute);

        _execute = execute;
        _canExecute = canExecute;
    }

    public void Execute(object? parameter) => _execute();

    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute.Invoke();
}