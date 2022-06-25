using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MVVM.Models;

namespace MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _canEditPanelClose = true; // Отвечает за то, может ли панель редактирования быть свернутой
        private int _animationFramesCount = 64; // количество кадров анимации
        private int _tabAnimationSpeed = 2; // скорость анимации (по умолчанию) где 1 - медленно, 2 - быстрее, 4, 8, 16, 32 - самая быстрая анимация, 64 - нет анимации 
        private PhonesDbContext _db = Helper.db;
        private ApplicationViewModel _applicationViewModel = new ApplicationViewModel();
        public MainWindow()
        {
            InitializeComponent();
            
            _db.Phones.Load();
            DataContext = _applicationViewModel;
            SlowMenuItem.IsChecked = true;
        }
        private void PhoneListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Открывать/закрывать панель редактирования на двойной клик
            if (GlobalGridRightColumn.Width.ToString() == "0*")
            {
                OpenTab();
                return;
            }
                CloseTab();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Открывать панель редактирования при добавлении нового элемента
            OpenTab();
        }

        private void EscBtn_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие панели редактирования на Esc
            CloseTab();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            // Открытие панели редактирования на Enter
            // (Будет открыта панель с тем элементом который выбран,
            // если не выбран - панель не откроется)
            if (PhoneListBox.SelectedIndex != -1)
                OpenTab();
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            // Закрытие панели редактирования при удалении элемента
            CloseTab();
        }

        private async void OpenTab()
        {
            if (GlobalGridRightColumn.Width.ToString() == "0*")
            {
                for (int currentFrame = 0; currentFrame < _animationFramesCount; currentFrame += _tabAnimationSpeed)
                {
                    GlobalGridRightColumn.Width = new GridLength(1.33 / _animationFramesCount * currentFrame, GridUnitType.Star);
                    await Task.Delay(1);
                }
                GlobalGridRightColumn.Width = new GridLength(1.33, GridUnitType.Star);
            }
        }
        private async void CloseTab()
        {
            if (GlobalGridRightColumn.Width.ToString() == "1.33*" && _canEditPanelClose == true)
            {
                for (int currentFrame = _animationFramesCount; currentFrame > 0; currentFrame -= _tabAnimationSpeed)
                {
                    GlobalGridRightColumn.Width = new GridLength(1.33 / _animationFramesCount * currentFrame, GridUnitType.Star);
                    await Task.Delay(1);
                }
                GlobalGridRightColumn.Width = new GridLength(0, GridUnitType.Star);
            }
        }
        private void VerySlowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _tabAnimationSpeed = 1;
            ChangeCheckedMenuItem(VerySlowMenuItem);

        }
        private void SlowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _tabAnimationSpeed = 2;
            ChangeCheckedMenuItem(SlowMenuItem);
        }

        private void MediumMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _tabAnimationSpeed = 4;
            ChangeCheckedMenuItem(MediumMenuItem);
        }

        private void FastMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _tabAnimationSpeed = 8;
            ChangeCheckedMenuItem(FastMenuItem);
        }

        private void VeryFastMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _tabAnimationSpeed = 16;
            ChangeCheckedMenuItem(VeryFastMenuItem);
        }

        private void DisableAnimMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _tabAnimationSpeed = 64;
            ChangeCheckedMenuItem(DisableAnimMenuItem);
        }

        private void ChangeCheckedMenuItem(MenuItem item)
        {
            VerySlowMenuItem.IsChecked = false;
            SlowMenuItem.IsChecked = false;
            MediumMenuItem.IsChecked = false;
            FastMenuItem.IsChecked = false;
            VeryFastMenuItem.IsChecked = false;
            DisableAnimMenuItem.IsChecked = false;
            item.IsChecked = true;
        }
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DisplayEditPanelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (DisplayEditPanelMenuItem.IsChecked)
            {
                OpenTab();
                _canEditPanelClose = false;
                return;
            }
            _canEditPanelClose = true;
            CloseTab();
        }


    }
}
