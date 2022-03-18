using System;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace LQClass.CustomControls.TabControlHelper;

public class CloseableHeader : BindableBase
{
    private string _Icon;

    public CloseableHeader(string key, string title, bool canClose)
    {
        Key = key;
        Title = title;
        CanClose = canClose;
        CloseCommand = new DelegateCommand(Close);
    }

    public ICommand CloseCommand { get; }
    public string Key { get; }

    public string Icon
    {
        get => _Icon;
        set => SetProperty(ref _Icon, value);
    }

    public string Title { get; }
    public bool CanClose { get; }

    public Visibility Visibility => CanClose ? Visibility.Visible : Visibility.Collapsed;

    public event Action RequestClose;

    private void Close()
    {
        if (RequestClose != null) RequestClose();
    }
}