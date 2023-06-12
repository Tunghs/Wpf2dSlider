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

namespace SequenceViewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<string> images = new List<string>();
            images.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_0.png");
            images.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_1.png");
            images.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_2.png");
            images.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_3.png");

            Dictionary<string, int> sequenceDic = new Dictionary<string, int>();
            sequenceDic.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_0.png", 1);
            sequenceDic.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_1.png", 2);
            sequenceDic.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_2.png", 3);
            sequenceDic.Add(@"C:\Users\selee\Desktop\TV Test Images\187404_2_3.png", 4);

            string[] imageArry = new string[4];
            imageArry[0] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_0.png";
            imageArry[1] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_1.png";
            imageArry[2] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_2.png";
            imageArry[3] = @"C:\Users\selee\Desktop\TV Test Images\187404_2_3.png";

            ImageIcl.ItemsSource = sequenceDic;
            ImageIcl.Loaded += ImageIcl_Loaded;
     
        }

        private void ImageIcl_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            ImageIcl.ApplyTemplate();
            for (var index = 0; index < ImageIcl.Items.Count; index++)
            {
                UIElement container = (UIElement)ImageIcl.ItemContainerGenerator.ContainerFromIndex(0);
                //grid.UpdateLayout();

                //for (var itemCount = 0; itemCount < grid.Children.Count; itemCount++)
                //{

                //}

            }

            foreach (var item in ImageIcl.Items)
            {
                var cp = (ContentPresenter)ImageIcl.ItemContainerGenerator.ContainerFromItem(item);
                
                Grid grid1 = (Grid)cp.ContentTemplate.FindName("PART_InsideGrid", cp);

                grid1.MouseEnter += Grid_MouseEnter;
                grid1.MouseLeave += Grid_MouseLeave;
                grid1.MouseLeftButtonDown += Grid1_MouseLeftButtonDown;
            }

        }

        private void Grid1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pair = (KeyValuePair<string, int>)((Grid)sender).DataContext;
            
            MessageBox.Show(pair.Key);
        }

        private T FindChild<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            if (parent == null)
                return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && child is FrameworkElement frameworkElement && frameworkElement.Name == name)
                    return typedChild;

                var foundChild = FindChild<T>(child, name);
                if (foundChild != null)
                    return foundChild;
            }

            return null;
        }

        private T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                    return typedChild;

                var foundChild = FindChild<T>(child);
                if (foundChild != null)
                    return foundChild;
            }

            return null;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            Grid grid3 = grid.FindName("SelectGrid") as Grid;
            grid3.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            Grid grid3 = grid.FindName("SelectGrid") as Grid;
            grid3.Visibility = Visibility.Collapsed;
        }
    }
}
