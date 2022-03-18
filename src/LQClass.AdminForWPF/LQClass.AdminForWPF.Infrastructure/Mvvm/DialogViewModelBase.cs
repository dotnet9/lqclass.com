using System;
using Prism.Commands;
using Prism.Services.Dialogs;

namespace LQClass.AdminForWPF.Infrastructure.Mvvm;

public class DialogViewModelBase : ViewModelBase, IDialogAware
{
    private DelegateCommand<string> _closeDialogCommand;
    private string _message;
    private string _title = "Notification";

    public DelegateCommand<string> CloseDialogCommand =>
        _closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(ExecuteCloseDialogCommand));

    public string Message
    {
        get => _message;
        set => SetProperty(ref _message, value);
    }

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
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

    private void ExecuteCloseDialogCommand(string parameter)
    {
        var result = ButtonResult.None;
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
}