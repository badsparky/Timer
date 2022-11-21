using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;


namespace timer_vr4
{
    public class Model_music
    {
        public string Name { get; set; }
        public string Text { get { return Link.Split('\\').Last(); } }
        public string Link { get; set; }
    }
    /// <summary>
    /// SettingPage.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            List<Model_music> Musics=new List<Model_music>();
            ListInitializr(Musics);
            InitializeComponent();
            DataContext=Musics;
        }

        private void ChoiceButtom_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void ListInitializr(List<Model_music> Musics)
        {
            string path = @"\data\musics.json";
            string tmp_data;
            try
            {
                using(var sr=new StreamReader(path))
                {
                    tmp_data = sr.ReadToEnd();
                }
                Musics=JsonConvert.DeserializeObject<List<Model_music>>(tmp_data);

            }
            catch (FileNotFoundException)
            {
                Musics.Add(new Model_music() { Name="ビビット",Link=""});
                Musics.Add(new Model_music() { Name="ビビット",Link=""});
                Musics.Add(new Model_music() { Name="ビビット",Link=""});
                Musics.Add(new Model_music() { Name="ビビット",Link=""});
            }
        }

    }
}
