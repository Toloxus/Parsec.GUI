using Microsoft.Toolkit.Mvvm.ComponentModel;
using Parsec.Shaiya.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Parsec.Core.Workspace
{
    public class FileViewModel : ObservableObject
    {
        #region Constructor

        public FileViewModel(SFile file)
        {
            _name = file.Name;
            _path = file.RelativePath;
        }

        public FileViewModel(SFolder folder)
        {
            _name = folder.Name;
            _path = folder.RelativePath;

            foreach (var f in folder.Subfolders.OrderBy(x => x.Name))
                Children.Add(new FileViewModel(f));

            foreach (var f in folder.Files.OrderBy(x => x.Name))
                Children.Add(new FileViewModel(f));
        }

        #endregion

        #region Properties

        private string _name;
        public string Name { get => _name; }

        private string _path;
        public string Path { get => _path; }

        public ObservableCollection<FileViewModel> Children { get; private set; } = new ObservableCollection<FileViewModel>();

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => SetProperty(ref _isExpanded, value);
        }

        public void Collapse()
        {
            IsExpanded = false;
            foreach (var child in Children)
                child.Collapse();
        }

        #endregion
    }
}
