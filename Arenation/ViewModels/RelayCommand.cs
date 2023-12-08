using System;
using System.Windows.Input;

namespace Arenation.ViewModels; 

public class RelayCommand<T> : ICommand {
	private readonly Action<T> execute;
	private readonly Predicate<T>? canExecute;

	public RelayCommand(Action<T> execute) : this(execute, null) { }

	public RelayCommand(Action<T> execute, Predicate<T>? canExecute) {
		this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
		this.canExecute = canExecute;
	}

	public bool CanExecute(object? parameter) {
		return this.canExecute == null || this.canExecute(((T)parameter!));
	}

	public void Execute(object? parameter) {
		this.execute(((T)parameter!));
	}

	public event EventHandler? CanExecuteChanged {
		add => CommandManager.RequerySuggested += value;
		remove => CommandManager.RequerySuggested -= value;
	}
}