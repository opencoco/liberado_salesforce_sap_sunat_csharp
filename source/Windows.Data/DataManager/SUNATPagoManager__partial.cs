using ACME.Data.Contracts;
using ACME.Data.Entity;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Net;
using Nancy.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ACME.Data.DataManager
{
    public partial class SUNATPagoManager : DbFactoryBase, ISUNATPagoManager
    {
        #region restapi digiflow

        internal class DigiflowApi
        {
            private string URL = "";
            //private readonly decimal igv = 0.18m;
            private SUNATPagoManager _manager;

            public DigiflowApi(SUNATPagoManager manager)
            {
                this._manager = manager;
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarComprobante(CabeceraDigiflow cabeceraCita, Receptor receptor, IEnumerable<DetalleCita> detalleCita)
            {
                URL = $"{_manager._digiflow_url}/RegistrarComprobantePago";

                var listDocument = new List<object>();

                foreach (var item in detalleCita)
                {
                    listDocument.Add(new
                    {
                        cantidad = item.cantidad,
                        precioUnitario = item.precio,
                        importe = (decimal)(item.precio * item.cantidad)
                    });
                }

                var registrosPago = new List<object>
                {
                    new
                    {
                        tipoPago = cabeceraCita.TipoPago,
                        banco = cabeceraCita.Banco,
                        numeroCuenta = cabeceraCita.NumeroCuenta,
                        numeroOperacion = cabeceraCita.NumeroOperacion,
                        monto = cabeceraCita.Subtotal * 1.18m,
                        fecha = cabeceraCita.FechaEmision.ToString("yyyy-MM-dd"),
                    }
                };

                var body = new {
                    tipoDocumento = cabeceraCita.TipoDocumento,
                    centroAtencion = cabeceraCita.CentroAtencion,
                    documentoPagador = cabeceraCita.DocumentoPagador,
                    razonSocialPagador = cabeceraCita.RazonSocialPagador,
                    direccion = cabeceraCita.Direccion,
                    descuento = 0,
                    descuentoValor = 0,
                    impuesto = "IGV",
                    impuestoValor = cabeceraCita.Subtotal * 0.18m,
                    subtotal = cabeceraCita.Subtotal,
                    totalConIGV = cabeceraCita.Subtotal * 1.18m,
                    detalle = listDocument,
                    registrosPago,
                };
                var _body = @"{
                       'tipoDocumento':'B',
                       'centroAtencion':'0001',
                       'documentoPagador':'20549743456',
                       'razonSocialPagador':'TIP ENGINEERING S.A.C.',
                       'direccion':'CAL.2 DE MAYO NRO. 516 INT. 408 LIMA - LIMA - MIRAFLORES',
                       'descuento':'0',
                       'descuentoValor':0,
                       'impuesto':'IGV',
                       'impuestoValor':23.3600,
                       'subtotal':129.8000,
                       'totalConIGV':153.1600,
                       'detalle':[
                          {
                             'cantidad':1,
                             'precioUnitario':129.8000,
                             'importe':129.8000
                          }
                       ],
                       'registrosPago':[
                          {
                             'tipoPago':'001',
                             'banco':'2',
                             'numeroCuenta':'11',
                             'numeroOperacion':'41020',
                             'monto':153.1600,
                             'fecha':'2020-05-22'
                          }
                       ]
                    }";

                return new Util().Request(body, URL, null, "post");
            }

            public (HttpStatusCode statusCode, string resultContent) ConsultarDocumento(dynamic body)
            {
                URL = $"{_manager._digiflow_url}/ConsultarDocumento";

                //var _body = @"{
                //               'tipoComprobante':'F',
                //               'serie':'F001',
                //               'numero':'00050168',
                //               'tipoArchivo':'pdf'
                //            }";

                return new Util().Request(body, URL, null, "post");
            }
        }

        #endregion restapi digiflow


        #region restapi tefacturo

        internal class TefacturoApi
        {
            private string URL = "";
            private SUNATPagoManager _manager;

            public TefacturoApi(SUNATPagoManager manager)
            {
                this._manager = manager;
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarFactura(CabeceraCita cabeceraCita, Receptor receptor, IEnumerable<DetalleCita> detalleCita)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/factura";

                var listDocument = new List<object>();
                short numeroOrden = 1;

                foreach (var item in detalleCita)
                {
                    listDocument.Add(new
                    {
                        cantidad = item.cantidad,
                        codigoProducto = item.iIdProtocolo.ToString(),
                        codigoProductoSunat = item.codigoProductoSunat,
                        descripcion = item.descripcion,
                        numeroOrden,
                        precioVentaUnitarioItem = item.precio,
                        tipoAfectacion = item.tipoAfectacion,
                        unidadMedida = item.unidadMedida
                    });

                    ++numeroOrden;
                }

                object body = new
                {
                    close2u = new
                    {
                        tipoIntegracion = TipoIntegracion.Offline,
                        tipoPlantilla = TipoDocSUNAT.Factura,
                        tipoRegistro = TipoRegistro.PrecioConIGV,
                    },
                    datosDocumento = new
                    {
                        fechaEmision = cabeceraCita.FechaEmision.ToString("yyyy-MM-dd"),
                        formaPago = _manager._configuration["TeFacturo:formaPago"],
                        medioPago = _manager._configuration["TeFacturo:medioPago"],
                        glosa = "",
                        moneda = _manager._configuration["TeFacturo:moneda"],
                        numero = cabeceraCita.Correlativo,
                        serie = cabeceraCita.Serie,
                    },
                    detalleDocumento = listDocument,
                    emisor = new
                    {
                        correo = _manager._configuration["TeFacturo:Emisor:correo"],
                        domicilioFiscal = new
                        {
                            departamento = _manager._configuration["TeFacturo:Emisor:departamento"],
                            direccion = _manager._configuration["TeFacturo:Emisor:direccion"],
                            distrito = _manager._configuration["TeFacturo:Emisor:distrito"],
                            pais = _manager._configuration["TeFacturo:Emisor:pais"],
                            provincia = _manager._configuration["TeFacturo:Emisor:provincia"],
                            ubigeo = _manager._configuration["TeFacturo:Emisor:ubigeo"],
                            urbanizacion = _manager._configuration["TeFacturo:Emisor:urbanizacion"],
                        },
                        nombreComercial = _manager._configuration["TeFacturo:Emisor:nombreComercial"],
                        nombreLegal = _manager._configuration["TeFacturo:Emisor:nombreLegal"],
                        numeroDocumentoIdentidad = _manager._configuration["TeFacturo:Emisor:numeroDocumentoIdentidad"],
                        tipoDocumentoIdentidad = _manager._configuration["TeFacturo:Emisor:tipoDocumentoIdentidad"],
                    },
                    informacionAdicional = new
                    {
                        tipoOperacion = _manager._configuration["TeFacturo:Emisor:tipoOperacion"]
                    },
                    receptor = new
                    {
                        correo = receptor.correo,
                        correoCopia = receptor.correoCopia,
                        domicilioFiscal = new
                        {
                            departamento = receptor.departamento,
                            direccion = receptor.direccion,
                            distrito = receptor.distrito,
                            pais = receptor.pais,
                            provincia = receptor.provincia,
                            ubigeo = receptor.ubigeo
                        },
                        nombreComercial = receptor.nombreComercial,
                        nombreLegal = receptor.nombreLegal,
                        numeroDocumentoIdentidad = receptor.numeroDocumentoIdentidad,
                        tipoDocumentoIdentidad = receptor.tipoDocumentoIdentidad
                    },
                };
                var _body = @"{
                            'close2u':{
                                'tipoIntegracion':'OFFLINE',
                                'tipoPlantilla':'01',
                                'tipoRegistro':'PRECIOS_CON_IGV'
                            },
                            'datosDocumento':{
                                'fechaEmision':'2021-05-20',
                                'formaPago':'CONTADO',
                                'medioPago':'EFECTIVO',
                                'glosa':'UNA OBSERVACION SIN TILDES',
                                'moneda':'PEN',
                                'numero':2,
                                'serie':'FFA5'
                            },
                            'detalleDocumento':[
                                {
                                    'cantidad':3,
                                    'codigoProducto':'PROD2',
                                    'codigoProductoSunat':'53103001',
                                    'descripcion':'COMISION DE RECARGAS',
                                    'numeroOrden':1,
                                    'precioVentaUnitarioItem':25,
                                    'tipoAfectacion':'GRAVADO_OPERACION_ONEROSA',
                                    'unidadMedida':'UNIDAD_SERVICIOS'
                                }
                            ],
                            'emisor':{
                                'correo':' facturacion@emisor.com.pe ',
                                'domicilioFiscal':{
                                    'departamento':'LIMA',
                                    'direccion':'DIRECCION DE VENDEMAS',
                                    'distrito':'MIRAFLORES',
                                    'pais':'PERU',
                                    'provincia':'LIMA',
                                    'ubigeo':'150133',
                                    'urbanizacion':''
                                },
                                'nombreComercial':'VENDEMAS',
                                'nombreLegal':'ARMIDA VICTORIA OCHOA TAMARIZ',
                                'numeroDocumentoIdentidad':'20431080002',
                                'tipoDocumentoIdentidad':'RUC'
                            },
                            'informacionAdicional':{
                                'tipoOperacion':'VENTA_INTERNA'
                            },
                            'receptor':{
                                'correo':'armida.ochoa@close2u.pe',
                                'correoCopia':'',
                                'domicilioFiscal':{
                                    'departamento':null,
                                    'direccion':'AV. PTE PIEDRA NRO. 386 COO. AMPLIACIÓN LAS UVAS - PRO - LIMA LIMA PUENTE PIEDRA',
                                    'distrito':null,
                                    'pais':'PERU',
                                    'provincia':null,
                                    'ubigeo':null
                                },
                                'nombreComercial':'CORPORACION E INVERSIONES PEPITO Y CLAUDIA S.A.C.',
                                'nombreLegal':'CORPORACION E INVERSIONES PEPITO Y CLAUDIA S.A.C.',
                                'numeroDocumentoIdentidad':'20603076550',
                                'tipoDocumentoIdentidad':'RUC'
                            }                            
                        }";

                //object body = new JavaScriptSerializer().DeserializeObject(_body);
                //JObject body = JObject.Parse(_body);

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarBoleta(CabeceraCita cabeceraCita, Receptor receptor, IEnumerable<DetalleCita> detalleCita)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/boleta";

                var listDocument = new List<object>();
                short numeroOrden = 1;
                decimal descuentoGlobal = 0m;

                foreach (var item in detalleCita)
                {
                    listDocument.Add(new
                    {
                        cantidad = item.cantidad,
                        codigoProducto = item.iIdProtocolo.ToString(),
                        codigoProductoSunat = item.codigoProductoSunat,
                        descripcion = item.descripcion,
                        numeroOrden,
                        precioVentaUnitarioItem = item.precio,
                        tipoAfectacion = item.tipoAfectacion,
                        unidadMedida = item.unidadMedida,
                        //descuento
                    });

                    ++numeroOrden;
                }

                object body = new
                {
                    anticipos= new object[] { },
                    close2u = new
                    {
                        tipoIntegracion = TipoIntegracion.Offline,
                        tipoPlantilla = TipoDocSUNAT.Factura,
                        tipoRegistro = TipoRegistro.PrecioConIGV,
                    },
                    datosDocumento = new
                    {
                        fechaEmision = cabeceraCita.FechaEmision.ToString("yyyy-MM-dd"),
                        fechaVencimiento = (DateTime?)null,
                        formaPago = _manager._configuration["TeFacturo:formaPago"],
                        medioPago = _manager._configuration["TeFacturo:medioPago"],
                        //condicionPago = _manager._configuration["TeFacturo:medioPago"],
                        glosa = "",
                        horaEmision = cabeceraCita.FechaEmision.ToString("HH:mm:ss"),
                        moneda = _manager._configuration["TeFacturo:moneda"],
                        numero = cabeceraCita.Correlativo,
                        //ordencompra,
                        //puntoEmisor,
                        serie = cabeceraCita.Serie,
                    },
                    descuentoGlobal,
                    detalleDocumento = listDocument,
                    //detraccion,
                    emisor = new
                    {
                        correo = _manager._configuration["TeFacturo:Emisor:correo"],
                        domicilioFiscal = new
                        {
                            departamento = _manager._configuration["TeFacturo:Emisor:departamento"],
                            direccion = _manager._configuration["TeFacturo:Emisor:direccion"],
                            distrito = _manager._configuration["TeFacturo:Emisor:distrito"],
                            pais = _manager._configuration["TeFacturo:Emisor:pais"],
                            provincia = _manager._configuration["TeFacturo:Emisor:provincia"],
                            ubigeo = _manager._configuration["TeFacturo:Emisor:ubigeo"],
                            urbanizacion = _manager._configuration["TeFacturo:Emisor:urbanizacion"],
                        },
                        nombreComercial = _manager._configuration["TeFacturo:Emisor:nombreComercial"],
                        nombreLegal = _manager._configuration["TeFacturo:Emisor:nombreLegal"],
                        numeroDocumentoIdentidad = _manager._configuration["TeFacturo:Emisor:numeroDocumentoIdentidad"],
                        tipoDocumentoIdentidad = _manager._configuration["TeFacturo:Emisor:tipoDocumentoIdentidad"],
                    },
                    //factorCambio,
                    informacionAdicional = new
                    {
                        tipoOperacion = _manager._configuration["TeFacturo:Emisor:tipoOperacion"],
                        //coVendedor
                    },
                    //otrosCargos,
                    //percepcion
                    receptor = new
                    {
                        correo = receptor.correo,
                        correoCopia = receptor.correoCopia,
                        domicilioFiscal = new
                        {
                            departamento = receptor.departamento,
                            direccion = receptor.direccion,
                            distrito = receptor.distrito,
                            pais = receptor.pais,
                            provincia = receptor.provincia,
                            ubigeo = receptor.ubigeo
                        },
                        nombreComercial = receptor.nombreComercial,
                        nombreLegal = receptor.nombreLegal,
                        numeroDocumentoIdentidad = receptor.numeroDocumentoIdentidad,
                        tipoDocumentoIdentidad = receptor.tipoDocumentoIdentidad
                    },
                };
                var _body = @"{
                               'anticipos':[
      
                               ],
                               'close2u':{
                                  'tipoIntegracion':'OFFLINE',
                                  'tipoPlantilla':'01',
                                  'tipoRegistro':'PRECIOS_SIN_IGV'
                               },
                               'datosDocumento':{
                                  'fechaEmision':'2020-06-08',
                                  'fechaVencimiento':null,
                                  'formaPago':'CONTADO',
                                  'condicionPago':'EFECTIVO',
                                  'glosa':null,
                                  'horaEmision':null,
                                  'moneda':'PEN',
                                  'numero':4,
                                  'ordencompra':null,
                                  'puntoEmisor':null,
                                  'serie':'BBV5'
                               },
                               'descuentoGlobal':null,
                               'detalleDocumento':[
                                  {
                                     'codigoProducto':'ABC123',
                                     'codigoProductoSunat':'',
                                     'descripcion':'PRODUCTO GRAVADO A',
                                     'tipoAfectacion':'GRAVADO_OPERACION_ONEROSA',
                                     'unidadMedida':'UNIDAD_BIENES',
                                     'cantidad':'1',
                                     'valorVentaUnitarioItem':100,
                                     'descuento':null
                                  },
                                  {
                                     'codigoProducto':'ABC124',
                                     'codigoProductoSunat':'',
                                     'descripcion':'PRODUCTO GRAVADO B',
                                     'tipoAfectacion':'GRAVADO_OPERACION_ONEROSA',
                                     'unidadMedida':'UNIDAD_BIENES',
                                     'cantidad':'1',
                                     'valorVentaUnitarioItem':200,
                                     'descuento':null
                                  }
                               ],
                               'detraccion':null,
                               'emisor':{
                                  'correo':'demo@democlose2u.pe',
                                  'nombreComercial':'OCHOA TAMARIZ ARMIDA VICTORIA',
                                  'nombreLegal':'OCHOA TAMARIZ ARMIDA VICTORIA',
                                  'numeroDocumentoIdentidad':'10324047366',
                                  'tipoDocumentoIdentidad':'RUC'
                               },
                               'factorCambio':null,
                               'informacionAdicional':{
                                  'tipoOperacion':'VENTA_INTERNA',
                                  'coVendedor':'analisis_sininv@democlose2u.pe'
                               },
                               'otrosCargos':null,
                               'percepcion':null,
                               'receptor':{
                                  'correo':'fernando.bravo@gmail.com',
                                  'correoCopia':'',
                                  'domicilioFiscal':{
                                     'departamento':null,
                                     'direccion':'AV. PTE PIEDRA NRO. 386 COO. AMPLIACIÓN LAS UVAS - PRO - LIMA LIMA PUENTE PIEDRA',
                                     'distrito':null,
                                     'pais':'PERU',
                                     'provincia':null,
                                     'ubigeo':'150101',
                                     'urbanizacion':''
                                  },
                                  'nombreComercial':'FERNANDO BRAVO',
                                  'nombreLegal':'FERNANDO BRAVO',
                                  'numeroDocumentoIdentidad':'71395170',
                                  'tipoDocumentoIdentidad':'DOC_NACIONAL_DE_IDENTIDAD'
                               }
                            }";

                //object body = new JavaScriptSerializer().DeserializeObject(_body);
                //JObject body = JObject.Parse(_body);

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarNotaDeCredito(string tipoDocumento, dynamic body)
            {
                URL = $"{_manager._tefacturo_url}/nota-credito";

                var _body = @"{
                                {
                                   'anticipos':[
      
                                   ],
                                   'close2u':{
                                      'tipoIntegracion':'OFFLINE',
                                      'tipoPlantilla':'01',
                                      'tipoRegistro':'PRECIOS_SIN_IGV'
                                   },
                                   'comprobanteAjustado':{
                                      'serie':'BBV1',
                                      'numero':24,
                                      'tipoDocumento':'BOLETA',
                                      'fechaEmision':'2020-06-09'
                                   },
                                   'datosDocumento':{
                                      'fechaEmision':'2020-06-10',
                                      'fechaVencimiento':null,
                                      'formaPago':'CONTADO',
                                      'medioPago':'EFECTIVO',
                                      'condicionPago':'CONTADO',
                                      'glosa':'Anulacion',
                                      'horaEmision':'01:13:07',
                                      'moneda':'PEN',
                                      'serie':'BBC1'
                                   },
                                   'descuentoGlobal':null,
                                   'detalleDocumento':[
                                      {
                                         'codigoProducto':'ABC123',
                                         'codigoProductoSunat':'',
                                         'descripcion':'PRODUCTO GRAVADO A',
                                         'tipoAfectacion':'GRAVADO_OPERACION_ONEROSA',
                                         'unidadMedida':'UNIDAD_BIENES',
                                         'cantidad':1,
                                         'valorVentaUnitarioItem':50
                                      }
                                   ],
                                   'detraccion':null,
                                   'emisor':{
                                      'correo':'demo@democlose2u.pe',
                                      'nombreComercial':'OCHOA TAMARIZ ARMIDA VICTORIA',
                                      'nombreLegal':'OCHOA TAMARIZ ARMIDA VICTORIA',
                                      'numeroDocumentoIdentidad':'10324047366',
                                      'tipoDocumentoIdentidad':'RUC'
                                   },
                                   'factorCambio':null,
                                   'informacionAdicional':{
                                      'tipoOperacion':null,
                                      'coVendedor':'analisis_sininv@democlose2u.pe'
                                   },
                                   'motivo':'ANULACION_OPERACION',
                                   'otrosCargos':'',
                                   'percepcion':null,
                                   'receptor':{
                                      'correo':'ventas@pepitoyclaudia.com.pe',
                                      'correoCopia':'otrocorreo@pepitoyclaudia.com.pe',
                                      'domicilioFiscal':{
                                         'departamento':null,
                                         'direccion':' AV. PTE PIEDRA NRO. 386 COO. AMPLIACIÓN LAS UVAS - PRO - LIMA LIMA PUENTE PIEDRA',
                                         'distrito':null,
                                         'pais':null,
                                         'provincia':null,
                                         'ubigeo':'150101',
                                         'urbanizacion':null
                                      },
                                      'nombreComercial':'JOSE LOPEZ RAMIREZ',
                                      'nombreLegal':'JOSE LOPEZ RAMIREZ',
                                      'numeroDocumentoIdentidad':'20756453',
                                      'tipoDocumentoIdentidad':'DOC_NACIONAL_DE_IDENTIDAD'
                                   }
                                }
                            }";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarNotaDeDebito(dynamic body)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/nota-debito";

                var _body = @"{

                            }";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarGuiaDeRemision(dynamic body)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/guia-remision";

                var _body = @"{
                                {
                                   'close2u':{
                                      'tipoIntegracion':'OFFLINE',
                                      'tipoPlantilla':'01',
                                      'tipoRegistro':'PRECIOS_CON_IGV'
                                   },
                                   'datosDocumento':{
                                      'fechaEmision':'2020-07-14',
                                      'glosa':'',
                                      'numero':2,
                                      'serie':'T002'
                                   },
                                   'documentosRelacionados':[
      
                                   ],
                                   'remitente':{
                                      'nombreLegal':'Empresa Remitente',
                                      'numeroDocumentoIdentidad':'20351589958',
                                      'tipoDocumentoIdentidad':'RUC'
                                   },
                                   'destinatario':{
                                      'nombreLegal':'Empresa Destino',
                                      'numeroDocumentoIdentidad':'20351589959',
                                      'tipoDocumentoIdentidad':'RUC'
                                   },
                                   'datosEnvio':{
                                      'motivoTraslado':'VENTA',
                                      'descripcionTraslado':'',
                                      'transbordoProgramado':'False',
                                      'pesoBruto':'101',
                                      'unidadMedida':'KILOS',
                                      'numeroPallet':'0',
                                      'modalidadTraslado':'PRIVADO',
                                      'fechaTraslado':'2020-07-14',
                                      'fechaEntrega':'2020-07-14',
                                      'puntoLlegada':{
                                         'departamento':'15',
                                         'direccion':'AV PRINCIPAL 234',
                                         'distrito':'SANTIAGO DE CHUCO',
                                         'pais':'PERU',
                                         'provincia':'LIMA',
                                         'ubigeo':'150133',
                                         'urbanizacion':'Santa Rosa'
                                      },
                                      'puntoPartida':{
                                         'departamento':'15',
                                         'direccion':'AV SECUNDARIA 654',
                                         'distrito':'SANTIAGO DE SURCO',
                                         'pais':'PERU',
                                         'provincia':'LIMA',
                                         'ubigeo':'150133',
                                         'urbanizacion':'Villa Hermosa'
                                      },
                                      'numeroContenedor':''
                                   },
                                   'transportista':{
                                      'nombreLegal':'Empresa Transportista',
                                      'numeroDocumentoIdentidad':'20603076550',
                                      'tipoDocumentoIdentidad':'RUC'
                                   },
                                   'vehiculos':[
                                      {
                                         'placa':'ACY-130',
                                         'conductor':{
                                            'nombreLegal':'Juan Lopez R',
                                            'numeroDocumentoIdentidad':'40867546',
                                            'tipoDocumentoIdentidad':'DOC_NACIONAL_DE_IDENTIDAD'
                                         }
                                      }
                                   ],
                                   'detalleGuia':[
                                      {
                                         'numeroOrden':1,
                                         'cantidad':1,
                                         'codigoProducto':'ABC123',
                                         'descripcion':'PRODUCTO GRAVADO A',
                                         'unidadMedida':'UNIDAD_BIENES'
                                      }
                                   ]
                                }
                            }";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "post");
            }

            public (HttpStatusCode statusCode, string resultContent) GenerarRetencion(dynamic body)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/retencion";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "post");
            }

            public (HttpStatusCode statusCode, string resultContent) ConsultarPdf(dynamic body)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/consultarPdf";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

            public (HttpStatusCode statusCode, string resultContent) ConsultarXml(dynamic body)
            {
                URL = $"{_manager._tefacturo_url}/comprobantesapi/comprobante/emisor/{body.emisor}/comprobante/{body.serie}/{body.numero}/{body.tipoComprobante}/archivo?tipo-archivo=0";

                return new Util().Request(new { }, URL, _manager._tefacturo_apikey, "get");
            }

            public (HttpStatusCode statusCode, string resultContent) ConsultarEstados(object body)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/consultarEstado";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

            public (HttpStatusCode statusCode, string resultContent) ResumenDeBaja(object body)
            {
                URL = $"{_manager._tefacturo_url}/apiemisor/invoice2u/integracion/resumen-baja";

                return new Util().Request(body, URL, _manager._tefacturo_apikey, "put");
            }

        }

        #endregion restapi tefacturo

        internal class Util
        {
            internal Util() { }

            public (HttpStatusCode statusCode, string resultContent) Request(object data, string url, string api_key, string type_method)
            {
                HttpResponseMessage result = null;
                using var client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", api_key);
                if (!string.IsNullOrEmpty(api_key))
                {
                    client.DefaultRequestHeaders.Add("X-Auth-Token", api_key);
                }
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                if (type_method.Equals("get"))
                {
                    result = client.GetAsync(url).Result;
                }
                else if (type_method.Equals("post"))
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    result = client.PostAsync(url, content).Result;
                }
                else if (type_method.Equals("put"))
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    result = client.PutAsync(url, content).Result;
                }
                else if (type_method.Equals("delete"))
                {
                    result = client.DeleteAsync(url).Result;
                }

                if (result.StatusCode != HttpStatusCode.Created)
                {

                }

                string resultContent = result.Content.ReadAsStringAsync().Result;
                //dynamic jsonData = new JavaScriptSerializer().DeserializeObject(resultContent);
                //dynamic jsonData = JObject.Parse(resultContent);

                return (result.StatusCode, resultContent);
            }
        }

        internal partial class CabeceraCita
        {
            public DateTime FechaEmision { get; set; }
            public string Serie { get; set; }
            public int Correlativo { get; set; }
        }

        internal partial class CabeceraDigiflow
        {
            public DateTime FechaEmision { get; set; }
            public string Serie { get; set; }
            public int Correlativo { get; set; }


            public string TipoDocumento { get; set; }
            public string CentroAtencion { get; set; }
            public string DocumentoPagador { get; set; }
            public string TipoDocumentoIdentidad { get; set; }
            public string Direccion { get; set; }
            public int NumeroOperacion { get; set; }
            public string RazonSocialPagador { get; set; }
            public decimal Subtotal { get; set; }
            public string TipoPago { get; set; }
            public int? Banco { get; set; }
            public int? NumeroCuenta { get; set; }
            public string Pais { get; set; }
            public string Departamento { get; set; }
            public string Provincia { get; set; }
            public string Distrito { get; set; }
            public string Ubigeo { get; set; }
        }


        internal partial class Receptor
        {
            //string correo, string correoCopia, string nombreComercial, string nombreLegal, string numeroDocumentoIdentidad, string tipoDocumentoIdentidad, string direccion,
            //string pais, string departamento, string provincia, string distrito, string ubigeo

            public string correo { get; set; }
            public string correoCopia { get; set; }
            public string nombreComercial { get; set; }
            public string nombreLegal { get; set; }
            public string numeroDocumentoIdentidad { get; set; }
            public string tipoDocumentoIdentidad { get; set; }
            public string direccion { get; set; }
            public string pais { get; set; }
            public string departamento { get; set; }
            public string provincia { get; set; }
            public string distrito { get; set; }
            public string ubigeo { get; set; }
        }

        internal partial class DetalleCita
        {
            //int? iIdCotEmpProtocolo, int iIdProtocolo, string codigoProducto, string codigoProductoSunat, string descripcion, decimal precio,
            //string tipoAfectacion, string unidadMedida, int tipoProtocolo, int iIdCitaTrabajador, int cantidad
            public int? iIdCotEmpProtocolo { get; set; }
            public int iIdProtocolo { get; set; }
            public string codigoProducto { get; set; }
            public string codigoProductoSunat { get; set; }
            public string descripcion { get; set; }
            public decimal precio { get; set; }
            public string tipoAfectacion { get; set; }
            public string unidadMedida { get; set; }
            public int tipoProtocolo { get; set; }
            public int iIdCitaTrabajador { get; set; }
            public int cantidad { get; set; }
        }

        #region Diccionarios

        internal static class TipoIntegracion
        {
            public const string Offline = "OFFLINE";
            public const string Online = "ONLINE";
        }

        internal static class FormaPago
        {
            public const string Contado = "CONTADO";
            public const string Credito = "CREDITO";
        }

        internal static class MedioPago
        {
            public const string DepositoEncuenta = "DEPOSITO_CUENTA";
            public const string Transferenciabancaria = "TRANSFERENCIA_FONDOS";
            public const string TarjetaDeDebito = "TARJETA_DEBITO";
            public const string Efectivo = "EFECTIVO";
            public const string OtrosMediosDePago = "OTROS_MEDIO_PAGO";
        }

        internal static class Moneda
        {
            public const string Soles = "PEN";
            public const string Dolares = "USD";
        }

        internal static class TipoDocumento
        {
            public const string Factura = "FACTURA";
            public const string BoletaDeVenta = "BOLETA";
            public const string NotaDeCredito = "NOTACREDITO";
            public const string NotaDeDebito = "NOTADEBITO";
        }

        internal static class TipoDocSUNAT
        {
            public const string Factura = "01";
            public const string Boleta = "03";
            public const string NotaDeCredito = "07";
            public const string NotaDeDebito = "08";
        }

        internal static class TipoDocIDentidad
        {
            public const string SinRUC = "DOC_TRIB_NO_DOM_SIN_RUC";
            public const string DNI = "DOC_NACIONAL_DE_IDENTIDAD";
            public const string CE = "CARNET_DE_EXTRANJERIA";
            public const string RUC = "RUC";
            public const string PASAPORTE = "PASAPORTE";
            public const string DIPLOMATICO = "CED_DIPLOMATICA_IDENTIDAD";
        }

        internal static class TipoOperacion
        {
            public const string VentaInterna = "VENTA_INTERNA";
            public const string Exportacion = "EXPORTACION";
            public const string NoDomiciliados = "NO_DOMICILIADOS";
            public const string VentaInternaAnticipos = "VENTA_INTERNA_ANTICIPOS";
            public const string VentaItinerante = "ENTA_ITINERANTE";
            public const string FacturaGuia = "FACTURA_GUIA";
            public const string VentaArrozPilado = "VENTA_ARROZ_PILADO";
            public const string FacturaComprobanteDePercepcion = "FACTURA_COMPROBANTE_PERCEPCION";
        }

        internal static class TipoRegistro
        {
            public const string PrecioConIGV = "PRECIOS_CON_IGV";
            public const string PrecioSinIGV = "PRECIOS_SIN_IGV";
        }

        internal static class TipoAfectacion
        {
            public const string GravadoOperacionOnerosa = "GRAVADO_OPERACION_ONEROSA";
            public const string GravadoRetiroPorPremio = "GRAVADO_RETIRO_POR_PREMIO";
            public const string GravadoRetiroPorDonacion = "GRAVADO_RETIRO_POR_DONACION";
            public const string GravadoRetiro = "GRAVADO_RETIRO";
            public const string GravadoRetiroPorPublicidad = "GRAVADO_RETIRO_POR_PUBLICIDAD";
            public const string GravadoBonificaciones = "GRAVADO_BONIFICACIONES";
            public const string GravadoRetiroPorEntregaATrabajadores = "GRAVADO_RETIRO_POR_ENTREGA_A_TRABAJADORES";
            public const string GravadoIvap = "GRAVADO_IVAP";
            public const string ExoneradoOperacionOnerosa = "EXONERADO_OPERACION_ONEROSA";
            public const string ExoneradoTransferenciaGratuita = "EXONERADO_TRANSFERENCIA_GRATUITA";
            public const string InafectoOperacionOnerosa = "INAFECTO_OPERACION_ONEROSA";
            public const string InafectoRetiroPorBonifacion = "INAFECTO_RETIRO_POR_BONIFACION";
            public const string InafectoRetiro = "INAFECTO_RETIRO";
            public const string InafectoRetiroPorMuestrasMedicas = "INAFECTO_RETIRO_POR_MUESTRAS_MEDICAS";
            public const string InafectoRetiroPorConvenioColectivo = "INAFECTO_POR_CONVENIO_COLECTIVO";
            public const string InafectoRetiroPorPremio = "INAFECTO_RETIRO_POR_PREMIO";
            public const string InafectoRetiroPorPublicidad = "INAFECTO_RETIRO_POR_PUBLICIDAD";
            public const string Exportacion = "EXPORTACION";
        }

        internal static class Unidad
        {
            public const string UnidadBienes = "UNIDAD_BIENES";
            public const string UnidadServicios = "UNIDAD_SERVICIOS";
            public const string Jarra = "JARRA";
            public const string Kilos = "KILOS";
            public const string Balde = "BALDE";
            public const string Barriles = "BARRILES";
            public const string Bobinas = "BOBINAS";
            public const string Bolsa = "BOLSA";
            public const string Botellas = "BOTELLAS";
            public const string Caja = "CAJA";
            public const string Cartones = "CARTONES";
            public const string CentimetroCuadrado = "CENTIMETRO_CUADRADO";
            public const string CentimetroCubico = "CENTIMETRO_CUBICO";
            public const string CentimetroLineal = "CENTIMETRO_LINEAL";
            public const string CientodeUnidades = "CIENTO_DE_UNIDADES";
            public const string Cilindro = "CILINDRO";
            public const string Conos = "CONOS";
            public const string Docena = "DOCENA";
            public const string Fardo = "FARDO";
            public const string GalonIngles = "GALON_INGLES";
            public const string Gramo = "GRAMO";
            public const string Gruesa = "GRUESA";
            public const string Hectolitro = "HECTOLITRO";
            public const string Hoja = "HOJA";
            public const string Hora = "HORA";
            public const string Juego = "JUEGO";
            public const string Kilogramo = "KILOGRAMO";
            public const string Kilometro = "KILOMETRO";
            public const string Kilovatio = "KILOVATIO";
            public const string Kit = "KIT";
            public const string Latas = "LATAS";
            public const string Libras = "LIBRAS";
            public const string Litro = "LITRO";
            public const string MegawattHora = "MEGAWATT_HORA";
            public const string Metro = "METRO";
            public const string MetroCuadrado = "METRO_CUADRADO";
            public const string MetroCubico = "METRO_CUBICO";
            public const string Miligramos = "MILIGRAMOS";
            public const string Mililitro = "MILILITRO";
            public const string Milimetro = "MILIMETRO";
            public const string MilimetroCuadrado = "MILIMETRO_CUADRADO";
            public const string Millares = "MILLARES";
            public const string MillondeUnidades = "MILLON_DE_UNIDADES";
            public const string Onzas = "ONZAS";
            public const string Paletas = "PALETAS";
            public const string Paquete = "PAQUETE";
            public const string Par = "PAR";
            public const string Pies = "PIES";
            public const string PiesCuadrados = "PIES_CUADRADOS";
            public const string PiesCubicos = "PIES_CUBICOS";
            public const string Piezas = "PIEZAS";
            public const string Placas = "PLACAS";
            public const string Pliego = "PLIEGO";
            public const string Pulgadas = "PULGADAS";
            public const string Resma = "RESMA";
            public const string Tambor = "TAMBOR";
            public const string ToneladaCorta = "TONELADA_CORTA";
            public const string ToneladaLarga = "TONELADA_LARGA";
            public const string Toneladas = "TONELADAS";
            public const string Tubos = "TUBOS";
            public const string UsGalon = "US_GALON";
            public const string Yarda = "YARDA";
            public const string YardaCuadrada = "YARDA_CUADRADA";
        }

        #endregion Diccionario

    }
}