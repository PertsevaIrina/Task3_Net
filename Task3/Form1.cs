using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task3
{
    public partial class Form1 : Form
    {
        private Label l = new Label(); //текстовое поле
        private TextBox[] boxes = new TextBox[3]; //текстовое поле с возможностью редактирования
        private List<Tour> list = new List<Tour>(); //все
        private List<TourType> type_filter = new List<TourType>(); //все типы для выборки
        private List<Transport> transport_filter = new List<Transport>(); //транспорт для выборки

        public Form1()
        {
            Font f = new Font("Times new roman", 30, Font.Style);

            string[] labels = {"Type", "Transport", "Min meals/d", "Min days", "Max days"}; //подпись интерфейса первой строки
            for (int i = 0; i < labels.Length; i++)
            {
                Label l1 = new Label();
                l1.Font = f;
                l1.Location = new Point(10 + 250 * i, 10);
                l1.Size = new Size(250, 50);
                l1.Text = labels[i];
                Controls.Add(l1);
            }

            var types = Enum.GetValues(typeof(TourType)); //список всех типов
            for (int i = 0; i < types.Length; i++)
            {
                CheckBox cb_type = new CheckBox();
                cb_type.Font = f;
                cb_type.Location = new Point(10, 60 + 50 * i);
                cb_type.Size = new Size(250, 50);
                cb_type.Text = types.GetValue(i).ToString();
                cb_type.Checked = true; //ставим галочку
                type_filter.Add((TourType) types.GetValue(i)); 
                cb_type.CheckedChanged += cb_type_changed; //обработчик событий
                Controls.Add(cb_type);
            }

            var transports = Enum.GetValues(typeof(Transport));
            for (int i = 0; i < transports.Length; i++)
            {
                CheckBox cb_transport = new CheckBox();
                cb_transport.Font = f;
                cb_transport.Location = new Point(10 + 250, 60 + 50 * i);
                cb_transport.Size = new Size(250, 50);
                cb_transport.Text = transports.GetValue(i).ToString();
                cb_transport.Checked = true;
                transport_filter.Add((Transport) transports.GetValue(i));
                cb_transport.CheckedChanged += cb_transport_changed;
                Controls.Add(cb_transport);
            }

            for (int i = 0; i < boxes.Length; i++) //создание полей ввода
            {
                TextBox tb = new TextBox();
                tb.Font = f;
                tb.Location = new Point(10 + 250 * (i + 2), 60);
                tb.Size = new Size(200, 50);
                tb.MaxLength = 2; //ограничение по кол-ву знаков
                tb.KeyPress += tb_KeyPress;
                boxes[i] = tb;
                Controls.Add(tb);
            }

            Button b = new Button();
            b.Font = f;
            b.Location = new Point(10 + 250 * 4, 10 + Math.Max(types.Length, transports.Length) * 50);
            b.Size = new Size(200, 50);
            b.Text = "Find";
            b.Click += b_click;
            Controls.Add(b);


            string[] path = {Application.StartupPath, "..\\..\\tests\\input01.txt"};
            list = Program.GetToursFromFile(Path.Combine(path));

            l.Font = f;
            l.Location = new Point(10, 60 + Math.Max(types.Length, transports.Length) * 50);
            l.Size = new Size(10 + labels.Length * 250, 500);
            l.Text = Program.getToursToText(list);
            Controls.Add(l);
            WindowState = FormWindowState.Maximized;
            Focus();
            // InitializeComponent();
        }

        private void b_click(object sender, EventArgs e)
        {
            int[] nums = new int[boxes.Length];
            for (int i = 0; i < boxes.Length; i++)
            {
                if (boxes[i].Text != "")
                {
                    nums[i] = Int32.Parse(boxes[i].Text);
                }
                else
                {
                    nums[i] = -1;
                }
            }
            
            l.Text = Program.getToursToText(Program.getToursByFilters(list, type_filter, transport_filter, nums)); //создает новый список туров
        }

        private void tb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char num = e.KeyChar;
            if (!char.IsControl(num) && !char.IsDigit(num)) //проверка на клавишу удаления или числа
            {
                e.Handled = true;
            }
        }

        private void cb_type_changed(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            string s = checkBox.Text;
            TourType type = (TourType) Enum.Parse(typeof(TourType), s);
            if (checkBox.Checked) //проверка на наличие галки
            {
                type_filter.Add(type);
            }
            else
            {
                type_filter.Remove(type);
            }
        }

        private void cb_transport_changed(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox) sender;
            string s = checkBox.Text;
            Transport transport = (Transport) Enum.Parse(typeof(Transport), s);
            if (checkBox.Checked)
            {
                transport_filter.Add(transport);
            }
            else
            {
                transport_filter.Remove(transport);
            }
        }
    }
}