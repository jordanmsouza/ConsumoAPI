using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationApp.Interfaces;
using Entities.Entities;
using Entities.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Web_ECommerce.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogger<BaseController> logger;

        public readonly UserManager<ApplicationUser> _userManager;

        public readonly InterfaceLogSistemaApp _InterfaceLogSistemaApp;

        public BaseController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, InterfaceLogSistemaApp InterfaceLogSistemaApp)
        {
            this.logger = logger;
            this._userManager = userManager;
            this._InterfaceLogSistemaApp = InterfaceLogSistemaApp;
        }

        public async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);
            return idUsuario.Id;
        }


        public async Task LogEcommerce(EnumTipoLog tipoLog, Object objeto)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await _InterfaceLogSistemaApp.Add(new LogSistema
            {
                TipoLog = tipoLog,
                JsonInformacao = JsonConvert.SerializeObject(objeto),
                UserId = await RetornarIdUsuarioLogado(),
                NomeAction = actionName,
                NomeController = controllerName
            });
        }

    }
}
