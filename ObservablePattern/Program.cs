using System.Collections.Generic;
using System;

namespace ObservablePattern
{
    class Program
    {
        static void Main(string[] args)
        {
            IBM ibm = new IBM(120, "IBM");
            ibm.Attach(new Investor("Koç"));
            ibm.Attach(new Investor("Fiat"));
            ibm.Attach(new Investor("Bmw"));

            ibm.Price = 120.12;
            ibm.Price = 130.12;
            ibm.Price = 110.12;
            ibm.Price = 120.12;
            ibm.Price = 140.12;

            Console.ReadKey();

        }
    }

    #region  PatternLibrary

    /// <summary>
    ///  The Observer interface...
    /// </summary>
    public interface IInvestor{
        void Update(Stock stock);
    }

    /// <summary>
    ///  Concrete Observer Pattern
    /// </summary>
    public class Investor : IInvestor
    {
        private string _name;
        private Stock _stock;

        public Stock Stock{
            get { return _stock; }
            set { _stock = value; }
        }


        public Investor(string name){
            this._name = name;


        }
        public void Update(Stock stock)
        {
            Console.WriteLine("Notified {0} of {1}'s " +
             "change to {2:C}", _name, stock.Symbol, stock.Price);
        }
    }

    /// <summary>
    ///  The subject abstract class
    /// </summary>
    public abstract class Stock
    {

        public Stock(double price, string symbol)
        {
            this._price = price;
            this._symbol = symbol;

        }

         string _symbol;
         double _price;

        public List<IInvestor> Investors = new List<IInvestor>();


        public void Attach(IInvestor investor)
        {
            Investors.Add(investor);
        }
        public void Detach(IInvestor investor)
        {
            Investors.Remove(investor);

        }

        public void Notify()
        {
            foreach (var investor in Investors)
            {
                investor.Update(this);
            }
            Console.WriteLine(" ");
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        public string Symbol{
            get { return _symbol; }
        }

    }

    /// <summary>
    /// Concrete Subject Class...
    /// </summary>
    public class IBM : Stock
    {
        public IBM(double price, string symbol) 
           : base(price, symbol)
        {
        }
    }
    #endregion

}
