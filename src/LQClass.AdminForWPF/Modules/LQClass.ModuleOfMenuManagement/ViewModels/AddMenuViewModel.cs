using LQClass.ModuleOfMenuManagement.DTOs;
using LQClass.ModuleOfMenuManagement.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LQClass.ModuleOfMenuManagement.ViewModels
{
    public class AddMenuViewModel :BindableBase,IDialogAware
    {
        #region 命令
        public DelegateCommand  SaveCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        #endregion
        #region 属性
        private EntityDTO  _entity;

        public EntityDTO Entity
        {
            get { return  _entity; }
            set {  _entity = value; }
        }

        private ObservableCollection<GetFoldersModel> _Menulist;

        public ObservableCollection<GetFoldersModel> Menulist
        {
            get { return _Menulist; RaisePropertyChanged(); }
            set { _Menulist = value; }
        }


        #endregion
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
        #region 弹窗
        public string Title { get; set; }

        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            DialogParameters keyValuePairs = new DialogParameters();
            keyValuePairs.Add("Value", Entity);
            RequestClose?.Invoke(new DialogResult(ButtonResult.OK, keyValuePairs));
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("getFoldersModelList"))
            {
                Menulist  =  parameters.GetValue<ObservableCollection<GetFoldersModel>>("getFoldersModelList");
            }
            else
            {
                Entity = new EntityDTO();
            }

        }
        #endregion

    }
}
