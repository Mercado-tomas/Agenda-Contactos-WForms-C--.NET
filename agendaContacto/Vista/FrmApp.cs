﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using agendaContacto;
using System.Diagnostics.Contracts;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace agendaContacto
{
    public partial class FrmApp : Form
    {
        private int currentContactoId;

        public FrmApp()
        {
            InitializeComponent();

            ConexionDb objAgenda = new ConexionDb();

            // Agregar opciones al ComboBox
            cmbCategoria.Items.Add("Familia");
            cmbCategoria.Items.Add("Amigos");
            cmbCategoria.Items.Add("Trabajo");
            // inicia vacio
            cmbCategoria.SelectedIndex = -1; 

            treeViewContactos.Nodes.Add("Familia");
            treeViewContactos.Nodes.Add("Trabajo");
            treeViewContactos.Nodes.Add("Amigos");

            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);


        }
        //Declaramos para poder exportar
        archivContact exportarContancto = new archivContact();

        private void btnExportar_Click(object sender, EventArgs e)
        {
            string categoria = cmbCategoria.Text;
            int telefono = int.Parse(txtTelefono.Text);
            //string telefono = txtTelefono.Text;
            
            exportarContancto.Grabar(txtNombre.Text, txtApellido.Text,
            telefono,txtCorreo.Text, categoria );
            MessageBox.Show("Datos listos para exportar");

            Limpiar();
        }


        //Guardamos datos para crear nuevos contactos
        private Contactos guardarDatos()
        {
            Contactos ContactoNuevo = new Contactos();

            ContactoNuevo.Nombre = txtNombre.Text;
            ContactoNuevo.Apellido = txtApellido.Text;
            ContactoNuevo.Telefono = int.Parse(txtTelefono.Text);
            ContactoNuevo.Correo = txtCorreo.Text;
         
            if (cmbCategoria.SelectedItem != null)
            {
                
                ContactoNuevo.Categoria = cmbCategoria.SelectedItem.ToString();
            }
            else
            {
                
                MessageBox.Show("Seleccionar una categoria.");
                return null; 
            }

            return ContactoNuevo;
 

        }
        
        
        //Verificamos si anda mediante el listado de la bd
        private void btnListar_Click(object sender, EventArgs e)
        {
            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);
        }
        private void NoSeRepita()
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Contactos nuevoContacto = guardarDatos();
            if (nuevoContacto != null) // Asegúrate de que no sea nulo
            {
                ConexionDb contactoNuevo = new ConexionDb();
                contactoNuevo.Agregar(nuevoContacto, treeViewContactos); // Pasa el TreeView
                
            }

            Limpiar();
            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ConexionDb editContacto = new ConexionDb();

            editContacto.Modificar(guardarDatos());

            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar eliminarForm = new Eliminar(); // Usa el nombre correcto
            eliminarForm.Show(); // Muestra el formulario
                
        }

        private void btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        //Actualizar 
        private void telefonoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConexionDb db = new ConexionDb();
            db.MostrarTree(treeViewContactos);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FrmBuscar buscarForm = new FrmBuscar(); // Usa el nombre correcto
            buscarForm.Show(); // Muestra el formulario
        }

        private void seleccionar(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Ver si el nodo tiene un objeto Contactos en el Tag (creado en mostartree)
            if (e.Node.Tag is Contactos contactoSeleccionado)
            {
                // Mostrar los datos del contacto en los TextBox
                txtNombre.Text = contactoSeleccionado.Nombre;
                txtApellido.Text = contactoSeleccionado.Apellido;
                txtTelefono.Text = contactoSeleccionado.Telefono.ToString();
                txtCorreo.Text = contactoSeleccionado.Correo;

                //Para msotar el cmb Categoria
                cmbCategoria.SelectedItem = contactoSeleccionado.Categoria;

                currentContactoId = contactoSeleccionado.Id;
            }

            Limpiar();
           
        }

        private void Limpiar()
        {
            //txtApellido.Clear();
            //txtCorreo.Clear();
            //txtNombre.Clear();
            //txtTelefono.Clear();
            //cmbCategoria.SelectedItem = 0;
        }

        private void frmAgenda_Load(object sender, EventArgs e)
        {

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Eliminar eliminarForm = new Eliminar(); // Usa el nombre correcto
            eliminarForm.Show(); // Muestra el formulario

        }

        private void buscarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmBuscar buscarForm = new FrmBuscar(); // Usa el nombre correcto
            buscarForm.Show(); // Muestra el formulario
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close(); 
        }

        private void treeViewContactos_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
        private void controlCajaDetexto()
        {
            if (txtApellido.Text != "" && txtCorreo.Text != " " && txtNombre.Text !="" && txtTelefono.Text != " ")
            {
                btnExportar.Enabled = true; 
            }
            else
            {
                btnExportar.Enabled = false;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
