using System;

using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CmpBE100;
using Primavera.Extensibility.BusinessEntities;
using Primavera.Extensibility.BusinessEntities.ExtensibilityService.EventArgs;
using Primavera.Extensibility.Purchases.Editors;
using StdBE100;
using static StdBE100.StdBETipos;


namespace PDEX.Extensibility
{ 
    public class UiEditorCompras : EditorCompras
    {
        //private GlobalFunctions globals = null;

        //public UiEditorCompras()
        //{
        //    globals = new GlobalFunctions(BSO, PSO);
        //}

        public override void AntesDeGravar(ref bool Cancel, ExtensibilityEventArgs e)
        {
            string avisos = string.Empty;

            // Valida se todas as linhas t�m os mesmos valores na FonteCBL, ClasseEconomica e Organica para cabimenta��o
            if (Globals.globalFunctions.DocSujeitoCabimentacao(DocumentoCompra.Tipodoc))
            {
                if (!Globals.globalFunctions.DocElegivelCabimentacao(DocumentoCompra, ref avisos))
                {
                    string msg = string.Format("O documento {0} est� sujeito a cabimenta��o mas existem condi��es que o impedem!", this.DocumentoCompra.Tipodoc) + Environment.NewLine + "Continuar com a grava��o do documento?";
                    Cancel = (PSO.Dialogos.MostraMensagem(StdPlatBS100.StdBSTipos.TipoMsg.PRI_SimNao, msg, StdPlatBS100.StdBSTipos.IconId.PRI_Questiona, avisos, 1, !string.IsNullOrWhiteSpace(avisos)) == StdPlatBS100.StdBSTipos.ResultMsg.PRI_Nao);
                }
            }
        }
    }
}
