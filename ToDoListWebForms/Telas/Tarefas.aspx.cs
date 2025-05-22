using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToDoListWebForms.Helpers;

namespace ToDoListWebForms
{
    public partial class Tarefas : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuscarTarefas();
            }
        }
        protected void btnBuscarTarefa_Click(object sender, EventArgs e)
        {
            BuscarTarefas();
        }

        private void BuscarTarefas()
        {
            using (var db = new DbHelper())
            {
                var parametros = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(filterStatus.SelectedValue))
                {
                    parametros.Add("@Filtro", filterStatus.SelectedValue);
                }

                DataTable tarefas = db.ExecutarProcedureDT("sp_Tarefas_Sel", parametros);
                gridTarefas.DataSource = tarefas;
                gridTarefas.DataBind();

                lblTarefas.Visible = (tarefas == null || tarefas.Rows.Count == 0);
            }
        }

        protected void chkConcluido_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.NamingContainer;
            int tarefaId = Convert.ToInt32(gridTarefas.DataKeys[row.RowIndex].Value);
            bool novoStatus = chk.Checked;

            AtualizarTarefas(tarefaId, novoStatus, 1);

            BuscarTarefas();
        }

        protected void AtualizarTarefas(int id, bool concluido, int tipoUpdate)
        {
            if (tipoUpdate == 1)
            {
                using (var db = new DbHelper())
                {
                    var parametros = new Dictionary<string, object>
                    {
                        { "@Id", id },
                        { "@Concluido", concluido },
                        { "@Tipo", tipoUpdate }

                    };

                    db.ExecutarProcedure("sp_Tarefas_Upd", parametros);
                }
            }
            //if(tipoUpdate != 1)
            //{
            //    using (var db = new DbHelper())
            //    {
            //        var parametros = new Dictionary<string, object>
            //    {
            //        { "@Id", id },
            //        { "@Tipo", tipoUpdate }
            //    };
            //        db.ExecutarProcedure("sp_Atualizar_Upd", parametros);
            //    }
            //}

        }

        protected void btnAdicionarTarefa_Click(object sender, EventArgs e)
        {
            //AdicionarTarefa();

            ScriptManager.RegisterStartupScript(this, GetType(), "fecharModal", "$('#modalNovaTarefa').modal('hide');", true);
        }

        private void AdicionarTarefa()
        {
            using (var db = new DbHelper())
            {
                var parametros = new Dictionary<string, object>();

                if (!string.IsNullOrEmpty(filterStatus.SelectedValue))
                {
                    parametros.Add("@Status", filterStatus.SelectedValue);
                }

                DataTable tarefas = db.ExecutarProcedureDT("sp_Tarefas_Sel", parametros);
                gridTarefas.DataSource = tarefas;
                gridTarefas.DataBind();

                lblTarefas.Visible = (tarefas == null || tarefas.Rows.Count == 0);
            }
        }
    }
}