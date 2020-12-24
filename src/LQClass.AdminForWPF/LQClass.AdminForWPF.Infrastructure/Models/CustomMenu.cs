using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace LQClass.AdminForWPF.Infrastructure.Models
{
	public class CustomMenu : BindableBase
	{
		public const string KEY_OF_HOME = "Home";

		public string ID { get; set; }
		public string ParentID { get; set; }
		public string Name { get; set; }

		public CustomMenu(int level, string id, string parentID, string name, string icon)
		{
			this.Level = level;
			this.ID = id;
			this.ParentID = parentID;
			this.Name = name;
			this.Icon = icon;
		}
		public int Level { get; set; }
		public string Icon { get; set; }

		public ObservableCollection<CustomMenu> Children { get; set; } = new ObservableCollection<CustomMenu>();


		private bool _IsSelected = false;
		/// <summary>
		/// new IsSelected
		/// </summary>
		public bool IsSelected
		{
			get { return this._IsSelected; ; }
			set
			{
				this.SetProperty(ref _IsSelected, value);
			}
		}
		private bool _IsExpanded = false;
		/// <summary>
		/// new IsExpanded
		/// </summary>
		public bool IsExpanded
		{
			get { return this._IsExpanded; ; }
			set
			{
				this.SetProperty(ref _IsExpanded, value);
			}
		}
	}
}
