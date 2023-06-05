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

namespace Wpf2dSlider
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private Ellipse draggableEllipse;
        private bool isDragging;
        private Point offset;

        private Grid xSliderPanel;
        private TextBlock xSliderValueTbk;
        private Border xSliderBorder;

        private Grid ySliderPanel;
        private TextBlock ySliderValueTbk;
        private Border ySliderBorder;

        private int xRange = 10;
        private int yRange = 10;

        public MainWindow()
        {
            InitializeComponent();
            CanvasTT.Width = xRange;
            CanvasTT.Height = yRange;

            xSliderCanvas.Width = xRange;
            xSliderCanvas.Height = 1;

            ySliderCanvas.Width = 1;
            ySliderCanvas.Height = yRange;

            InitializeDraggableEllipse();
            CreateSlider();
            CreatySlider();
        }

        private void CreateSlider()
        {
            xSliderPanel = new Grid();

            Border border = new Border();
            border.CornerRadius = new CornerRadius(0.2, 0.2, 0.2, 0.2);
            border.Width = 10;
            border.Height = 0.5;
            border.BorderBrush = Brushes.Gainsboro;
            border.BorderThickness = new Thickness(0.05);
            border.Background = Brushes.Gainsboro;

            xSliderBorder = new Border();
            xSliderBorder.CornerRadius = new CornerRadius(0.2, 0.2, 0.2, 0.2);
            xSliderBorder.Width = 0.25;
            xSliderBorder.Height = 0.5;
            xSliderBorder.BorderBrush = Brushes.SeaGreen;
            xSliderBorder.BorderThickness = new Thickness(0.05);
            xSliderBorder.Background = Brushes.SeaGreen;

            Canvas.SetTop(xSliderBorder, 0.25);
            Canvas.SetTop(border, 0.25);

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Stroke = Brushes.MediumSeaGreen;
            ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            xSliderValueTbk = new TextBlock();
            xSliderValueTbk.Text = "0";
            xSliderValueTbk.Foreground = Brushes.White;
            xSliderValueTbk.FontSize = 0.4;
            xSliderValueTbk.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            xSliderValueTbk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            xSliderPanel.Children.Add(ellipse);
            // xSliderPanel.Children.Add(inEllipse);
            xSliderPanel.Children.Add(xSliderValueTbk);

            xSliderCanvas.Children.Add(border);
            xSliderCanvas.Children.Add(xSliderBorder);
            xSliderCanvas.Children.Add(xSliderPanel);

        }

        private void CreatySlider()
        {
            ySliderPanel = new Grid();

            Border border = new Border();
            border.CornerRadius = new CornerRadius(0.2, 0.2, 0.2, 0.2);
            border.Width = 0.5;
            border.Height = 10;
            border.BorderBrush = Brushes.Gainsboro;
            border.BorderThickness = new Thickness(0.05);
            border.Background = Brushes.Gainsboro;

            ySliderBorder = new Border();
            ySliderBorder.CornerRadius = new CornerRadius(0.2, 0.2, 0.2, 0.2);
            ySliderBorder.Width = 0.5;
            ySliderBorder.Height = 0.25;
            ySliderBorder.BorderBrush = Brushes.SeaGreen;
            ySliderBorder.BorderThickness = new Thickness(0.05);
            ySliderBorder.Background = Brushes.SeaGreen;

            Canvas.SetLeft(ySliderBorder, 0.25);
            Canvas.SetLeft(border, 0.25);

            Ellipse ellipse = new Ellipse();
            ellipse.Width = 1;
            ellipse.Height = 1;
            ellipse.Stroke = Brushes.MediumSeaGreen;
            ellipse.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ellipse.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            ySliderValueTbk = new TextBlock();
            ySliderValueTbk.Text = "0";
            ySliderValueTbk.Foreground = Brushes.White;
            ySliderValueTbk.FontSize = 0.4;
            ySliderValueTbk.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ySliderValueTbk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            ySliderPanel.Children.Add(ellipse);
            ySliderPanel.Children.Add(ySliderValueTbk);

            ySliderCanvas.Children.Add(border);
            ySliderCanvas.Children.Add(ySliderBorder);
            ySliderCanvas.Children.Add(ySliderPanel);
        }

        private void InitializeDraggableEllipse()
        {
            draggableEllipse = new Ellipse
            {
                Width = 1,
                Height = 1,
                Fill = Brushes.Transparent,
                Stroke = Brushes.Blue,
                StrokeThickness = 0.1
            };

            // 마우스 이벤트 처리기 등록
            draggableEllipse.MouseLeftButtonDown += DraggableEllipse_MouseLeftButtonDown;
            draggableEllipse.MouseLeftButtonUp += DraggableEllipse_MouseLeftButtonUp;
            draggableEllipse.MouseMove += DraggableEllipse_MouseMove;
            draggableEllipse.MouseRightButtonDown += DraggableEllipse_MouseRightButtonDown;

            // Canvas에 동그라미 추가
            CanvasTT.Children.Add(draggableEllipse);
        }

        private void DraggableEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // 마우스 클릭 시 드래그 시작
            isDragging = true;
            draggableEllipse.CaptureMouse();
            offset = e.GetPosition(draggableEllipse);
        }

        private void DraggableEllipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // 마우스 버튼 놓을 시 드래그 종료
            isDragging = false;
            draggableEllipse.ReleaseMouseCapture();
        }

        private void DraggableEllipse_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas.SetLeft(draggableEllipse, CanvasTT.ActualWidth / 2 - draggableEllipse.Width / 2);
            Canvas.SetTop(draggableEllipse, CanvasTT.ActualHeight / 2 - draggableEllipse.Height / 2);
        }

        private void DraggableEllipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // 마우스 드래그 시 동그라미 위치 업데이트
                Point currentPosition = e.GetPosition(CanvasTT);

                double newLeft = (int)(currentPosition.X - offset.X);
                double newTop = (int)(currentPosition.Y - offset.Y);

                if (newLeft >= 0 && newLeft <= CanvasTT.ActualWidth - draggableEllipse.Width)
                {
                    Canvas.SetLeft(draggableEllipse, newLeft);
                    Canvas.SetLeft(xSliderPanel, newLeft);
                    xSliderBorder.Width = newLeft + 0.25;
                    xSliderValueTbk.Text = newLeft.ToString();
                    // xSlider.Value = newLeft;
                }

                if (newTop >= 0 && newTop <= CanvasTT.ActualHeight - draggableEllipse.Height)
                {
                    Canvas.SetTop(draggableEllipse, newTop);
                    Canvas.SetTop(ySliderPanel, newTop);
                    ySliderBorder.Height = newTop + 0.25;
                    ySliderValueTbk.Text = newTop.ToString();

                    // ySlider.Value = 10 - newTop;
                }

                Console.WriteLine($"{newLeft}, {newTop}");
            }
        }
    }
}
