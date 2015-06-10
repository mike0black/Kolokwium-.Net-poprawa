using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Dices
{
    public class DiceViewModel : IDiceViewModel
    {
        private readonly IDispatcher _dispatcher;

        public DiceViewModel(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            LastResult = new TwoDicesResult();
            var Players = new List<UserInfo>();
            Players.Add(new UserInfo() {Name = "Adam"});
            Players.Add(new UserInfo() { Name = "Bartosz" });
            Players.Add(new UserInfo() { Name = "Cezary" });
            Players.Add(new UserInfo() { Name = "Damian" });
            SelectedPlayer = Players[0];
            randomizer = new Random();
        }

        private TwoDicesResult LastResult;

        private Random randomizer;

        public IList<UserInfo> Players
        {
            get { return this.Players; }
            private set { this.Players = value; }
        }

        public UserInfo SelectedPlayer
        {
            get
            {
                return this.SelectedPlayer;
            }
            set
            {
                if (value != this.SelectedPlayer)
                {
                    this.SelectedPlayer = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DiceResult FirstDiceResult
        {
            get { return this.LastResult.First; }
        }

        public DiceResult SecondDiceResult
        {
            get { return this.LastResult.Second; }
        }

        public System.Windows.Input.ICommand ThrowDicesForSelectedPlayerCommand
        {
            get { return null; }
        }


        private void ThrowDicesForSelectedPlayer()
        {
            int f = randomizer.Next(1, 7);
            int s = randomizer.Next(1, 7);
            TwoDicesResult result = new TwoDicesResult() { First = (DiceResult)f, Second = (DiceResult)s };
            LastResult = result;
            NotifyPropertyChanged();
            SelectedPlayer.Results.Add(result);
        }

        public IList<UserInfo> Cheaters
        {
            get { return this.Cheaters; }
        }

        public System.Windows.Input.ICommand ShowMeCheatersCommand
        {
            get { return null; }
        }

        //void ShowMeCheaters()
        //{
        //    Cheaters = from player in Players where ;
        //}

        public System.Windows.Input.ICommand SaveResultsCommand
        {
            get { return null; }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
