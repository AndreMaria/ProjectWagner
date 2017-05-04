using ProjectWagner.Models;
using ProjectWagner.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectWagner.Controllers
{
    public class TransporteController : ApiController
    {
        [Route("api/Transporte/ObterTodasPosicoes")]
        [System.Web.Http.HttpGet]
        public Result ObterTodasPosicoes()
        {
            String retorno = String.Empty;
            Gateway oGateway = null;
            Result oResult = new Result();
            try
            {
                oGateway = new Gateway();
                var list = oGateway.GetTransporte();
                if (null != list)
                {
                    oResult.Mensagem = "Processo realizado com sucesso!";
                    oResult.Transportes = list;
                }
                else
                {
                    oResult.Mensagem = "Não existe dados relacionados!";
                }
                oResult.Status = true;
            }
            catch (Exception ex)
            {
                oResult.Mensagem = String.Format("[Erro]: {0} [Data]: {1}", ex.Message, DateTime.Now);
                oResult.Status = false;
            }
            return oResult;
        }
    }
}
