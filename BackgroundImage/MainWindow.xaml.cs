using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace BackgroundImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const int SPI_SETDESKWALLPAPER = 20;
        public const int SPIF_UPDATEINIFILE = 1;
        public const int SPIF_SENDCHANGE = 2;

        //profile path(s)
        public const string mainPath = "./Profiles";
        public const string profilePathSchema = "Collection";

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Load;
        }
            //SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, "C:\\Users\\Mardani.Amirhosein\\Desktop\\DeviathanSourceControlDark.png", SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

        private void MainWindow_Load(object sender, RoutedEventArgs e)
        {
            int profileExists = checkDir();

            ImageTemplateList.IsEnabled = false;
            ImageTemplate.Visibility = Visibility.Hidden;

        }

        private int checkDir()
        {
            var bools = Directory.Exists(mainPath);
            if (!bools)
            {
                Directory.CreateDirectory(mainPath);
                ImageProfiles.Items.Clear();
                ImageProfiles.Items.Add("Nothing Found!");
                return 0;
            }
            else
            {
                var subDirectories = Directory.GetDirectories(mainPath);

                int profileIsExists = 0;
                foreach (var f in subDirectories)
                {
                    if (f.Contains(profilePathSchema))
                        profileIsExists++;
                }

                if (profileIsExists == 0)
                {
                    ImageProfiles.Items.Clear();
                    ImageProfiles.Items.Add("Nothing Found!");
                }
                return profileIsExists;
            }
        }
    }
}
