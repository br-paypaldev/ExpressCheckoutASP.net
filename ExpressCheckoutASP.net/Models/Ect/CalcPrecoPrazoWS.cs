using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace ExpressCheckoutASP.net.Models.Ect
{
    [WebServiceBinding(Name = "CalcPrecoPrazoWSSoap", Namespace = "http://tempuri.org/")]

    public partial class CalcPrecoPrazoWS : SoapHttpClientProtocol
    {
     public CalcPrecoPrazoWS()
        {
            this.Url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx";
        }
        [SoapDocumentMethodAttribute("http://tempuri.org/CalcPrecoPrazo",
        RequestNamespace = "http://tempuri.org/",
        ResponseNamespace = "http://tempuri.org/",
        ParameterStyle = SoapParameterStyle.Wrapped,
        Use = SoapBindingUse.Literal
        )]
        public cResultado CalcPrecoPrazo(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento)
        {
            object[] results = this.Invoke("CalcPrecoPrazo", new object[] {
            nCdEmpresa,
            sDsSenha,
            nCdServico,
            sCepOrigem,
            sCepDestino,
            nVlPeso,
            nCdFormato,
            nVlComprimento,
            nVlAltura,
            nVlLargura,
            nVlDiametro,
            sCdMaoPropria,
            nVlValorDeclarado,
            sCdAvisoRecebimento
            });

            return ((cResultado)(results[0]));
        }
    }
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class cResultado
    {
        private cServico[] servicosField;
        [System.Xml.Serialization.XmlArrayItem(
        IsNullable = false
        )]
        public cServico[] Servicos
        {
            get { return this.servicosField; }
            set { this.servicosField = value; }
        }
    }
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://tempuri.org/")]
    public partial class cServico
    {
        private int codigoField;
        private string valorField;
        private int prazoEntregaField;
        private string valorMaoPropriaField;
        private string valorAvisoRecebimentoField;
        private string valorValorDeclaradoField;
        private string entregaDomiciliarField;
        private string entregaSabadoField;
        private string erroField;
        private string msgErroField;

        public int Codigo
        {
            get { return this.codigoField; }
            set { this.codigoField = value; }
        }

        public string Valor
        {
            get { return this.valorField; }
            set { this.valorField = value; }
        }

        public int PrazoEntrega
        {
            get { return this.prazoEntregaField; }
            set { this.prazoEntregaField = value; }
        }

        public string ValorMaoPropria
        {
            get { return this.valorMaoPropriaField; }
            set { this.valorMaoPropriaField = value; }
        }

        public string ValorAvisoRecebimento
        {
            get { return this.valorAvisoRecebimentoField; }
            set { this.valorAvisoRecebimentoField = value; }
        }

        public string ValorValorDeclarado
        {
            get { return this.valorValorDeclaradoField; }
            set { this.valorValorDeclaradoField = value; }
        }

        public string EntregaDomiciliar
        {
            get { return this.entregaDomiciliarField; }
            set { this.entregaDomiciliarField = value; }
        }

        public string EntregaSabado
        {
            get { return this.entregaSabadoField; }
            set { this.entregaSabadoField = value; }
        }

        public string Erro
        {
            get { return this.erroField; }
            set { this.erroField = value; }
        }

        public string MsgErro
        {
            get { return this.msgErroField; }
            set { this.msgErroField = value; }
        }
    }
}