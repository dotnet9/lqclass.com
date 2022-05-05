using System;
using System.Collections.ObjectModel;
using LQClass.ModuleOfMenuManagement.DTOs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;

namespace LQClass.ModuleOfMenuManagement.ViewModels;

public class AddMenuViewModel : BindableBase, IDialogAware
{
    public AddMenuViewModel()
    {
        SaveCommand = new DelegateCommand(Save);
        CancelCommand = new DelegateCommand(Cancel);
    }

    private void Cancel()
    {
        RequestClose?.Invoke(new DialogResult(ButtonResult.No));
    }

    private void Save()
    {
        OnDialogClosed();
    }

    #region 命令

    public DelegateCommand SaveCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }

    #endregion

    #region 属性

    public EntityDTO Entity { get; set; }

    private ObservableCollection<GetFoldersModel> _Menulist;

    public ObservableCollection<GetFoldersModel> Menulist
    {
        get
        {
            return _Menulist;
            RaisePropertyChanged();
        }
        set => _Menulist = value;
    }

    #endregion

    #region 弹窗

    public string Title { get; set; }

    public event Action<IDialogResult> RequestClose;

    public bool CanCloseDialog()
    {
        return true;
    }

    public void OnDialogClosed()
    {
        var keyValuePairs = new DialogParameters();
        keyValuePairs.Add("Value", Entity);
        RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keyValuePairs));
    }

    public void OnDialogOpened(IDialogParameters parameters)
    {
        if (parameters.ContainsKey("getFoldersModelList"))
            Menulist = parameters.GetValue<ObservableCollection<GetFoldersModel>>("getFoldersModelList");
        else
            Entity = new EntityDTO();
    }

    #endregion
}