using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using NeilX.DoubanFM.Services;
using NeilX.DoubanFM.UserControls.ControlsModel;
using NeilX.DoubanFM.View;

namespace NeilX.DoubanFM.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private Frame _navigationService;
        private SystemNavigationManager _backButton;
        private ObservableCollection<MenuListItem> _menuList;

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                // Code runs "for real"
                InitializeMenuData();
            }

        }

        public ObservableCollection<MenuListItem> MenuList
        {
            get { return _menuList; }
            set
            {
                Set(ref _menuList, value);
            }
        }




        public PlayerSessionService PlayerSession
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerSessionService>();
            }

        }





        #region Public  Methods
        public void SetupNavigationService(object sender, object e)
        {
            _navigationService = (Frame)sender;
            _navigationService.Navigate(typeof(View.PlayingView));
        }

        public void OpenSttingView()
        {
            MessengerInstance.Send("OpenSettingView","MainPage" );
        }


        public void MenuListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MenuListItem selectedItem = e.AddedItems[0] as MenuListItem;
            NavigateToView(selectedItem.GotoView);

        }

        #endregion


        #region private Helper Methods
        private void InitializeMenuData()
        {
            MenuList = new ObservableCollection<MenuListItem>();
            MenuListItem m = new MenuListItem
            {
                Name = "MyMusicView",
                IsEnabled = true,
                GotoView = MenuGotoView.MyMusicView,
                Icon =
                    "M48.850243,32.061127L40.837259,42.988266 36.466324,40.074284 26.633064,51.365921 61.962049,51.365921 57.591017,36.067707 52.492086,36.067707z M30.036716,26.927057C27.798449,26.927057 25.983376,28.740846 25.983376,30.979034 25.983376,33.21732 27.798449,35.031313 30.036716,35.031313 32.273483,35.031313 34.088657,33.21732 34.088657,30.979034 34.088657,28.740846 32.273483,26.927057 30.036716,26.927057z M17.322001,19.069001L65.332998,19.069001 65.332998,54.707001 17.322001,54.707001z M42.922237,0L50.809,15.735769 13.98841,15.735769 13.98841,49.422001 0,21.513156z"
            };

            MenuList.Add(m);

            m = new MenuListItem
            {
                Name = "RadioListView",
                IsEnabled = true,
                GotoView = MenuGotoView.RadioListView,
                Icon =
                    "M31.348,48.494C32.685356,48.494 33.770001,49.577489 33.770001,50.913598 33.770001,52.249612 32.685356,53.333001 31.348,53.333001 30.013346,53.333001 28.930001,52.249612 28.930001,50.913598 28.930001,49.577489 30.013346,48.494 31.348,48.494z M22.1227,48.494C23.457356,48.494 24.542,49.577489 24.542,50.913598 24.542,52.249612 23.457356,53.333001 22.1227,53.333001 20.785345,53.333001 19.702,52.249612 19.702,50.913598 19.702,49.577489 20.785345,48.494 22.1227,48.494z M35.26405,41.272998C36.60138,41.272998 37.686001,42.357621 37.686001,43.69365 37.686001,45.029576 36.60138,46.113 35.26405,46.113 33.929424,46.113 32.846001,45.029576 32.846001,43.69365 32.846001,42.357621 33.929424,41.272998 35.26405,41.272998z M26.508051,41.272998C27.845278,41.272998 28.930001,42.357621 28.930001,43.69365 28.930001,45.029576 27.845278,46.113 26.508051,46.113 25.173323,46.113 24.09,45.029576 24.09,43.69365 24.09,42.357621 25.173323,41.272998 26.508051,41.272998z M17.2318,41.272998C18.567579,41.272998 19.652001,42.357621 19.652001,43.69365 19.652001,45.029576 18.567579,46.113 17.2318,46.113 15.896121,46.113 14.813,45.029576 14.813,43.69365 14.813,42.357621 15.896121,41.272998 17.2318,41.272998z M44.021054,19.436998L46.029053,20.664867C46.758328,21.111492 46.98745,22.068645 46.540806,22.799085 46.17791,23.393704 45.478031,23.656512 44.837174,23.493527L44.815964,23.486991 44.755482,23.36905C44.331001,22.588596,43.814919,21.86488,43.221752,21.212421L43.130867,21.117109 43.312847,20.826513C43.580288,20.382326,43.817284,19.918125,44.021054,19.436998z M44.828007,13.720999L47.233292,13.786101C48.090233,13.810902 48.76614,14.523125 48.741337,15.381254 48.719235,16.238081 48.005619,16.913905 47.148682,16.890402L44.771996,16.825301C44.846211,16.353886 44.893116,15.874769 44.908718,15.386453 44.92432,14.821335 44.895718,14.266617 44.828007,13.720999z M21.976753,10.773001C26.979286,10.773001 31.208513,14.566015 32.614826,19.786035 33.396027,19.568634 34.218937,19.442332 35.069241,19.442332 40.136975,19.442332 44.245003,23.550448 44.245003,28.616866 44.245003,31.877278 42.539391,34.731389 39.976872,36.357696 38.948166,37.194999 37.640854,37.698899 36.213749,37.710603 35.840042,37.756803 35.459843,37.790002 35.069241,37.790002 34.687737,37.790002 34.312737,37.756803 33.940331,37.7119L10.320378,37.7119C9.942775,37.756803 9.5625736,37.790002 9.1705706,37.790002 4.1054971,37.790002 2.7073101E-07,33.840787 0,28.969668 2.7073101E-07,24.09855 4.1054971,20.150635 9.1705706,20.150635 9.871175,20.150635 10.552179,20.232635 11.208384,20.375936 12.447993,14.856316 16.789219,10.773001 21.976753,10.773001z M45.672386,7.174134C46.223133,7.1814017 46.751801,7.4817498 47.025681,8.0036976 47.424053,8.762845 47.132473,9.7023534 46.374729,10.100129L44.221188,11.232998C43.857914,10.249938,43.361851,9.3319101,42.749996,8.4998286L44.929535,7.352598C45.167145,7.2280803,45.422043,7.1708302,45.672386,7.174134z M22.363896,6.4076095C22.626137,6.4149718,22.889933,6.4890428,23.129202,6.6360126L25.232002,7.9243071C24.983002,8.2199299,24.749006,8.5292008,24.531187,8.8507769L24.325581,9.1660111 23.90588,9.0875378C23.275191,8.9828922 22.630835,8.9289981 21.976551,8.9289984 21.806952,8.9289981 21.637982,8.9326974 21.469708,8.9400355L21.134642,8.961946 21.056421,8.86325C20.704596,8.3712656 20.660401,7.6966183 20.996401,7.148304 21.275526,6.6914134 21.754027,6.4305587 22.251677,6.4085212 22.289,6.4068675 22.326431,6.4065576 22.363896,6.4076095z M33.776642,5.6300144C33.857746,5.6301656 33.939079,5.6313434 34.020622,5.6335616 39.242119,5.7754722 43.354096,10.120625 43.213494,15.340688 43.169556,16.971583 42.715012,18.494635 41.951519,19.813686L41.865544,19.954264 41.657589,19.790779C39.818852,18.414998 37.537552,17.599476 35.068867,17.599476 34.66787,17.599476 34.261574,17.622975 33.857876,17.668475 32.385815,13.866086 29.595959,11.014804 26.199589,9.711732L26.027451,9.6495949 26.288347,9.2962282C28.025217,7.0564322,30.74283,5.6243949,33.776642,5.6300144z M40.950501,2.1025438C41.249367,2.0946379 41.553959,2.1728296 41.825966,2.345706 42.547425,2.8067083 42.760941,3.7664986 42.298603,4.4892459L40.963902,6.5819988C40.17614,5.913939,39.294472,5.3526425,38.339997,4.9202757L39.681301,2.8197699C39.9702,2.3680468,40.452393,2.1157179,40.950501,2.1025438z M27.487883,1.5610685C28.038714,1.5662165,28.569157,1.8650618,28.844816,2.3860846L30.014,4.5899091C29.027098,4.9434433,28.105391,5.4317431,27.266875,6.033999L26.101694,3.8418369C25.700634,3.0827036 25.989704,2.1438856 26.746128,1.7428083 26.982916,1.6170683 27.237505,1.5587282 27.487883,1.5610685z M34.094422,0.00037670135C34.120896,-0.00020503998 34.147526,-0.00011539459 34.174298,0.00065898895 35.032345,0.022751808 35.706803,0.73763561 35.683403,1.593688L35.615608,4.0929995C35.11044,4.0083418 34.593571,3.9536633 34.066303,3.9406538 33.541535,3.9269848 33.023365,3.952373 32.512997,4.0096216L32.580693,1.5110502C32.603359,0.67979717,33.273724,0.018374443,34.094422,0.00037670135z"
            };

            MenuList.Add(m);
        }

        #region Title Bar
        private void SetupTitleBar()
        {
            //CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            //coreTitleBar.ExtendViewIntoTitleBar = true;
            _backButton = SystemNavigationManager.GetForCurrentView();
            _backButton.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            _backButton.BackRequested += BackButton_BackRequested;
            //     Window.Current.SetTitleBar(TitleGrid);
        }

        private void BackButton_BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (_navigationService.CanGoBack)
            {
                _navigationService.GoBack();
                if (!_navigationService.CanGoBack)
                {
                    _backButton.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                }
            }
        }
        #endregion
        private void NavigateToView(MenuGotoView view)
        {
            switch (view)
            {
                case MenuGotoView.RadioListView:
                    _navigationService.Navigate(typeof(RadioListView));
                    break;
                case MenuGotoView.MyMusicView:
                    _navigationService.Navigate(typeof(MyMusicView));
                    break;
                default:
                    break;
            }
        }

        #endregion




    }
}