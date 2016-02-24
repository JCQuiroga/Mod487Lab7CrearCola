using System;
using System.Collections.Generic;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace EscribirCola
{
    public class ManejadorCola
    {
        private static ManejadorCola _instancia;
        public static ManejadorCola Instancia
        {
            get
            {
                if (_instancia == null) _instancia = new ManejadorCola();
                return _instancia;
            }
        }


        public bool CrearCola(string conn, string nombre, int tamano, int tiempo)
        {
            var c = NamespaceManager.CreateFromConnectionString(conn);

            if (c.QueueExists(nombre)) return false;

            var cola = new QueueDescription(nombre);
            cola.MaxSizeInMegabytes = tamano;
            cola.DefaultMessageTimeToLive = new TimeSpan(0, 0, tiempo);

            try
            {
                c.CreateQueue(cola);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return false;
        }

        public void EnviarMensaje(String conn, String nombre, Dictionary<String, String> parametros, String texto)
        {
            QueueClient cl = QueueClient.CreateFromConnectionString(conn, nombre);
            BrokeredMessage msg = new BrokeredMessage(texto);
            foreach (var key in parametros.Keys)
            {
                msg.Properties[key] = parametros[key];
            }

            cl.Send(msg);
        }
    }
}