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
                // Входные данные
                double Z = double.Parse(input_z.Text);    // Импеданс
                double R = double.Parse(input_r.Text);    // Сопротивление R
                double R1 = double.Parse(input_r1.Text);  // Сопротивление R1 (не используется в расчете L)

                // Проверка допустимости значений
                if (Z <= 0 || R <= 0)
                {
                    output.Text = "Ошибка: Z и R должны быть положительными числами.";
                    return;
                }

                // Расчет коэффициента A
                double A = R / Z; // Условие R/Z
                if (A <= 0 || A >= 2)
                {
                    output.Text = "Ошибка: Некорректное значение коэффициента A.";
                    return;
                }

                // Расчет потерь L
                double L = -20 * Math.Log10((A - 1) / (A + 1));

                // Вывод результата
                output.Text = $"Вычисленное значение L: {L:F3} дБ";
            }
            catch (Exception ex)
            {
                output.Text = $"Ошибка: {ex.Message}";
            }
        }
    }
}