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

namespace ImageDragAndDrop
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] imagePaths;

        public MainWindow()
        {
            InitializeComponent();
            imagePaths = new string[4];
            imagePaths[0] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_0.png";
            imagePaths[1] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_1.png";
            imagePaths[2] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_2.png";
            imagePaths[3] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_3.png";

            List<Grid> grids = new List<Grid>()
            {
                Grid0, Grid1, Grid2, Grid3
            };

            foreach(var grid in grids)
            {
                grid.MouseMove += Grid2_MouseMove;
                grid.Drop += Grid2_Drop;
                int index = int.Parse(grid.Tag.ToString());

                ((Image)grid.Children[0]).Source = new BitmapImage(new Uri(imagePaths[index], UriKind.Absolute));
            }
        }

        private void Grid2_Drop(object sender, DragEventArgs e)
        {
            string tag = ((Grid)sender).Tag.ToString();
            int index = int.Parse(tag);

            if (e.Data.GetDataPresent("GridData"))
            {
                Grid preGrid = (Grid) e.Data.GetData("GridData");
                int preIndex = int.Parse(preGrid.Tag.ToString());

                string temp = imagePaths[index];
                imagePaths[index] = imagePaths[preIndex];
                imagePaths[preIndex] = temp;

                ((Image)((Grid)sender).Children[0]).Source = new BitmapImage(new Uri(imagePaths[index], UriKind.Absolute));
                ((Image)preGrid.Children[0]).Source = new BitmapImage(new Uri(imagePaths[preIndex], UriKind.Absolute));
            }
        }

        private (string, string) tt()
        {
            return ("", "");
        }

        private void Grid2_MouseMove(object sender, MouseEventArgs e)
        {
            string tag = ((Grid)sender).Tag.ToString();
            int index = int.Parse(tag);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject();
                data.SetData("GridData", sender);

                DragDrop.DoDragDrop((Grid)sender, data, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }
    }
}
