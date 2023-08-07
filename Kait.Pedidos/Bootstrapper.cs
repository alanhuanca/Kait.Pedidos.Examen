using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Mvc5;
using Unity;
using Kaits.Aplicacion.Productos;
using Kaits.Infraestructura.Productos;
using Kaits.Aplicacion.Clientes;
using Kaits.Infraestructura.Clientes;
using Kaits.Aplicacion.Pedidos;
using Kaits.Infraestructura.Pedidos;

namespace Kait.Pedidos
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer(); 
            container.RegisterType<IProductoServicio, ProductoServicio>();
            container.RegisterType<IProductoRepositorio, ProductoRepositorio>();
            container.RegisterType<IClienteServicio, ClienteServicio>();
            container.RegisterType<IClienteRepositorio, ClienteRepositorio>();
            container.RegisterType<IPedidoServicio, PedidoServicio>();
            container.RegisterType<IPedidoRepositorio, PedidoRepositorio>();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {

        }
    }
}