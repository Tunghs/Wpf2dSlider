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

        private int baseGridWidth = 300;
        private int baseGridHeight = 300;

        public MainWindow()
        {
            InitializeComponent();
            CanvasTT.Width = xRange;
            CanvasTT.Height = yRange;

            xSliderCanvas.Width = xRange;
            xSliderCanvas.Height = 1;

            ySliderCanvas.Width = 1;
            ySliderCanvas.Height = yRange;
            
            SetBaseValues();

            InitializeDraggableEllipse();
            CreateSlider();
            CreatySlider();
        }

        private void SetBaseValues()
        {
            BaseGridWidthTbx.Text = baseGridWidth.ToString();
            BaseGridHeightTbx.Text = baseGridHeight.ToString();

            BaseGrid.Width = baseGridWidth;
            BaseGrid.Height = baseGridHeight;
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
            xSliderValueTbk = new TextBlock();
            xSliderValueTbk.Text = text;
            xSliderValueTbk.Foreground = foreground;
            xSliderValueTbk.FontSize = fontSize;
            xSliderValueTbk.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            xSliderValueTbk.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            return xSliderValueTbk;
        }

        private void CreateSlider()
        {
            xSliderPanel = new Grid();

            Border border = CreateBorder(10, 0.5, Brushes.Gainsboro);
            xSliderBorder = CreateBorder(0.25, 0.5, Brushes.SeaGreen);

            Canvas.SetTop(xSliderBorder, 0.25);
            Canvas.SetTop(border, 0.25);

            Ellipse ellipse = CreateEllipse(1, 1, Brushes.MediumSeaGreen);
            xSliderValueTbk = CreateValueTextBlock("0", 0.4, Brushes.White);

            xSliderPanel.Children.Add(ellipse);
            xSliderPanel.Children.Add(xSliderValueTbk);

            xSliderCanvas.Children.Add(border);
            xSliderCanvas.Children.Add(xSliderBorder);
            xSliderCanvas.Children.Add(xSliderPanel);
        }

        private void CreatySlider()
        {
            ySliderPanel = new Grid();

            Border border = CreateBorder(0.5, 10, Brushes.Gainsboro);
            ySliderBorder = CreateBorder(0.5, 0.25, Brushes.SeaGreen);

            Canvas.SetLeft(ySliderBorder, 0.25);
            Canvas.SetLeft(border, 0.25);

            Ellipse ellipse = CreateEllipse(1, 1, Brushes.MediumSeaGreen);
            ySliderValueTbk = CreateValueTextBlock("0", 0.4, Brushes.White);

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
                Fill = Brushes.SeaGreen
            };

            // 마우스 이벤트 처리기 등록
            draggableEllipse.MouseLeftButtonDown += DraggableEllipse_MouseLeftButtonDown;
            draggableEllipse.MouseLeftButtonUp += DraggableEllipse_MouseLeftButtonUp;
            draggableEllipse.MouseMove += DraggableEllipse_MouseMove;
            draggableEllipse.MouseRightButtonDown += DraggableEllipse_MouseRightButtonDown;
            draggableEllipse.MouseEnter += DraggableEllipse_MouseEnter;
            draggableEllipse.MouseLeave += DraggableEllipse_MouseLeave;

            // Canvas에 동그라미 추가
            CanvasTT.Children.Add(draggableEllipse);
        }

        private void DraggableEllipse_MouseLeave(object sender, MouseEventArgs e)
        {
            draggableEllipse.Fill = Brushes.SeaGreen;
        }

        private void DraggableEllipse_MouseEnter(object sender, MouseEventArgs e)
        {
            draggableEllipse.Fill = Brushes.MediumSeaGreen;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(BaseGridWidthTbx.Text, out int baseGridWidth);
            int.TryParse(BaseGridHeightTbx.Text, out int baseGridHeight);

            BaseGrid.Width = baseGridWidth;
            BaseGrid.Height = baseGridHeight;
        }
    }
}
