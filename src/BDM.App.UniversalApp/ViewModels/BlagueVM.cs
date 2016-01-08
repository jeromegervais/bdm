using BDM.App.UniversalApp.Mvvm.ViewModel;
using BDM.Common.Model;
using System;

namespace BDM.App.UniversalApp.ViewModels
{
    public class BlagueVM : NotificationObject
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public string Author { get; set; }

        public int _nbGoods;
        public int NbGoods
        {
            get
            {
                return _nbGoods;
            }
            set
            {
                _nbGoods = value;
                RaisePropertyChanged();
            }
        }

        public int _nbBads;
        public int NbBads
        {
            get
            {
                return _nbBads;
            }
            set
            {
                _nbBads = value;
                RaisePropertyChanged();
            }
        }

        public int CategoryId { get; set; }

        public DateTime PublicationDate { get; set; }

        public BlagueVM(Blague blague)
        {
            Id = blague.Id;
            Text = blague.Text;
            Author = blague.Author;
            NbGoods = blague.NbGoods;
            NbBads = blague.NbBads;
            CategoryId = blague.CategoryId;
            PublicationDate = blague.PublicationDate;
        }
    }
}
