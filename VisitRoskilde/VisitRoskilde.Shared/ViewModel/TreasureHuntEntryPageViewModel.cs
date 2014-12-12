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
            set
            {
                _selectedHunt = value;
                OnPropertyChanged("SelectedHunt");
            }
        }

        public TreasureHuntEntryPageViewModel()
        {
            HuntList=new ObservableCollection<HuntTopicModel>();
            HuntTopicModel h1=new HuntTopicModel();
            h1.Topic = "Vikings";
            h1.Time = "2:30 hours";
            h1.Theme = "Viking oriented hunt";
            h1.Reward = "Get 120% off on viking ship museum upon completion";
            h1.Start = "Outside Cathedral";
            h1.End = "Viking Musem";
            

            HuntTopicModel h2=new HuntTopicModel();
            h2.Topic = "Art";
            h2.Time = "40 minutes";
            h2.Theme = "Art oriented hunt";
            h2.Reward = "Katharsis upon completion";
            h2.Start = "Roskilde station";
            h2.End = "Roskilde Museum";

            HuntTopicModel h3=new HuntTopicModel();

            h3.Topic = "Nature";
            h3.Time = "3 hours";
            h3.Theme = "Some nature for you nerds";
            h3.Reward = "Fresh air upon completion";
            h3.Start = "Scandic Roskilde";
            h3.End = "Roskilde Fjord";

            HuntTopicModel h4 = new HuntTopicModel();

            h4.Topic = "Kids";
            h4.Time = "2 hours";
            h4.Theme = "Something to get kids of yo ass";
            h4.Reward = "20% off at Jensens Bofhus upon completion";
            h4.Start = "ElGiganten Roskilde";
            h4.End = "Kvickly Hydrehoj";

            HuntList.Add(h1);
            HuntList.Add(h2);
            HuntList.Add(h3);
            HuntList.Add(h4);
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
