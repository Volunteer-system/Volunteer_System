using System;
using System.Windows.Input;

namespace Volunteer_WPF.View_Model
{
    internal class RelayCommand : ICommand
    {
        private Action tESTFUN;
        private Func<bool> canShow;

        public RelayCommand(Action tESTFUN, Func<bool> canShow)
        {
            this.tESTFUN = tESTFUN;
            this.canShow = canShow;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canShow == null ? true : canShow();
        }

        public void Execute(object parameter)
        {
            this.tESTFUN();
        }
    }
}