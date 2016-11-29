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
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace PisanieReczne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string nazwaPliku = "";

        public static System.Windows.Media.Color col;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnCzysc_Click(object sender, RoutedEventArgs e)
        {
            reczne.Strokes.Clear();
        }
        public double FormWidth()
        {return this.Width;      }
        public double FormHeight()
        { return this.Height; }

        public void EskportDoJPEG(InkCanvas obszar)
        {

            double
                    x1 = obszar.Margin.Left,
                    x2 = obszar.Margin.Top,
                    x3 = obszar.Margin.Right,
                    x4 = obszar.Margin.Bottom;
            obszar.Margin = new Thickness(0, 0, 0, 0);

            Size size = new Size(FormWidth()+150, FormHeight()+150);
            //Size size = new Size(obszar.Width, obszar.Height);

            obszar.Measure(size);
            obszar.Arrange(new Rect(size));
            RenderTargetBitmap renderBitmap =

             new RenderTargetBitmap(
               (int)size.Width,
               (int)size.Height,
               96,
               96,
               PixelFormats.Default);
            renderBitmap.Render(obszar);
            otworzOknoDialogoZapisz();
            try
            {
                using (FileStream fs = File.Open(nazwaPliku, FileMode.Create))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(fs);
                }
                obszar.Margin = new Thickness(x1, x2, x3, x4);
            }
            catch (Exception e)
            {
            }
            
        }
            
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            EskportDoJPEG(reczne);
        }
        void otworzOknoDialogoZapisz()
        {
            //open save dialog box
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "nazwa_pliku"; // domyślna nazwa pliku
            dlg.DefaultExt = ".jpg"; // domyśle rozszerzenie pliku
            dlg.Filter = "Obrazek (.jpg)|*.jpg"; // pokauj tylko pliku *.jpg

           // show save dialog
            Nullable<bool> result = dlg.ShowDialog();

            
            if (result == true)
            {
                //saving document
                nazwaPliku = dlg.FileName;
            }
        }
       
         ////   koloDialog();
         //   //System.draw joo
         //   col.A = 25;//opacity
         //   col.R = 12;
         //   col.G = 12;
         //   col.B = 12;
         //   ChangeColor(reczne, col);
        
        //void ChangeColor(InkCanvas Canvasss, System.Windows.Media.Color color)

        ////void ChangeColor(InkCanvas inkCanvas, Color color)
        //{
        //    //foreach (var stroke in inkCanvas.Strokes)
        //    //{
        //    //stroke.DrawingAttributes.Color = Colors.Aqua;// col;
        //  //  Canvasss.DefaultDrawingAttributes.Color = Color.FromArgb(col.A, col.R, col.G, col.B);
        //    Canvasss.DefaultDrawingAttributes.Color = Colors.Red;
        //    //stroke.DrawingAttributes.Color = Color.FromArgb(col.A,col.R,col.G,col.B);
        //   //}
       // }
        private void cboBrushSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboBrushSize.Items.Count > 0 && ((ComboBoxItem)cboBrushSize.SelectedItem).Content != null)
            {
                /// Sets the brush size.
                reczne.DefaultDrawingAttributes.Width = Convert.ToDouble(((ComboBoxItem)cboBrushSize.SelectedItem).Content);
                reczne.DefaultDrawingAttributes.Height = Convert.ToDouble(((ComboBoxItem)cboBrushSize.SelectedItem).Content);
            }
        }
        private void InkColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (InkColor.Items.Count > 0 && ((ComboBoxItem)InkColor.SelectedItem).Content != null)
    {
        /// Sets the brush color based on the selected color.
        if (((ComboBoxItem)InkColor.SelectedItem).Content.ToString() == "Black")
            reczne.DefaultDrawingAttributes.Color = Colors.Black;
        else if (((ComboBoxItem)InkColor.SelectedItem).Content.ToString() == "Blue")
            reczne.DefaultDrawingAttributes.Color = Colors.Blue;
        else if (((ComboBoxItem)InkColor.SelectedItem).Content.ToString() == "Green")
            reczne.DefaultDrawingAttributes.Color = Colors.Green;
        else if (((ComboBoxItem)InkColor.SelectedItem).Content.ToString() == "Red")
            reczne.DefaultDrawingAttributes.Color = Colors.Red;
        else if (((ComboBoxItem)InkColor.SelectedItem).Content.ToString() == "Yellow")
            reczne.DefaultDrawingAttributes.Color = Colors.Yellow;
    }
        }
        //private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    reczne.Background = Brushes.DarkSlateBlue;
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)//gumka
        {
            reczne.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)//rysuj
        {
            reczne.EditingMode = InkCanvasEditingMode.Ink;
           // reczne.Cursor = Pen;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)//delete line
        {
            reczne.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }

        private void Draw_Checked(object sender, RoutedEventArgs e)
        {
           // if (radDrawingMode.IsChecked.Value == true)
            reczne.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void select_Checked(object sender, RoutedEventArgs e)
        {
            reczne.EditingMode = InkCanvasEditingMode.Select;
        }

        private void Zaznacz_Click(object sender, RoutedEventArgs e)
        {
            reczne.EditingMode = InkCanvasEditingMode.Select;
            //MessageBox.Show(obszar.Width.ToString() + obszar.Height);
            
        }

     





    }
}
