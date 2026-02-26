using System.Windows.Input;

namespace AntDesign.WPF.Input;

/// <summary>
/// A lightweight <see cref="ICommand"/> implementation for MVVM support.
/// Delegates execution and can-execute logic to caller-supplied delegates.
/// Uses <see cref="CommandManager.RequerySuggested"/> to automatically
/// re-query <see cref="CanExecute"/> whenever WPF's command infrastructure
/// detects relevant UI changes.
/// </summary>
public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;
    private readonly Func<object?, bool>? _canExecute;

    /// <summary>
    /// Initialises a new instance with separate execute and optional can-execute delegates
    /// that receive the command parameter.
    /// </summary>
    /// <param name="execute">The action to invoke when the command is executed.</param>
    /// <param name="canExecute">
    /// An optional predicate that determines whether the command is currently enabled.
    /// When <c>null</c>, the command is always enabled.
    /// </param>
    public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <summary>
    /// Initialises a new instance with parameterless execute and optional can-execute delegates.
    /// </summary>
    /// <param name="execute">The action to invoke when the command is executed.</param>
    /// <param name="canExecute">
    /// An optional predicate that determines whether the command is currently enabled.
    /// When <c>null</c>, the command is always enabled.
    /// </param>
    public RelayCommand(Action execute, Func<bool>? canExecute = null)
        : this(_ => execute(), canExecute != null ? _ => canExecute() : null) { }

    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <inheritdoc/>
    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

    /// <inheritdoc/>
    public void Execute(object? parameter) => _execute(parameter);

    /// <summary>
    /// Forces WPF to re-evaluate <see cref="CanExecute"/> for all commands.
    /// Call this when external state changes that affects can-execute logic.
    /// </summary>
    public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}

/// <summary>
/// A generic variant of <see cref="RelayCommand"/> that automatically casts the command
/// parameter to <typeparamref name="T"/> before passing it to the execute and can-execute
/// delegates.
/// </summary>
/// <typeparam name="T">The expected type of the command parameter.</typeparam>
public class RelayCommand<T> : ICommand
{
    private readonly Action<T?> _execute;
    private readonly Func<T?, bool>? _canExecute;

    /// <summary>
    /// Initialises a new instance with typed execute and optional can-execute delegates.
    /// </summary>
    /// <param name="execute">The action to invoke when the command is executed.</param>
    /// <param name="canExecute">
    /// An optional predicate that determines whether the command is currently enabled.
    /// When <c>null</c>, the command is always enabled.
    /// </param>
    public RelayCommand(Action<T?> execute, Func<T?, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    /// <inheritdoc/>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <inheritdoc/>
    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter is T t ? t : default) ?? true;

    /// <inheritdoc/>
    public void Execute(object? parameter) => _execute(parameter is T t ? t : default);

    /// <summary>
    /// Forces WPF to re-evaluate <see cref="CanExecute"/> for all commands.
    /// Call this when external state changes that affects can-execute logic.
    /// </summary>
    public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
}
