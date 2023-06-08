using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cibc.PriceService.Prices;

namespace Cibc.UI.Model
{
    public class DeltaIndicator : INotifyPropertyChanged
    {
        public DeltaIndicator(uint instrumentId, double currentPrice, double previousPrice)
        {
            this.instrumentId = instrumentId;
            this.CurrentPrice = currentPrice;
            this.PreviousPrice = previousPrice;
        }

        private uint _instrumentId;
        public uint instrumentId
        {
            get { return _instrumentId; }
            set
            {
                _instrumentId = value;
                OnPropertyChanged("instrumentId");
            }
        }

        private double _currentPrice = 0.00;
        public double CurrentPrice
        {
            get { return _currentPrice; }
            set
            {
                if (_currentPrice != value)
                {
                    PreviousPrice = _currentPrice;
                    _currentPrice = value;

                    PriceChangeDirection = (_currentPrice > PreviousPrice) ?
                        PriceChangeDirection.Up : PriceChangeDirection.Down;

                    OnPropertyChanged("CurrentPrice");
                    OnPropertyChanged("ChangeInPrice");
                    OnPropertyChanged("PriceChangeDirection");
                }
            }
        }

        private double _previousPrice = 0.00;
        public double PreviousPrice
        {
            get { return _previousPrice; }
            set
            {
                _previousPrice = value;
                OnPropertyChanged("PreviousPrice");
                OnPropertyChanged("ChangeInPrice");
            }
        }

        public double ChangeInPrice
        {
            get
            {
                return CurrentPrice - PreviousPrice;
            }
        }
        public PriceChangeDirection PriceChangeDirection { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}