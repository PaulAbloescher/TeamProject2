// <copyright file="Command.cs" company="KW Softworks">
//     Copyright (c) Paul-Noel Ablöscher. All rights reserved.
// </copyright>
// <summary>Represents a command.</summary>
// <author>Killerwasps</author>

namespace YALS_WaspEdition.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Represents a command.
    /// </summary>
    /// <seealso cref="System.Windows.Input.ICommand" />
    public class Command : ICommand
    {
        /// <summary>
        /// The execute field.
        /// </summary>
        private readonly Action<object> execute;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the given command is null.</exception>
        public Command(Action<object> execute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        /// <summary>
        /// Fired when the execution state was changed.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// The method that checks if the command can be executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns>
        /// True, if the command can be executed otherwise false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the specified action.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}
