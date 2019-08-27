using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food2Weight.ViewModels
{
    public abstract class WeightViewModel : BusyViewModel
    {
        private char firstDigit;
        private char secondDigit;
        private char thirdDigit;
        private double weight;
        private char firstDecimalPlace;
        private DateTime date;
        private TimeSpan time;

        protected WeightViewModel()
        {
            ChangeWeightCommand = new Command<string>(ChangeWeight);
            SubmitCommand = new Command(async () => await Submit());
            Weight = 100;
        }

        public Command<string> ChangeWeightCommand { get; }

        public Command SubmitCommand { get; }

        public double Weight
        {
            get => weight;
            set
            {
                if (Equals(weight, value)) return;
                if (value < 0) return;
                if (value > 999.9) return;
                weight = value;
                var weightString = $"{weight:0.0}";
                if (weightString.Length == 3)
                {
                    FirstDigit = ' ';
                    SecondDigit = ' ';
                    ThirdDigit = weightString[0];
                    FirstDecimalPlace = weightString[2];
                }
                if (weightString.Length == 4)
                {
                    FirstDigit = ' ';
                    SecondDigit = weightString[0];
                    ThirdDigit = weightString[1];
                    FirstDecimalPlace = weightString[3];
                }
                if (weightString.Length == 5)
                {
                    FirstDigit = weightString[0];
                    SecondDigit = weightString[1];
                    ThirdDigit = weightString[2];
                    FirstDecimalPlace = weightString[4];
                }
                OnPropertyChanged(nameof(FirstDigit));
                OnPropertyChanged(nameof(SecondDigit));
                OnPropertyChanged(nameof(ThirdDigit));
                OnPropertyChanged(nameof(FirstDecimalPlace));
            }
        }

        public char FirstDigit
        {
            get => firstDigit;
            set
            {
                if (Equals(firstDigit, value)) return;
                firstDigit = value;
                OnPropertyChanged();
            }
        }

        public char SecondDigit
        {
            get => secondDigit;
            set
            {
                if (Equals(secondDigit, value)) return;
                secondDigit = value;
                OnPropertyChanged();
            }
        }

        public char ThirdDigit
        {
            get => thirdDigit;
            set
            {
                if (Equals(thirdDigit, value)) return;
                thirdDigit = value;
                OnPropertyChanged();
            }
        }

        public char FirstDecimalPlace
        {
            get => firstDecimalPlace;
            set
            {
                if (Equals(firstDecimalPlace, value)) return;
                firstDecimalPlace = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                if (Equals(date, value)) return;
                date = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time
        {
            get => time;
            set
            {
                if (Equals(time, value)) return;
                time = value;
                OnPropertyChanged();
            }
        }

        protected void ChangeWeight(object obj)
        {
            if (obj is string change)
            {
                Weight += double.Parse(change);
            }
        }

        protected abstract Task Submit();
    }
}