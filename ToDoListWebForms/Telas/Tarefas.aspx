<%@ Page Title="Gerenciar Tarefas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tarefas.aspx.cs" Inherits="ToDoListWebForms.Tarefas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
        <div class="row mb-3">
            <h2>Lista de Tarefas</h2>
            <div class="col-md-2">
                <asp:DropDownList ID="filterStatus" runat="server" CssClass="form-select">
                    <asp:ListItem Value="">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Concluídas</asp:ListItem>
                    <asp:ListItem Value="0">Pendentes</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary w-100" OnClick="btnBuscarTarefa_Click" Text="Buscar" />
            </div>
            <div class="col-md-2">
                <asp:Button ID="btnAdicionar" runat="server" CssClass="btn btn-success w-100" OnClientClick="return AbrirModal();" Text="Nova Tarefa" />
            </div>
        </div>


        <div>
            <asp:GridView ID="gridTarefas" runat="server" AutoGenerateColumns="false" 
                CssClass="table table-bordered" DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="false"/>
                    <asp:BoundField DataField="Titulo" HeaderText="Título" />
                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
                    <asp:BoundField DataField="DataCriacao" HeaderText="Data Criação" />
                    <asp:BoundField DataField="DataConclusao" HeaderText="Data Conclusão" />
                    
                    <asp:TemplateField HeaderText="Concluído">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkConcluido" runat="server"
                                Checked='<%# Convert.ToBoolean(Eval("Concluido")) %>'
                                AutoPostBack="true"
                                OnCheckedChanged="chkConcluido_CheckedChanged"/>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>

            </asp:GridView>
            <asp:Label ID="lblTarefas" runat="server" Text="Nenhuma tarefa encontrada" Visible="false" CssClass="text-muted"></asp:Label>
        </div>   

        </ContentTemplate>
    </asp:UpdatePanel> 

    <!--- Modal p/ adicionar ---->
    <div class="modal fade" id="modalNovaTarefa" tabindex="-1" role="dialog" aria-labelledby="modalNovaTarefaLabel1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title" id="modalNovaTarefaLabel">Nova Tarefa</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

                <div class="modal-body">
                    <asp:Label ID="lblTitulo" runat="server" Text="Título"></asp:Label>
                    <asp:TextBox ID="txtTitulo" runat="server" CssClass="form-control"></asp:TextBox>

                    <asp:Label ID="lblDescricao" runat="server" Text="Descrição" CssClass="mt-2 d-block"></asp:Label>
                    <asp:TextBox ID="txtDescricao" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btnSalvarTarefa" runat="server" Text="Salvar" CssClass="btn btn-success" OnClick="btnAdicionarTarefa_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <!--- Fim Modal ---->
    <script type="text/javascript">
        function AbrirModal() {
            $('#modalNovaTarefa').modal('show');

            return false; //evitar postback
        }
    </script>
</asp:Content>
