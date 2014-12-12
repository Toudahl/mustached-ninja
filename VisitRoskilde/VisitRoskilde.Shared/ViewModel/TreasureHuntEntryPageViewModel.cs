using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using VisitRoskilde.Annotations;
using VisitRoskilde.Model;

namespace VisitRoskilde.ViewModel
{
    class TreasureHuntEntryPageViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<HuntTopicModel> _huntList;
        private HuntTopicModel _selectedHunt;

        public HuntTopicModel SelectedHunt
        {
            get { return _selectedHunt; }
            set { _selectedHunt = value; }
        }

        public TreasureHuntEntryPageViewModel()
        {
            HuntList=new ObservableCollection<HuntTopicModel>();
            HuntTopicModel h1=new HuntTopicModel();
            h1.Topic = "Vikings";
            h1.Time = "2:30 hours";
            h1.Theme = "Viking Oriented Hunt";
            h1.Reward = "Get 120% off on viking ship museum";
            h1.Start = "Outside Cathedral";
            h1.End = "Viking Musem";
            HuntList.Add(h1);


        }

        

        public ObservableCollection<HuntTopicModel> HuntList
        {
            get { return _huntList; }
            set
            {
                _huntList = value;
                OnPropertyChanged("HuntList");
            }
        }
        
        
        #region prop changed

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    
}
