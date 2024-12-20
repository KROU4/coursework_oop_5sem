using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr_4_5
{
    internal class Order
    {
        [Key]
        public int order_id {  get; set; }
        public string order_name, date_start, time_start;
        public int user_id, hour_quantity, total_price;

        public string Order_name
        {
            get
            {
                return this.order_name;
            }
            set
            {
                this.order_name = value;
            }
        }

        public string Date_start
        {
            get
            {
                return date_start;
            }
            set
            {
                date_start = value;
            }
        }

        public string Time_start
        {
            get
            {
                return time_start;
            }
            set
            {
                time_start = value;
            }
        }

        public int User_id
        {
            get
            {
                return user_id;
            }
            set
            {
                user_id = value;
            }
        }
         
        public int Hour_quantity
        {
            get
            {
                return hour_quantity;
            }
            set
            {
                hour_quantity = value;
            }
        }

        public int Total_price
        {
            get
            {
                return total_price;
            }
            set
            {
                total_price = value;
            }
        }

        public Order() { }

        public Order(int user_id, string date_start, string time_start, int hour_quantity, string order_name, int total_price)
        {
            this.order_name = order_name;
            this.date_start = date_start;
            this.time_start = time_start;
            this.user_id = user_id;
            this.hour_quantity = hour_quantity;
            this.total_price = total_price;
        }
    }
}
