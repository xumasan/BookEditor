using System;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using BookEditor.Models;

namespace BookEditor.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            KifMoves = new ObservableCollection<Move>();
            NextMoves = new ObservableCollection<Move>();
        }

        public ObservableCollection<Move> KifMoves { get; }
        public ObservableCollection<Move> NextMoves { get; }
    }
}
