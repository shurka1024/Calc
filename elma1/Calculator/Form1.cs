using Calc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Калькулятор
        /// </summary>
        private Calc.Calc Calc { get; set; }

        private IEnumerable<string> OperationNames { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            var operations = new List<IOperation>();

            GetOperations(ref operations);

            Calc = new Calc.Calc(operations);

            OperationNames = Calc.GetOperationNames();

            // Заполнить Комбобокс
            FillCombobox();
        }
        private void FillCombobox()
        {
            this.comboBox1.Items.AddRange(OperationNames.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lblresult.Text = Calculate();
        }

        private string Calculate()
        {
            var operations = new List<IOperation>();

            GetOperations(ref operations);

            var calc = new Calc.Calc(operations);
            
            var activeoper = comboBox1.Text.Trim();
            object[] parameters = { this.textBox1.Text, this.textBox2.Text };


            var result = calc.Execute(activeoper, parameters);

            return Convert.ToString(result);
        }

        private void GetOperations(ref List<IOperation> operations)
        {
            var files = Directory.GetFiles(Environment.CurrentDirectory, "*.dll");
            foreach (var file in files)
            {
                var types = Assembly.LoadFile(file).GetTypes(); // Получили сборку и ее типы
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces.Contains(typeof(IOperation)) && !type.IsInterface)
                    {
                        var oper = Activator.CreateInstance(type) as IOperation;
                        if (oper != null)
                        {
                            operations.Add(oper);
                        }
                    }
                }
            }
        }

    }
}
