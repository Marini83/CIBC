using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using static Cibc.PriceService.Prices;

namespace Cibc.PriceService
{
    public class RandomWalkPriceService : IPriceService
    {
        private const double TickSize = 0.01;
        private const int ShouldUpdatePrices = 1;

        private readonly Prices[] _prices;
        private readonly Random _randGenerator;
        private readonly Timer _timer;

        public RandomWalkPriceService()
        {
            _randGenerator = new Random();
            _timer = new Timer(1000); // update interval to 1000ms = 1s
            _timer.Elapsed += OnTimerHandler;
            _prices = new Prices[2];
        }


        public void Start()
        {
            lock (_prices)
            {
                if (_timer.Enabled)
                    throw new InvalidOperationException("Already started!");

                DoStart();
            }
        }

        private void DoStart()
        {
            for (int i = 0; i < 2; ++i)
            {
                var prices = new Prices();
                double mid = 0;

                if (i == 0)
                    mid = Math.Round(_randGenerator.NextDouble() * (270 - 240) + 240, 2);
                else if (i == 1)
                    mid = Math.Round(_randGenerator.NextDouble() * (210 - 180) + 180, 2);

                prices.BidPx = Math.Max(0, Math.Round(mid - TickSize, 2));
                prices.AskPx = Math.Max(0, Math.Round(mid + TickSize, 2));
                prices.BidQty = (uint)_randGenerator.Next(1, 10) * 10;
                prices.AskQty = (uint)_randGenerator.Next(1, 10) * 10;
                prices.Volume = 0;

                _prices[i] = prices;
            }

            DispatchPrices();
            _timer.Start();
        }


        private void DispatchPrices()
        {
            if (NewPricesArrived == null)
                return;

            for (int i = 0; i < 2; ++i)
                NewPricesArrived(this, (uint)i, _prices[i]);
        }


        private void OnTimerHandler(object source, ElapsedEventArgs e)
        {
            UpdatePrices();
        }


        private void UpdatePrices()
        {
            for (int i = 0; i < 2; ++i)
            {
                if (_randGenerator.Next(0, 2) != ShouldUpdatePrices)
                    continue;

                Prices prices = _prices[i];
                double oldBidPx = prices.BidPx;
                bool walkUp = Convert.ToBoolean(_randGenerator.Next(0, 2));

                // generate a random tick size from 0.01 to 1.00
                double tickToUse = Math.Round(_randGenerator.NextDouble() * (1.00 - 0.01) + 0.01, 2);
                tickToUse = walkUp ? tickToUse : -1 * tickToUse;

                prices.BidPx = Math.Round(prices.BidPx + tickToUse, 2);
                prices.AskPx = Math.Round(prices.AskPx + tickToUse, 2);
                prices.BidQty = (uint)_randGenerator.Next(1, 10) * 10;
                prices.AskQty = (uint)_randGenerator.Next(1, 10) * 10;
                prices.Volume += (uint)_randGenerator.Next(1, 10) * 10;

                if (NewPricesArrived != null)
                    NewPricesArrived(this, (uint)i, prices);

                // Raise PriceChanged event
                if (oldBidPx != prices.BidPx)
                {
                    PriceChangeDirection direction = prices.BidPx > oldBidPx
                        ? PriceChangeDirection.Up
                        : PriceChangeDirection.Down;
                    PriceChanged?.Invoke(i, direction);
                }
            }
        }



        public bool Stop()
        {
            lock (_prices)
            {
                if (!IsStarted)
                    return false;

                _timer.Stop();
                return true;
            }
        }


        public bool IsStarted
        {
            get { return _timer.Enabled; }
        }

        public double Interval
        {
            get { return _timer.Interval; }
            set { _timer.Interval = value; }
        }

        public event PriceUpdateDelegate NewPricesArrived;
        public event Action<int, PriceChangeDirection> PriceChanged;
    }
}
