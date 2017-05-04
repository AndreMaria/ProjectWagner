using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Script.Serialization;

namespace ProjectWagner.Util
{
    public class Gateway
    {
        public List<Models.Transporte> GetTransporte()
        {
            string url = ConfigurationManager.AppSettings["DadosAbertosRio"];
            HttpWebRequest request = null;
            WebResponse response = null;
            Stream dataStream = null;
            StreamReader reader = null;
            JavaScriptSerializer serializer = null;
            List<Models.Transporte> listResult = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Get;
                request.Accept = "application/json";

                response = request.GetResponse();
                dataStream = response.GetResponseStream();
                reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

                dynamic resultado = serializer.DeserializeObject(responseFromServer);

                foreach (KeyValuePair<string, object> entry in resultado)
                {
                    var key = entry.Key;
                    if (key == "DATA")
                    {
                        listResult = new List<Models.Transporte>();
                        System.Array list = (System.Array)entry.Value;
                        foreach (var item in list)
                        {
                            System.Array sublist = (System.Array)item;

                            sublist.GetValue(0);

                            DateTime validation;
                            string format = "dd/MM/yyyy";

                            String data = (String)sublist.GetValue(0);
                            data = data.Replace('-', '/');
                            DateTime.TryParseExact(data, format, new CultureInfo("pt-BR", true), DateTimeStyles.None, out validation);

                            DateTime.TryParse(data, out validation);

                            float Latitude = 0;
                            float.TryParse(sublist.GetValue(3).ToString(), out Latitude);

                            float Longitude = 0;
                            float.TryParse(sublist.GetValue(4).ToString(), out Longitude);

                            decimal Velocidade = 0;
                            decimal.TryParse(sublist.GetValue(5).ToString(), out Velocidade);

                            listResult.Add(new Models.Transporte()
                            {
                                DataHora = validation,
                                Ordem = (String)sublist.GetValue(1),
                                Linha = sublist.GetValue(2).ToString(),
                                Latitude = Latitude,
                                Longitude = Longitude,
                                Velocidade = Velocidade
                            });

                        }       

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (null != reader)
                {
                    reader.Close();
                    reader.Dispose();
                }

                if (null != dataStream)
                {
                    dataStream.Close();
                    dataStream.Dispose();
                }

                if (null != response)
                {
                    response.Close();
                    response.Dispose();
                }
            }

            return listResult;
        }

    }
}