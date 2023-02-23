using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductiveApp_Ava.ViewModels
{
    public class PathLinkViewModel: ViewModelBase
    {
        private string _name = string.Empty;
        public string name
        {
            get { return _name; }
            set { _name = value; this.RaisePropertyChanged(nameof(name)); }
        }

        public ulong id;

        public PathLinkViewModel(ulong id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public void LoadBoardFromPath()
        {
            MainWindowViewModel.LoadBoardFromDatabase(id);
        }
    }
}
