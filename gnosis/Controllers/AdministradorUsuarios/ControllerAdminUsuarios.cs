﻿using gnosis.Models.DAO;
using gnosis.Views.Administrador_de_usuarios;
using System;
using System.Data;
using System.Windows.Forms;

namespace gnosis.Controllers.AdministradorUsuarios
{
    class ControllerAdminUsuarios
    {
        ViewAdministradorUsuarios ObjAdminUser;
        public ControllerAdminUsuarios(ViewAdministradorUsuarios Vista)
        {
            ObjAdminUser = Vista;
            ObjAdminUser.Load += new EventHandler(LoadData);
            //Evento click de botón
            ObjAdminUser.btnNuevo.Click += new EventHandler(NewUser);
            ObjAdminUser.cmsActualizar.Click += new EventHandler(UpdateUser);
            ObjAdminUser.cmsEliminar.Click += new EventHandler(DeleteUser);
            ObjAdminUser.cmsFicha.Click += new EventHandler(ViewData);
            ObjAdminUser.txtSearch.KeyPress += new KeyPressEventHandler(Search);
        }

        public void Search(object sender, KeyPressEventArgs e)
        {
            //Objeto de la clase DAOAdminUsuarios
            DAOAdminUsers objAdmin = new DAOAdminUsers();
            //Declarando nuevo DataSet para que obtenga los datos del metodo ObtenerPersonas
            DataSet ds = objAdmin.BuscarPersonas(ObjAdminUser.txtSearch.Text);
            //Llenar DataGridView
            ObjAdminUser.dgvPersonas.DataSource = ds.Tables["viewPerson"];
        }

        public void LoadData(object sender, EventArgs e)
        {
            RefrescarData();
        }

        //DataGridView
        public void RefrescarData()
        {
            //Objeto de la clase DAOAdminUsuarios
            DAOAdminUsers objAdmin = new DAOAdminUsers();
            //Declarando nuevo DataSet para que obtenga los datos del metodo ObtenerPersonas
            DataSet ds = objAdmin.ObtenerPersonas();
            //Llenar DataGridView
            ObjAdminUser.dgvPersonas.DataSource = ds.Tables["viewPerson"];
        }

        #region Código para generar columnas de editar y eliminar
        //public void GenerarColumnaEliminarDataGrid()
        //{
        //    DataGridViewButtonColumn btnClmDel = new DataGridViewButtonColumn();
        //    btnClmDel.Name = "Eliminar";
        //    ObjAdminUser.dgvPersonas.Columns.Add(btnClmDel);
        //}
        //public void GenerarColumnaEditarDataGrid()
        //{
        //    DataGridViewButtonColumn btnClmEdit = new DataGridViewButtonColumn();
        //    btnClmEdit.Name = "Editar";
        //    ObjAdminUser.dgvPersonas.Columns.Add(btnClmEdit);
        //}
        //public void FormatoColumnaGrid(Object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.ColumnIndex >= 0 && ObjAdminUser.dgvPersonas.Columns[e.ColumnIndex].Name == "Eliminar" && e.RowIndex >= 0)
        //    {
        //        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
        //        DataGridViewButtonCell celboton = ObjAdminUser.dgvPersonas.Rows[e.RowIndex].Cells["Eliminar"] as DataGridViewButtonCell;
        //        Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"../../../Resources/Trash.ico");
        //        e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
        //        ObjAdminUser.dgvPersonas.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
        //        ObjAdminUser.dgvPersonas.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;
        //        e.Handled = true;
        //    }
        //    else if (e.ColumnIndex >= 0 && ObjAdminUser.dgvPersonas.Columns[e.ColumnIndex].Name == "Editar" && e.RowIndex >= 0)
        //    {
        //        e.Paint(e.CellBounds, DataGridViewPaintParts.All);
        //        DataGridViewButtonCell celboton = ObjAdminUser.dgvPersonas.Rows[e.RowIndex].Cells["Editar"] as DataGridViewButtonCell;
        //        Icon icoAtomico = new Icon(Environment.CurrentDirectory + @"../../../Resources/Refresh.ico");
        //        e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);
        //        ObjAdminUser.dgvPersonas.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
        //        ObjAdminUser.dgvPersonas.Columns[e.ColumnIndex].Width = icoAtomico.Width + 8;
        //        e.Handled = true;
        //    }
        //}
        #endregion

        private void NewUser(object sender, EventArgs e)
        {
            /*Se invoca al formulario ViewAddUser y se le envía un numero, este numero servirá para indicarle que tipo de acción se quiere realizar, donde 1 significa Inserción y 2 significa Actualización*/
            ViewAddUser openForm = new ViewAddUser(1);
            //Se muestra el formulario
            openForm.ShowDialog();
            //Cuando el formulario ha sido cerrado se procede a refrescar el DataGrid para que se puedan observar los nuevo datos ingresados o actualizados.
            RefrescarData();
        }

        private void UpdateUser(object sender, EventArgs e)
        {
            //Se captura el numero de la fila a la cual se le dió click, sabiendo que la primer fila tiene como valor cero.
            int pos = ObjAdminUser.dgvPersonas.CurrentRow.Index;
            /*Se invoca al formulario llamado ViewAddUser y se crea un objeto de el, posteriormente se envían los datos del datagrid al constructor del formulario según el orden establecido (se sugiere ver el código del formulario para observar ambos constructores).
             * El numero dos indicado en la linea posterior significa que la acción que se desea realizar es una actualízación.*/
            ViewAddUser openForm = new ViewAddUser(2,
                int.Parse(ObjAdminUser.dgvPersonas[0, pos].Value.ToString()),
                ObjAdminUser.dgvPersonas[1, pos].Value.ToString(),
                ObjAdminUser.dgvPersonas[2, pos].Value.ToString(),
                DateTime.Parse(ObjAdminUser.dgvPersonas[3, pos].Value.ToString()),
                ObjAdminUser.dgvPersonas[4, pos].Value.ToString(),
                ObjAdminUser.dgvPersonas[5, pos].Value.ToString(),
                ObjAdminUser.dgvPersonas[6, pos].Value.ToString(),
                ObjAdminUser.dgvPersonas[7, pos].Value.ToString(),
                ObjAdminUser.dgvPersonas[8, pos].Value.ToString(),
                ObjAdminUser.dgvPersonas[9, pos].Value.ToString());
            //Una vez los datos han sido enviados al constructor de la vista se procede a mostrar el formulario (se sugiere ver el código del constructor que esta en la vista)
            openForm.ShowDialog();
            //Una vez el formulario se haya cerrado se procederá a refrescar el dataGrid para mostrar los nuevos datos.
            RefrescarData();
        }

        private void ViewData(object sender, EventArgs e)
        {
            int pos = ObjAdminUser.dgvPersonas.CurrentRow.Index;
            int id;
            string firstName, lastName, dni, address, email, phone, username, role;
            DateTime birthday;

            id = int.Parse(ObjAdminUser.dgvPersonas[0, pos].Value.ToString());
            firstName = ObjAdminUser.dgvPersonas[1, pos].Value.ToString();
            lastName = ObjAdminUser.dgvPersonas[2, pos].Value.ToString();
            birthday = DateTime.Parse(ObjAdminUser.dgvPersonas[3, pos].Value.ToString());
            dni = ObjAdminUser.dgvPersonas[4, pos].Value.ToString();
            address = ObjAdminUser.dgvPersonas[5, pos].Value.ToString();
            email = ObjAdminUser.dgvPersonas[6, pos].Value.ToString();
            phone = ObjAdminUser.dgvPersonas[7, pos].Value.ToString();
            username = ObjAdminUser.dgvPersonas[8, pos].Value.ToString();
            role = ObjAdminUser.dgvPersonas[9, pos].Value.ToString();
            //nameImage = ObjAdminUser.dgvPersonas[10, pos].Value.ToString();

            ViewAddUser openForm = new ViewAddUser(3, id, firstName, lastName, birthday, dni, address, email, phone, username, role);
            openForm.ShowDialog();
            RefrescarData();
        }
        private void DeleteUser(object sender, EventArgs e)
        {
            //
            int pos = ObjAdminUser.dgvPersonas.CurrentRow.Index;
            if (MessageBox.Show($"¿Esta seguro que desea elimar a:\n {ObjAdminUser.dgvPersonas[1, pos].Value.ToString()} {ObjAdminUser.dgvPersonas[2, pos].Value.ToString()}.\nConsidere que dicha acción no se podrá revertir.","Confirmar acción",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DAOAdminUsers daoDel = new DAOAdminUsers();
                daoDel.PersonId = int.Parse(ObjAdminUser.dgvPersonas[0, pos].Value.ToString());
                int valorRetornado = daoDel.EliminarUsuario();
                if (valorRetornado == 1)
                {
                    MessageBox.Show("Registro eliminado","Acción completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefrescarData();
                }
                else
                {
                    MessageBox.Show("Registro no pudo ser eliminado, verifique que el registro no tenga datos asociados.", "Acción interrumpida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}