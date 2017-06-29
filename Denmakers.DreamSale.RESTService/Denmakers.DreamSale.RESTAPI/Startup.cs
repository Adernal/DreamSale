using System;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Denmakers.DreamSale.RESTAPI;
using Autofac;

[assembly: OwinStartup(typeof(Denmakers.DreamSale.RESTAPI.Startup))]

namespace Denmakers.DreamSale.RESTAPI
{
    public class Startup
    {
    }
}