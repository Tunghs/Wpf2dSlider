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

namespace Wpf2vSlider
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private Grid negativeSliderPanel;
        private TextBlock negativeSliderValueTbk;
        private Border negativeSliderBorder;

        private Grid positiveSliderPanel;
        private TextBlock positiveSliderValueTbk;
        private Border positiveSliderBorder;

        private bool isDragging;
        private Point offset;

        private int width = 10;

        public MainWindow()
        {
            InitializeComponent();

            SetUI();
        }

        private void SetUI()
        {
            negativeSliderCvs.Width = width;
            negativeSliderCvs.Height = 1;

            positiveSliderCvs.Width = width;
            positiveSliderCvs.Height = 1;

            CreateNegativeSlider();
            CreatePogitiveSlider();
        }

        private void CreateNegativeSlider()
        {
            negativeSliderPanel = new Grid();

            Border border = CreateBorder(10, 0.5, Brushes.Gainsboro);
            negativeSliderBorder = CreateBorder(0.25, 0.5, Brushes.Tomato);

            Canvas.SetTop(negativeSliderBorder, 0.25);
            Canvas.SetTop(border, 0.25);

            Ellipse ellipse = CreateEllipse(1, 1, Brushes.Red);
            negativeSliderValueTbk = CreateValueTextBlock("0", 0.4, Brushes.White);

            ellipse.MouseLeftButtonDown += NegativeEllipse_MouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += NegativeEllipse_MouseLeftButtonUp;
            ellipse.MouseMove += NegativeEllipse_MouseMove;

            negativeSliderPanel.Children.Add(ellipse);
            negativeSliderPanel.Children.Add(negativeSliderValueTbk);

            Grid grid = new Grid();
            grid.Width = 10;
            grid.HorizontalAlignment = HorizontalAlignment.Right;

            grid.Children.Add(negativeSliderBorder);
            negativeSliderBorder.HorizontalAlignment = HorizontalAlignment.Right;
            Canvas.SetTop(grid, 0.25);

            negativeSliderCvs.Children.Add(border);
            negativeSliderCvs.Children.Add(grid);
            negativeSliderCvs.Children.Add(negativeSliderPanel);

            Canvas.SetLeft(negativeSliderPanel, 9);
        }

        private void CreatePogitiveSlider()
        {
            positiveSliderPanel = new Grid();

            Border border = CreateBorder(10, 0.5, Brushes.Gainsboro);
            positiveSliderBorder = CreateBorder(0.25, 0.5, Brushes.SkyBlue);

            Canvas.SetTop(positiveSliderBorder, 0.25);
            Canvas.SetTop(border, 0.25);

            Ellipse ellipse = CreateEllipse(1, 1, Brushes.DeepSkyBlue);
            positiveSliderValueTbk = CreateValueTextBlock("0", 0.4, Brushes.White);

            ellipse.MouseLeftButtonDown += PositiveEllipse_MouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += PositiveEllipse_MouseLeftButtonUp;
            ellipse.MouseMove += PositiveEllipse_MouseMove;

            positiveSliderPanel.Children.Add(ellipse);
            positiveSliderPanel.Children.Add(positiveSliderValueTbk);

            positiveSliderCvs.Children.Add(border);
            positiveSliderCvs.Children.Add(positiveSliderBorder);
            positiveSliderCvs.Children.Add(positiveSliderPanel);
        }

        private Border CreateBorder(double width, double height, Brush fill)
        {
            Border border = new Border();
            border.CornerRadius = new CornerRadius(0.2, 0.2, 0.2, 0.2);
            border.Width = width;
            border.Height = height;
            border.BorderBrush = fill;
            border.BorderThickness = new Thickness(0.05);
            border.Background = fill;
            return border;
        }

        private Ellipse CreateEllipse(double width, double height, Brush fill)
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = width;
            ellipse.Height = height;
            ellipse.Stroke = fill;
            ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            return ellipse;
        }
        private TextBlock CreateValueTextBlock(string text, double fontSize, Brush foreground)
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = foreground;
            textBlock.FontSize = fontSize;
            textBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            textBlock.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            textBlock.IsHitTestVisible = false;
            return textBlock;
        }

        private void NegativeEllipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            var ellipse = sender as Ellipse;
            ellipse.ReleaseMouseCapture();
        }

        private void NegativeEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;

            var ellipse = sender as Ellipse;
            ellipse.CaptureMouse();
            offset = e.GetPosition(ellipse);
        }

        private void NegativeEllipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var ellipse = sender as Ellipse;
                var canvas = (Canvas)((Grid)ellipse.Parent).Parent;
                Point currentPosition = e.GetPosition(canvas);

                double leftPos = (int)(currentPosition.X - offset.X);

                if (leftPos <= canvas.ActualWidth - ellipse.Width)
                {
                    UpdateNegativePosition(leftPos, -1);
                }
            }
        }

        private void PositiveEllipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            var ellipse = sender as Ellipse;
            ellipse.ReleaseMouseCapture();
        }

        private void PositiveEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;

            var ellipse = sender as Ellipse;
            ellipse.CaptureMouse();
            offset = e.GetPosition(ellipse);
        }

        private void PositiveEllipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var ellipse = sender as Ellipse;
                var canvas = (Canvas)((Grid)ellipse.Parent).Parent;
                Point currentPosition = e.GetPosition(canvas);

                double leftPos = (int)(currentPosition.X - offset.X);

                if (leftPos <= canvas.ActualWidth - ellipse.Width)
                {
                    UpdatePositivePosition(leftPos, -1);
                }
            }
        }

        private void UpdateNegativePosition(double leftPos, double topPos)
        {
            if (leftPos >= 0)
            {
                Canvas.SetLeft(negativeSliderPanel, leftPos);
                negativeSliderBorder.Width = 10 - leftPos;
                negativeSliderValueTbk.Text = (9 - leftPos).ToString();

                Canvas.SetLeft(positiveSliderPanel, 9 - leftPos);
                positiveSliderBorder.Width = 10 - leftPos;
                positiveSliderValueTbk.Text = (9 - leftPos).ToString();
            }
        }

        private void UpdatePositivePosition(double leftPos, double topPos)
        {
            if (leftPos >= 0)
            {
                Canvas.SetLeft(negativeSliderPanel, 9 - leftPos);
                negativeSliderBorder.Width = leftPos + 0.25;
                negativeSliderValueTbk.Text = leftPos.ToString();

                Canvas.SetLeft(positiveSliderPanel, leftPos);
                positiveSliderBorder.Width = leftPos + 0.25;
                positiveSliderValueTbk.Text = leftPos.ToString();
            }
        }
    }
}
