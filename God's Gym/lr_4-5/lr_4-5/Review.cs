using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr_4_5
{
    internal class Review
    {
        [Key]
        public int review_id { get; set; }
        public int user_id;
        public string userName;
        public int rating;
        public string comment;
        public string date;

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

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public int Rating
        {
            get
            {
                return rating;
            }
            set { rating = value; }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public Review() { }
        public Review(int user_id, string userName, int rating, string comment, string date)
        {
            this.user_id = user_id;
            this.userName = userName;
            this.rating = rating;
            this.comment = comment;
            this.date = date;
        }
    }
}
