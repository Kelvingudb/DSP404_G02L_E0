using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace guia02_ejercicio1
{
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }
        
        /*
         Este metodo es asincrono, para poder esperar la tarea que rellena el listbox
        mientras esta tarea se ejecuta lo que validamos es que el usuario no pueda reiniciar
        la tarea clickeando el boton, solo se podra hasta que la tarea termine.
         */
        private async void btnStart_Click(object sender, EventArgs e)
        {

            //Haremos nuestra validacion utilizando un metodo que nos verificara si el campo esta vacio
            if (IsEmptyTextBox())
            {
                MessageBox.Show("No se ha ingresado ningun numero", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {
                
                try
                {
                    btnStart.Enabled = false;
                    int limit_number = int.Parse(txtNumber.Text);
                    if (listFibonacciNumbers.Items.Count > 0)
                    {
                        listFibonacciNumbers.Items.Clear();
                    }

                   await FibonacciSucession(limit_number);
                }
                catch(FormatException format_exception)
                {
                    MessageBox.Show(format_exception.Message, "ERROR",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                    
                }
                finally
                {
                    ClearTextBox(txtNumber);
                    btnStart.Enabled = true; 
                }
            }
        }

        /*La clausure guard nos indica que cuando el campo este vacio
          el enviara un valor true para indicar que si lo esta.*/
        private bool IsEmptyTextBox()
        {
            
            if (!(txtNumber.Text.Length > 0)) //clausure guard.
            {

                return true;
            }

            return false;
        }
        private  void ClearTextBox(TextBox textBox)
        {
            textBox.Clear();
        }

        /*
         La siguiente tarea ejecuta la sucesion de fibonacci, en base al limite 
         que nosotros le indiquemos. Por temas visuales agregaremos unas lineas de
        codigo para que se visualice la carga de los numeros, mas lento de lo normal.
         
        la hemos creado como asincrona solo para simular la carga de los datos mas lenta de 
        lo habitual.
         */
        private async Task FibonacciSucession(int limit_number)
        {
            int number_1 = 0, number_2= 1;
            int sum = 0;

            //Bucle que sumara los numeros e ira mostrando los numeros de la sucesion
            while (number_1 <= limit_number)
            { 
                sum = number_1 + number_2;
               
                listFibonacciNumbers.Items.Add(number_1);
               
                number_1 = number_2;
                number_2 = sum;
                await Task.Delay(100);
            }
        }
    }
}
