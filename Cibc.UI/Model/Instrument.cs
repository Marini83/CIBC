using System;
using System.ComponentModel;
using Cibc.PriceService;
using static Cibc.PriceService.Prices;

namespace Cibc.UI.Model
{
    public class Instrument : INotifyPropertyChanged
    {
        private uint _instrumentId;
        private double _askPx;
        private DeltaIndicator _deltaIndicator;
        private PriceChangeDirection _priceChangeDirection;
        private RandomWalkPriceService _service;

        public uint InstrumentId
        {
            get { return _instrumentId; }
            set
            {
                _instrumentId = value;
                OnPropertyChanged("InstrumentId");
            }
        }

        public double BidPx { get; set; }
        public uint BidQty { get; set; }

        public double AskPx
        {
            get { return _askPx; }
            set
            {
                if (_deltaIndicator == null)
                {
                    _deltaIndicator = new DeltaIndicator(InstrumentId, value, _askPx);
                }
                else
                {
                    _deltaIndicator.PreviousPrice = _deltaIndicator.CurrentPrice;
                    _deltaIndicator.CurrentPrice = value;
                }

                _askPx = value;
                OnPropertyChanged("AskPx");
                OnPropertyChanged("DeltaIndicator");
            }
        }

        public uint AskQty { get; set; }
        public uint Volume { get; set; }

        public DeltaIndicator DeltaIndicator
        {
            get { return _deltaIndicator; }
            set
            {
                _deltaIndicator = value;
                OnPropertyChanged("DeltaIndicator");
            }
        }

        public PriceChangeDirection PriceChangeDirection
        {
            get { return _priceChangeDirection; }
            set
            {
                _priceChangeDirection = value;
                OnPropertyChanged("PriceChangeDirection");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Instrument()
        {
        }

        public Instrument(RandomWalkPriceService service, uint instrumentId, double bidPx, uint bidQty, double askPx, uint askQty, uint vol)
        {
            _service = service;
            _service.PriceChanged += OnPriceChanged;

            InstrumentId = instrumentId;
            BidPx = bidPx;
            BidQty = bidQty;
            AskPx = askPx;
            AskQty = askQty;
            Volume = vol;
        }

        private void OnPriceChanged(int instrumentId, PriceChangeDirection direction)
        {
            if (InstrumentId == instrumentId)
            {
                PriceChangeDirection = direction;
            }
        }

        
    }
}
