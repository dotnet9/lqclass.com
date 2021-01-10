using Prism.Commands;
using Prism.Services.Dialogs;
using System;

namespace LQClass.AdminForWPF.Infrastructure.Mvvm
{
	public class DialogViewModelBase : ViewModelBase, IDialogAware
	{
		private DelegateCommand<string> _closeDialogCommand;
		public DelegateCommand<string> CloseDialogCommand =>
			_closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(ExecuteCloseDialogCommand));
		void ExecuteCloseDialogCommand(string parameter)
		{
			ButtonResult result = ButtonResult.None;
			if (parameter?.ToLower() == "true")
				result = ButtonResult.Yes;
			else if (parameter?.ToLower() == "false")
				result = ButtonResult.No;
			RaiseRequestClose(new DialogResult(result));
		}
		//触发窗体关闭事件
		public virtual void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}
		private string _message;
		public string Message
		{
			get { return _message; }
			set { SetProperty(ref _message, value); }
		}
		private string _title = "Notification";
		public new string Title
		{
			get { return _title; }
			set { SetProperty(ref _title, value); }
		}
		public event Action<IDialogResult> RequestClose;
		public bool CanCloseDialog()
		{
			return true;
		}
		public void OnDialogClosed()
		{

		}
		public void OnDialogOpened(IDialogParameters parameters)
		{
			Message = parameters.GetValue<string>("message");
		}
	}
}
