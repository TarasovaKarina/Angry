using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Petzold.ImageTheButton
{
    public class ImageTheButton : Window
    {
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ImageTheButton());
        }
        public ImageTheButton()
        {
            Title = "Image the Button"; 
            Uri uri = new Uri("pack://application:,,,/AngryBirds.jpg"); //создается объект Uri для указания пути к изображению "AngryBirds.jpg", которое будет отображаться на кнопке
            BitmapImage bitmap = new BitmapImage(uri); //создается объект BitmapImage для загрузки изображения из указанного Uri
            Image img = new Image(); //создается объект Image, который является элементом управления WPF для отображения изображений
            img.Source = bitmap; // устанавливается источник изображения для элемента Image
            img.Stretch = Stretch.None; // устанавливается режим растягивания изображения (чтобы изображение не растягивалось)
            Button btn = new Button(); // создаётся кнопка
            btn.Content = img; // содержимое кнопки устанавливается как изображение img
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.VerticalAlignment = VerticalAlignment.Center;
            Content = btn; // содержимое окна устанавливается как кнопка
        }
    }
}
