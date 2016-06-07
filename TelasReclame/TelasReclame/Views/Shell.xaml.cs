using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco é documentado em http://go.microsoft.com/fwlink/?LinkId=234238

namespace TelasReclame.Views
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class Shell : Page
    {
        public Shell()
        {
            this.InitializeComponent();
            PageFrame.Navigate(typeof(Home));            
            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) =>
            {
                // This is the missing line!
                e.Handled = true;

                // Close the App if you are on the startpage
                if (PageFrame.CurrentSourcePageType == typeof(Home))
                    App.Current.Exit();

                // Navigate back
                if (PageFrame.CanGoBack)
                {
                    PageFrame.GoBack();
                }
            };

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;
        }

        private void HamburgerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HamburgerList.SelectedItem.Equals(HamburgerItemHome))
                PageFrame.Navigate(typeof(Home));
            else if (HamburgerList.SelectedItem.Equals(HamburgerItemProfile))
                PageFrame.Navigate(typeof(Profile));
        }

    }
}
