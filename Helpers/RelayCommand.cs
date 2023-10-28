using System;
using System.Windows.Input;

namespace CoinTrack.Helpers;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<bool>? _canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }


    public RelayCommand(Action<object?> execute)
    {
        ArgumentNullException.ThrowIfNull(execute);

        _execute = execute;
    }

    public RelayCommand(Action<object?> execute, Func<bool> canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        ArgumentNullException.ThrowIfNull(canExecute);

        _execute = execute;
        _canExecute = canExecute;
    }

    public void Execute(object? parameter) => _execute(parameter);

    public bool CanExecute(object? parameter) => _canExecute is null || _canExecute.Invoke();
}