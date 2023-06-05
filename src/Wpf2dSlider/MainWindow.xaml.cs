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

        private Grid ySliderPanel;
        private TextBlock ySliderValueTbk;

        public MainWindow()
        {
            InitializeComponent();
            CanvasTT.Width = 10;
            CanvasTT.Height = 10;

            xSliderCanvas.Width = 10;
            xSliderCanvas.Height = 1;

            ySliderCanvas.Width = 1;
            ySliderCanvas.Height = 10;

            InitializeDraggableEllipse();
            CreateSlider();
            CreatySlider();
        }

        private void CreateSlider()
        {
            xSliderPanel = new Grid();


            Rectangle backgroundRect = new Rectangle();
            backgroundRect.Width = 10;
            backgroundRect.Height = 0.5;
            backgroundRect.Fill = Brushes.Crimson;
            Canvas.SetTop(backgroundRect, 0.25);

            Rectangle rect = new Rectangle();
            rect.Width = 1;
            rect.Height = 1;
            rect.Stroke = Brushes.Black;

            xSliderValueTbk = new TextBlock();
            xSliderValueTbk.Text = "0";
            xSliderValueTbk.Foreground = Brushes.White;
            xSliderValueTbk.FontSize = 0.4;
            xSliderValueTbk.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            xSliderValueTbk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            xSliderPanel.Children.Add(rect);
            xSliderPanel.Children.Add(xSliderValueTbk);

            xSliderCanvas.Children.Add(backgroundRect);
            xSliderCanvas.Children.Add(xSliderPanel);
        }

        private void CreatySlider()
        {
            ySliderPanel = new Grid();

            Rectangle backgroundRect = new Rectangle();
            backgroundRect.Width = 0.5;
            backgroundRect.Height = 10;
            backgroundRect.Fill = Brushes.Crimson;
            Canvas.SetLeft(backgroundRect, 0.25);

            Rectangle rect = new Rectangle();
            rect.Width = 1;
            rect.Height = 1;
            rect.Stroke = Brushes.Black;

            ySliderValueTbk = new TextBlock();
            ySliderValueTbk.Text = "0";
            ySliderValueTbk.Foreground = Brushes.White;
            ySliderValueTbk.FontSize = 0.4;
            ySliderValueTbk.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            ySliderValueTbk.VerticalAlignment = System.Windows.VerticalAlignment.Center;

            ySliderPanel.Children.Add(rect);
            ySliderPanel.Children.Add(ySliderValueTbk);

            ySliderCanvas.Children.Add(backgroundRect);
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
                StrokeThickness = 0.005
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
                    xSliderValueTbk.Text = newLeft.ToString();
                    // xSlider.Value = newLeft;
                }

                if (newTop >= 0 && newTop <= CanvasTT.ActualHeight - draggableEllipse.Height)
                {
                    Canvas.SetTop(draggableEllipse, newTop);
                    Canvas.SetTop(ySliderPanel, newTop);
                    ySliderValueTbk.Text = newTop.ToString();

                    // ySlider.Value = 10 - newTop;
                }

                Console.WriteLine($"{newLeft}, {newTop}");
            }
        }
    }
}
