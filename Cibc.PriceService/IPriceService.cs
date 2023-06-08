using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Cibc.PriceService.Prices;

namespace Cibc.PriceService
{
    public delegate void PriceUpdateDelegate(IPriceService sender, uint instrumentID, IPrices prices);

    public interface IPriceService
    {
        void Start();
        bool Stop();
        bool IsStarted { get; }

        event PriceUpdateDelegate NewPricesArrived;

        event Action<int, PriceChangeDirection> PriceChanged;

    }
}
