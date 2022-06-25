using Microsoft.Data.SqlClient;
using MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVM
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Phone _selectedPhone;

        public ObservableCollection<Phone> Phones { get; set; }
        //private ObservableCollection<Phone> _phones;
        //public ObservableCollection<Phone> Phones 
        //{
        //    get { return _phones; }
        //    set
        //    {
        //        _phones = value;
        //        OnPropertyChanged("Phones");
        //    }
        //}

        private RelayCommand _addCommand;
        private RelayCommand _deleteCommand;
        private RelayCommand _saveCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                    (_addCommand = new RelayCommand(obj =>
                    {
                        Phone phone = new Phone() { Id = 1 };
                        if (Phones.Any())
                            phone = new Phone() { Id = Phones.Last().Id + 1 };
                        Phones.Add(phone);
                        SelectedPhone = phone;
                        Helper.db.Phones.Add(phone);
                        Helper.db.SaveChanges();

                    }));
            }
        }
        //public RelayCommand SaveCommand
        //{
        //    get
        //    {
        //        return _saveCommand ??
        //            (_saveCommand = new RelayCommand(obj =>
        //            {
        //                var phone = SelectedPhone;
        //                Helper.db.Phones.Add(phone);
        //                Helper.db.SaveChanges();
        //            }));
        //    }
        //}
        public RelayCommand DeleteCommand
        {
            get
            {
                return _deleteCommand ??
                    (_deleteCommand = new RelayCommand(obj =>
                    {
                        if (Helper.db.Phones.Any() )
                        {
                            var phone_ = Helper.db.Phones.Where(x => x.Id == SelectedPhone.Id);
                            if (SelectedPhone != null && phone_.Any())
                            {
                                var phone = phone_.FirstOrDefault();
                                Helper.db.Phones.Remove(phone);
                                Phones.Remove(phone);
                            }
                            Helper.db.SaveChanges();
                            SelectedPhone = null;
                        }
                        //if (Phones.Any())
                        //    SelectedPhone = Phones.First();
                    }));
            }
        }
        public Phone SelectedPhone
        {
            get { return _selectedPhone; }
            set
            {
                _selectedPhone = value;
                OnPropertyChanged("SelectedPhone");
            }
        }
        public ApplicationViewModel()
        {
            Phones = new ObservableCollection<Phone>(Helper.db.Phones);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
