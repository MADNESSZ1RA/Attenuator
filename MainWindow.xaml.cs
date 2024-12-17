using System;
using System.Windows;

namespace Attenuator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработчик кнопки "Рассчитать"
        public void Test(object sender, RoutedEventArgs e)
        {
            try
            {
                // Считывание и преобразование входных данных
                double Z = double.Parse(input_z.Text);
                double R = double.Parse(input_r.Text);
                double R1 = double.Parse(input_r1.Text);

                double ratio = R / Z;
                double A = (R - Z) / (R + Z);
                double L = -20 * Math.Log10(Math.Abs(A));


                // Вывод результатов
                output.Text = $"Вычисленное значение L: {L:F3}";
            }
            catch (FormatException)
            {
                output.Text = "Ошибка: Введите корректные числа!";
            }
            catch (Exception ex)
            {
                output.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}
