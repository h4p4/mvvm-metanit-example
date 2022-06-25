using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MVVM.Models
{
    public partial class Phone : INotifyPropertyChanged
    {
        private int _id;
        private string? _title;
        private string? _company;
        private int? _price;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { 
            get { return _id; }
            set 
            {
                _id = value;
                OnPropertyChanged("Id");
            } 
        }
        public string? Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string? Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPropertyChanged("Company");
            }
        }
        public int? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
            Helper.db.SaveChanges();
        }
    }
}
