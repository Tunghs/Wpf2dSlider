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

        public MainWindow()
        {
            InitializeComponent();
            CanvasTT.Width = 1;
            CanvasTT.Height = 1;

            InitializeDraggableEllipse();
        }

        private void InitializeDraggableEllipse()
        {
            draggableEllipse = new Ellipse
            {
                Width = 0.05,
                Height = 0.05,
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

                double newLeft = currentPosition.X - offset.X;
                double newTop = currentPosition.Y - offset.Y;

                if (newLeft >= 0 && newLeft <= CanvasTT.ActualWidth - draggableEllipse.Width)
                {
                    Canvas.SetLeft(draggableEllipse, newLeft);
                    xSlider.Value = newLeft;
                }

                if (newTop >= 0 && newTop <= CanvasTT.ActualHeight - draggableEllipse.Height)
                {
                    Canvas.SetTop(draggableEllipse, newTop);
                    ySlider.Value = 1 - newTop;
                }

                Console.WriteLine($"{newLeft}, {newTop}");
            }
        }
    }
}
