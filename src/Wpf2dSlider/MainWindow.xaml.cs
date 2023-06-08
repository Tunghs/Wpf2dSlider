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

            // 이벤트 추가
            ellipse.MouseLeftButtonDown += Ellipse_MouseLeftButtonDown;
            ellipse.MouseLeftButtonUp += Ellipse_MouseLeftButtonUp;
            ellipse.MouseMove += Ellipse_MouseMove;

            return ellipse;
        }

        private void Ellipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var ellipse = sender as Ellipse;
                var canvas = (Canvas)((Grid)ellipse.Parent).Parent;
                Point currentPosition = e.GetPosition(canvas);

                double leftPos = (int)(currentPosition.X - offset.X);
                double topPos = (int)(currentPosition.Y - offset.Y);

                if (leftPos + topPos == 0)
                {
                    UpdatePosition(0, 0);
                }
                else
                {
                    if (leftPos > topPos && leftPos <= canvas.ActualWidth - ellipse.Width)
                    {
                        UpdatePosition(leftPos, -1);
                    }
                }
                if (leftPos >= 0 && leftPos <= canvas.ActualWidth - ellipse.Width)
                {
                    UpdatePosition(leftPos, -1);

                    Console.WriteLine($"x 슬라이더 {leftPos}");
                }

                if (topPos >= 0 && topPos <= canvas.ActualHeight - ellipse.Height)
                {
                    UpdatePosition(-1, topPos);

                    Console.WriteLine($"y 슬라이더 {topPos}");
                }
            }
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;

            var ellipse = sender as Ellipse;
            ellipse.ReleaseMouseCapture();
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;

            var ellipse = sender as Ellipse;
            ellipse.CaptureMouse();
            offset = e.GetPosition(ellipse);
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
            double leftPos = CanvasTT.ActualWidth / 2 - draggableEllipse.Width / 2;
            double topPos = CanvasTT.ActualHeight / 2 - draggableEllipse.Height / 2;

            UpdatePosition(leftPos, -1);
            UpdatePosition(-1, topPos);
        }

        private void DraggableEllipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // 마우스 드래그 시 동그라미 위치 업데이트
                Point currentPosition = e.GetPosition(CanvasTT);

                double leftPos = (int)(currentPosition.X - offset.X);
                double topPos = (int)(currentPosition.Y - offset.Y);

                if (leftPos >= 0 && leftPos <= CanvasTT.ActualWidth - draggableEllipse.Width)
                {
                    UpdatePosition(leftPos, -1);
                }

                if (topPos >= 0 && topPos <= CanvasTT.ActualHeight - draggableEllipse.Height)
                {
                    UpdatePosition(-1, topPos);
                }
            }
        }

        private void UpdatePosition(double leftPos, double topPos)
        {
            if (leftPos >= 0)
            {
                Canvas.SetLeft(draggableEllipse, leftPos);
                Canvas.SetLeft(xSliderPanel, leftPos);
                xSliderBorder.Width = leftPos + 0.25;
                xSliderValueTbk.Text = leftPos.ToString();
            }

            if (topPos >= 0)
            {
                Canvas.SetTop(draggableEllipse, topPos);
                Canvas.SetTop(ySliderPanel, topPos);
                ySliderBorder.Height = topPos + 0.25;
                ySliderValueTbk.Text = topPos.ToString();
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
