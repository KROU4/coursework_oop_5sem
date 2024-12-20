using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr_4_5
{
    public enum Range
    {
        General,
        VIP
    }
    public class Offer
    {
        public string name;
        public string description;
        public float price;
        public string time;
        private Range range;

        public Range Range
        {
            get 
            { 
                return range;
            }
            set
            {
                range = value;
            }
        }
        
        public string Name 
        { 
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }
        public Offer(string name, string description, float price, string time, Range range) 
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.time = time;
            this.Range = range;
        }
    }
}
