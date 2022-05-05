using System.Collections.ObjectModel;
using Prism.Mvvm;

namespace LQClass.ModuleOfMenuManagement.DTOs;

public class GetFoldersModel
{
    public string ParentId { get; set; }
    public bool Selected { get; set; }
    public bool Disabled { get; set; }
    public string Text { get; set; }
    public string Value { get; set; }
}

public class GetFoldersModelList : BindableBase
{
    private ObservableCollection<GetFoldersModel> _getFoldersList;

    public ObservableCollection<GetFoldersModel> getFoldersList
    {
        get
        {
            return _getFoldersList;
            RaisePropertyChanged();
        }
        set => _getFoldersList = value;
    }
}