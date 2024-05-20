using System;
using System.Windows;
using System.Windows.Controls; //тут находятся классы для  классических элементов управления
using System.Windows.Input;
using System.Windows.Media;
namespace Petzold.ClickTheButton
{
    public class ClickTheButton : Window
    {
        TextBox[] txtbox = new TextBox[5];
        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            app.Run(new ClickTheButton());
        }
        public ClickTheButton()
        {
            Title = "Angry Birds"; //название окна
            Button btn = new Button(); //этим классом представлена кнопка со свойством Content и событием Click
            btn.Content = "Начать игру!";  //свойству Content объекта Button задается текстовая строка
            btn.Click += ButtonOnClick;
            Content = btn; //сам объект Button задаётся свойству Content объекта Window
        }
        void ButtonOnClick(object sender, RoutedEventArgs args)
        {
            EnterTheGrid();
        }
        void EnterTheGrid()
        {
            Title = "Angry Birds";
            MinWidth = 350;
            SizeToContent = SizeToContent.WidthAndHeight;
            // Создание объекта StackPanel для содержимого окна
            StackPanel stack = new StackPanel();
            Content = stack;
            // Создание объекта Grid и его добавление в StackPanel.
            Grid grid1 = new Grid();
            grid1.Margin = new Thickness(5);
            stack.Children.Add(grid1);
            // Создание определений строк
            for (int i = 0; i < 5; i++)
            {
                RowDefinition rowdef = new RowDefinition();
                rowdef.Height = GridLength.Auto;
                grid1.RowDefinitions.Add(rowdef);
            }
            // Создание определений столбцов
            ColumnDefinition coldef = new ColumnDefinition();
            coldef.Width = GridLength.Auto;
            grid1.ColumnDefinitions.Add(coldef);
            coldef = new ColumnDefinition();
            coldef.Width = new GridLength(100, GridUnitType.Star);
            grid1.ColumnDefinitions.Add(coldef);
            // Создание надписей и текстовых полей
            string[] strLabels = { "Скорость:",  "Угол наклона:",
                "Высота:",
                "Обцисса препядствия:",
                "Ордината препядствия:" };
            for (int i = 0; i < strLabels.Length; i++)
            {
                Label lbl = new Label();
                lbl.Content = strLabels[i];
                //Выравнивание текстовых полей по центру
                lbl.VerticalContentAlignment = VerticalAlignment.Center;
                grid1.Children.Add(lbl);
                //Количество столбцов и строк, занимаемых элментом, задаётся статистическими методами SetRow и SetColumn  
                Grid.SetRow(lbl, i);
                Grid.SetColumn(lbl, 0);
                txtbox[i] = new TextBox();
                txtbox[i].Margin = new Thickness(5);
                grid1.Children.Add(txtbox[i]);
                Grid.SetRow(txtbox[i], i);
                Grid.SetColumn(txtbox[i], 1);
            }
            // Создание второго объекта Grid и добавление в  StackPanel.
            Grid grid2 = new Grid();
            grid2.Margin = new Thickness(10);
            stack.Children.Add(grid2);
            // Для одной строки создавать определение не обязательно     
            // В определениях строк по умолчанию используется режим "star"
            grid2.ColumnDefinitions.Add(new ColumnDefinition());
            grid2.ColumnDefinitions.Add(new ColumnDefinition());
            // Создание кнопок
            Button btn = new Button();
            btn.Content = "Submit";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsDefault = true;
            btn.Click += delegate { new Movement(txtbox[0].Text) };//дописать функцию!
            grid2.Children.Add(btn);  // Row &  column are 0.
            btn = new Button();

            //MessageBox.Show(txtbox[0].Text);
            /*btn.Content = "Cancel";
            btn.HorizontalAlignment = HorizontalAlignment.Center;
            btn.IsCancel = true;
            btn.Click += delegate { Close(); };
            grid2.Children.Add(btn);
            Grid.SetColumn(btn, 1);  // Row is 0.          
                                     // Передача фокуса первому текстовому полю.*/
            (stack.Children[0] as Panel).Children[1].Focus();
        }
    }
}